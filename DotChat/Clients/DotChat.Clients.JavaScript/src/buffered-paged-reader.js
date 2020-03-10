import _ from 'lodash';

export class BufferedPagedReader{

    /*
    loadPage: function(cursor)
    settings
    .frameSize: number
    .maxBufferSize: number
    .minBufferSize: number
    .keyFunction : function(item):any
    .sortKeyFunction : function(item):int
    */
    constructor(loadPage, settings){
        this._loadPage = loadPage;
        this._onFrameChanged = [];
        this._settings = settings;
        this._buffer = [];
        this._bufferNextCursor = null;
        this._bufferPreviousCursor = null;
        this._frameIndex = 0;
        this._refCount = 0;
    }

    async open(){
        if(!this._openPromise){
            this._openPromise = this._open();
        }
        await this._openPromise;
    }

    get opened(){
        return this._opened;
    }

    aquire(onFrameChanged){
        if(this.disposed){
            throw new 'This buffered reader is disposed and can not be aquired.';
        }
        this._onFrameChanged.push(onFrameChanged);
        this._refCount = this._refCount + 1;
        return () => this.release(onFrameChanged);
    }

    release(onFrameChanged){
        const index = this._onFrameChanged.indexOf(onFrameChanged);
        if (index > -1) {
            this._onFrameChanged.splice(index, 1);
          }
        if(this._refCount > 0){
            this._refCount = this._refCount - 1;
        }    
    }

    get closed(){
        return this._refCount == 0;
    }

    dispose(){
        this._disposed = true;
    }

    get disposed(){
        return this._disposed;
    }

    get current(){
        return this._frame;
    }

    async next(){
        var result;
        var frameIndex = Math.max(0, this._frameIndex - this._settings.frameSize);
        if(frameIndex === 0){
            if(this._bufferNextCursor == null){
                result = this._frameIndex;
            }else{
                var loadedCount = await this._safeLoadNextCursor();
                frameIndex = Math.max(loadedCount + this._frameIndex - this._settings.frameSize, 0);
                result = Math.min(loadedCount + this._frameIndex, this._settings.frameSize);
            }
        }else{
            result = this._settings.frameSize;
        }
        this._frameIndex = frameIndex;
        this._setFrame();
        this._loadNextCursors();
        return result;
    }

    async previous(){
        var result;
        var maxFrameIndex = this._buffer.length - this._settings.frameSize;
        var frameIndex = Math.min(maxFrameIndex, this._frameIndex + this._settings.frameSize);
        if(frameIndex === maxFrameIndex){
            if(this._bufferPreviousCursor == null){
                result = maxFrameIndex - this._frameIndex;
            }else{
                var loadedCount = await this._safeLoadPreviousCursor();
                frameIndex = Math.min(this._settings.frameSize + this._frameIndex, maxFrameIndex + loadedCount);
                result = Math.min((maxFrameIndex - this._frameIndex) + loadedCount, this._settings.frameSize);
            }
        }else{
            result = this._settings.frameSize;
        }
        this._frameIndex = frameIndex;
        this._setFrame();
        this._loadPreviousCursors();
        return result;
    }

    get(id){
        return this._buffer.find(r => this._settings.keyFunction(r) === id);
    }

    addOrUpdate(key, item, updateFunc){
        var exist = _.find(this._buffer, (i) => _.isEqual(this._settings.keyFunction(i), key));
        if(exist){
            if(exist.version == undefined || exist.version < item.version){
                updateFunc(exist);
            }
        }else{
            var index = _.sortedLastIndexBy(this._buffer, item, this._settings.sortKeyFunction);
            this._buffer.splice(index, 0, item);
        }
        this._setFrame();
    }

    update(key, newVersion, updateFunc){
        var exist = _.find(this._buffer, (i) => _.isEqual(this._settings.keyFunction(i), key));
        if(exist){
            if(exist.version == undefined || exist.version < newVersion){
                updateFunc(exist);
            }
        }
        this._setFrame();
    }

    async _open(){
        this._opened = true;
        var page = await this._loadPage();
        this._buffer = page.items;
        this._bufferNextCursor = page.next;
        this._bufferPreviousCursor = page.previous;
        this._setFrame();
        this._loadPreviousCursors();
    }

    _setFrame(){
        var frame = this._buffer.slice(-this._frameIndex - this._settings.frameSize, this._frameIndex === 0 ? undefined : - this._frameIndex);
        this._frame = frame;
        if(!this.closed){
            this._onFrameChanged.forEach(onFrameChanged => onFrameChanged(frame));
        }
    }

    async _loadNextCursor(){
        var page = await this._loadPage(this._bufferNextCursor);
        this._bufferNextCursor = page.next;
        _.pullAllWith(page.items, this._buffer, (i1, i2) => _.isEqual(this._settings.keyFunction(i1), this._settings.keyFunction(i2)));
        var removeCount = this._buffer.length + page.items.length - this._settings.maxBufferSize;
        if(removeCount > 0){
            this._buffer.splice(0, removeCount);
        }
        this._buffer.push(...page.items);
        this._sortBuffer();
        return page.items.length;
    }

    async _safeLoadNextCursor(){
        if(this._nextPagePromise){
            return await this._nextPagePromise;
        }else{
            this._nextPagePromise = this._loadNextCursor();
            var result = await this._nextPagePromise;
            this._nextPagePromise = null;
            return result;
        }
    }

    async _loadPreviousCursor(){
        var page = await this._loadPage(this._bufferNextCursor);
        this._bufferPreviousCursor = page.previous;
        _.pullAllWith(page.items, this._buffer, (i1, i2) => _.isEqual(this._settings.keyFunction(i1), this._settings.keyFunction(i2)));
        var removeCount = this._buffer.length + page.items.length - this._settings.maxBufferSize;
        if(removeCount > 0){
            this._buffer.splice(-1, removeCount);
        }
        this._buffer.splice(0, 0, ...page.items);
        this._sortBuffer();
        return page.items.length;
    }

    async _safeLoadPreviousCursor(){
        if(this._previousPagePromise){
            return await this._previousPagePromise;
        }else{
            this._previousPagePromise = this._loadPreviousCursor();
            var result = await this._previousPagePromise;
            this._previousPagePromise = null;
            return result;
        }
    }

    _sortBuffer(){
        this._buffer.sort((a, b) => {
            var x = this._settings.sortKeyFunction(a); 
            var y = this._settings.sortKeyFunction(b);
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }

    async _loadPreviousCursors(){
        while(this._buffer.length < this._settings.minBufferSize){
            if(await this._safeLoadPreviousCursor() === 0){
                break;
            }
        }
    }

    async _loadNextCursors(){
        while(this._buffer.length < this._settings.minBufferSize){
            if(await this._safeLoadNextCursor() === 0){
                break;
            }
        }
    }
}
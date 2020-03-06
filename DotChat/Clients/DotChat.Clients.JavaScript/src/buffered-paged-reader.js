import _ from 'lodash';

export class BufferedPagedReader{

    /*
    loadPage: function(cursor)
    onFrameChanged: function(items)
    settings
    .frameSize: number
    .maxBufferSize: number
    .minBufferSize: number
    .keyFunction : function(item):any
    .sortKeyFunction : function(item):int
    */
    constructor(loadPage, onFrameChanged, settings){
        this._loadPage = loadPage;
        this._onFrameChanged = onFrameChanged;
        this._settings = settings;
        this._buffer = [];
        this._bufferNextCursor = null;
        this._bufferPreviousCursor = null;
        this._frameIndex = 0;
    }

    get frame(){
        return this._frame;
    }

    async init(){
        var page = await this._loadPage();
        this._buffer = page.items;
        this._bufferNextCursor = page.next;
        this._bufferPreviousCursor = page.previous; 
        this._loadPreviousCursors();
    }

    async next(){
        var result;
        var frameIndex = Math.max(0, this._frameIndex - this._settings.frameSize);
        if(frameIndex === 0){
            if(this._bufferNextCursor == null){
                result = this._frameIndex;
            }else{
                var loadedCount = await this._loadNextCursor();
                frameIndex = Math.max(loadedCount + this._frameIndex - this._settings.frameSize, 0);
                result = Math.min(loadedCount + this._frameIndex, this._settings.frameSize);
            }
        }else{
            result = this._settings.frameSize;
        }
        this._frameIndex = frameIndex;
        this._setFrame();
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
                var loadedCount = await this._loadPreviousCursor();
                frameIndex = Math.min(this._settings.frameSize + this._frameIndex, maxFrameIndex + loadedCount);
                result = Math.min((maxFrameIndex - this._frameIndex) + loadedCount, this._settings.frameSize);
            }
        }else{
            result = this._settings.frameSize;
        }
        this._frameIndex = frameIndex;
        this._setFrame();
        return result;
    }

    add(item){
        var index = _.sortedLastIndexBy(this._buffer, item, this._settings.sortKeyFunction);
        this._buffer.splice(index, 0, item);
        this._setFrame();
    }

    edit(item){
        var exist = _.find(this._buffer, (i) => _.isEqual(this._settings.keyFunction(i), this._settings.keyFunction(item)));
        if(exist){
            _.assign(exist, item);
        }else{
            this.add(item);
        }
    }

    _setFrame(){
        var frame = this._buffer.slice(-this._frameIndex - this._settings.frameSize, this._frameIndex === 0 ? undefined : - this._frameIndex);
        this._onFrameChanged(frame);
    }

    async _loadNextCursor(){
        var page = await this._loadPage(this._bufferNextCursor);
        this._bufferNextCursor = page.next;
        _.pullAllWith(page.items, this._buffer, (i1, i2) => _.isEqual(this._settings.keyFunction(i1), this._settings.keyFunction(i2)));
        var removeCount = this._settings.maxBufferSize - this._buffer.length - page.items.length;
        if(removeCount > 0){
            this._buffer.splice(0, removeCount);
        }
        this._buffer.push(...page.items);
        this._sortBuffer();
        return page.items.length;
    }

    async _loadPreviousCursor(){
        var page = await this._loadPage(this._bufferNextCursor);
        this._bufferPreviousCursor = page.previous;
        _.pullAllWith(page.items, this._buffer, (i1, i2) => _.isEqual(this._settings.keyFunction(i1), this._settings.keyFunction(i2)));
        var removeCount = this._settings.maxBufferSize - this._buffer.length - page.items.length;
        if(removeCount > 0){
            this._buffer.splice(-1, removeCount);
        }
        this._buffer.splice(0, 0, ...page.items);
        this._sortBuffer();
        return page.items.length;
    }

    _sortBuffer(){
        this._buffer.sort(function(a, b) {
            var x = this._settings.sortKeyFunction(a); 
            var y = this._settings.sortKeyFunction(b);
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }

    async _loadPreviousCursors(){
        while(this._settings.minBufferSize - this._buffer.length > 0){
            await this._loadPreviousCursor(true);
        }
    }
}
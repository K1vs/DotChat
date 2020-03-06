import BufferedPagedReader from './buffered-paged-reader';

export class ChatsService{
    /*
    connector
    callbacks
    .onFrameChanged
    settings
    .chatsSettings
    ..pageSize: number
    ..frameSize: number
    ..maxBufferSize: number
    ..minBufferSize: number
    .messagesSettings
    */
    constructor(connector, callbacks, settings){
        this._connector = connector;
        this._callbacks = callbacks;
        this._settings = settings;
    }

    get summary(){
        return this._summary;
    }

    async init(){
        this._summary = await this._connector.chats.getSummary();
    }

    createChatsReader(filter){
        var loadPage = filter ? 
            ((cursor) => this._connector.chats.getPage(filter, this._createPagedOptions(cursor))): 
            ((cursor) => this._connector.chats.getPage(this._createPagedOptions(cursor)));
        var reader = BufferedPagedReader(loadPage, this._callbacks.onFrameChanged, this._settings.chatsSettings);
        return reader;
    }

    selectChat(chat){
        this._chat = chat;
    }

    get chat(){
        return this._chat;
    }

    _createPagedOptions(cursor){
        return {
            cursor: cursor,
            limit: this._settings.chatsSettings.pageSize
        };
    }
}
import BufferedPagedReader from './buffered-paged-reader';

export class ChatsService{
    /*
    connector
    callbacks
    .onFrameChanged
    settings
    .chatsSettings
    ..maxReaders: number 
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
        this._chatsReaders = [];
    }

    get summary(){
        return this._summary;
    }

    async init(){
        this._summary = await this._connector.chats.getSummary();
    }

    getChatsReader(filter){
        var loadPage = filter ? 
            ((cursor) => this._connector.chats.getPage(filter, this._createPagedOptions(cursor))): 
            ((cursor) => this._connector.chats.getPage(this._createPagedOptions(cursor)));
        var reader = BufferedPagedReader(loadPage, frame => this._onChatsFrameChanged(frame), this._settings.chatsSettings);
        if(reader){
            
        }
        return reader;
    }

    selectChat(chat){
        this._chat = chat;
    }

    get chat(){
        return this._chat;
    }

    async loadChat(chatId){
        this._chat = await this._connector.chats.get(chatId);
    }

    _createPagedOptions(cursor){
        return {
            cursor: cursor,
            limit: this._settings.chatsSettings.pageSize
        };
    }

    _onChatsFrameChanged(frame){
        for (var chatId in this._chatMessageReaders) {
            if(obj.hasOwnProperty(chatId)) {
                var _chatMessageReaders = this._chatsMessages[chatId];
            } 
        }
        if(this._callbacks.onFrameChanged){
            this._callbacks.onFrameChanged(frame); 
        }
    }
}
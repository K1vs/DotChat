import BufferedPagedReader from './buffered-paged-reader';

export class ChatsService{
    /*
    connector
    callbacks
    .onFrameChanged
    settings
    .chatsSettings
    ..maxNamedReaders: number 
    ..pageSize: number
    ..frameSize: number
    ..maxBufferSize: number
    ..minBufferSize: number
    .messagesSettings
    ..maxReaders: number 
    ..maxNamedReaders: number 
    ..pageSize: number
    ..frameSize: number
    ..maxBufferSize: number
    ..minBufferSize: number
    */
    constructor(connector, callbacks, settings){
        this._connector = connector;
        this._callbacks = callbacks;
        this._settings = settings;
        this._chatsReaders = {
            default: null,
            named: {}
        };
        this._messagesReaders = {};
    }

    get summary(){
        return this._summary;
    }

    async init(){
        this._summary = await this._connector.chats.getSummary();
    }

    getChatsReader(onChatsFrameChanged, name, filter){
        if(name){
            var existedNamedReader = this._chatsReaders.named[name];
            if(existedNamedReader){
                existedNamedReader.aquire();
                return existedNamedReader;
            }
        }else if(this._chatsReader.default) {
            this._chatsReader.default.aquire();
            return this._chatsReader.default;
        }
        if(this._chatsReaders.named.length > this._settings.chatsSettings.maxNamedReaders){
            _.remove(this._chatsReaders.named, r => r.closed)
            .forEach(removed => removed.dispose());
        }
        var createPageOptions = (cursor) => ({
            cursor: cursor,
            limit: this._settings.chatsSettings.pageSize
        });
        var loadPage = filter ? 
            ((cursor) => this._connector.chats.getPage(filter, createPageOptions(cursor))): 
            ((cursor) => this._connector.chats.getPage(createPageOptions(cursor)));
        var reader = BufferedPagedReader(loadPage, frame => onChatsFrameChanged(frame), this._settings.chatsSettings);
        if(name){
            this._chatsReaders.named[name] = {
                reader: reader,
                filter: filter
            };
        }else{
            this._chatsReader.default = reader;
        }
        reader.aquire();
        return reader;
    }

    getMessagesReader(chatId, onChatsFrameChanged, name, filter){
        var chatMessagesReaders = this._messagesReaders[chatId];
        if(chatMessagesReaders){
            if(name){
                var existedNamedReader = chatMessagesReaders.named[name];
                if(existedNamedReader){
                    existedNamedReader.aquire();
                    return existedNamedReader;
                }
            }else if(chatMessagesReaders.default) {
                chatMessagesReaders.default.aquire();
                return chatMessagesReaders.default;
            }
        }else{
            chatMessagesReaders = {
                default: null,
                named: []
            };
        }

        this._shrinkMessagesReaders();

        var createPageOptions = (cursor) => ({
                cursor: cursor,
                limit: this._settings.messagesSettings.pageSize
        });
        var loadPage = filter ? 
            ((cursor) => this._connector.messages.getPage(filter, createPageOptions(cursor))): 
            ((cursor) => this._connector.messages.getPage(createPageOptions(cursor)));
        var reader = BufferedPagedReader(loadPage, frame => onChatsFrameChanged(frame), this._settings.chatsSettings);
        if(name){
            chatMessagesReaders.named[name] = {
                reader: reader,
                filter: filter
            };
        }else{
            chatMessagesReaders.default = reader;
        }
        reader.aquire();
        return reader;
    }

    _shrinkMessagesReaders(){
        var allChatsMessagesReaders = this._messagesReaders.entries().map(([_, value]) => value)
        var namedCount = _.sum(allChatsMessagesReader, r => r.named.length);
        if(namedCount > this._settings.messagesSettings.maxNamedReaders || allChatsMessagesReaders.length > this._settings.messagesSettings.maxNamedReaders){
            for(сhatMessagesReaders in allChatsMessagesReaders){
                _.remove(сhatMessagesReaders, r => r.closed)
                .forEach(removed => removed.dispose());
            }
        }
        if(allChatsMessagesReaders.length > this._settings.messagesSettings.maxNamedReaders){
            for(сhatMessagesReaders in allChatsMessagesReaders){
                if(сhatMessagesReaders.default.closed){
                    сhatMessagesReaders.default.dispose();
                    сhatMessagesReaders.default = null;
                }
            }
        }
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
}
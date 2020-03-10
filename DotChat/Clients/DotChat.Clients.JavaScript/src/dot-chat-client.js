import _ from 'lodash';
import {BufferedPagedReader} from './buffered-paged-reader';
import {ChatParticipantStatus} from './enums/participant-status';
import {MessageStatus} from './enums/message-status';
import {dotChatSettings} from './settings/dot-chat-settings';

export default class DotChatClient{
    /*
    userId
    connector
    callbacks
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
    constructor(userId, connector, settings, callbacks){
        this._userId = userId;
        this._connector = connector;
        this._callbacks = callbacks;
        this._settings = _.merge({}, dotChatSettings, settings);
        this._chatsReaders = {
            default: null,
            named: {}
        };
        this._messagesReaders = {};
        this._register();
    }

    get summary(){
        return this._summary;
    }

    async init(){
        await this._connector.start();
        this._summary = await this._connector.chats.getSummary();
        return this.summary;
    }

    getChatsReader(name, filter){
        if(!name && filter){
            throw 'Name is required for filtered chat reader';
        }
        if(name){
            var existedNamed = this._chatsReaders.named[name];
            if(existedNamed){
                return existedNamed.reader;
            }
        }else if(this._chatsReaders.default) {
            return this._chatsReaders.default;
        }
        if(this._chatsReaders.named.length > this._settings.chatsSettings.maxNamedReaders){
            _.remove(this._chatsReaders.named, r => r.closed).forEach(removed => removed.dispose());
        }
        var createPageOptions = (cursor) => ({
            cursor: cursor,
            limit: this._settings.chatsSettings.pageSize
        });
        var loadPage = filter ? 
            ((cursor) => this._connector.chats.getPage(filter, createPageOptions(cursor))): 
            ((cursor) => this._connector.chats.getPage(createPageOptions(cursor)));
        var reader = new BufferedPagedReader(loadPage, this._settings.chatsSettings);
        if(name){
            this._chatsReaders.named[name] = {
                reader: reader,
                filter: filter
            };
        }else{
            this._chatsReaders.default = reader;
        }
        return reader;
    }

    getMessagesReader(chatId, name, filter){
        if(!name && filter){
            throw 'Name is required for filtered message reader';
        }
        var chatMessagesReaders = this._messagesReaders[chatId];
        if(chatMessagesReaders){
            if(name){
                var existedNamedReader = chatMessagesReaders.named[name];
                if(existedNamedReader){
                    return existedNamedReader;
                }
            }else if(chatMessagesReaders.default) {
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
        var reader = new BufferedPagedReader(loadPage, this._settings.chatsSettings);
        if(name){
            chatMessagesReaders.named[name] = {
                reader: reader,
                filter: filter
            };
        }else{
            chatMessagesReaders.default = reader;
        }
        return reader;
    }

    async addChat(chatInfo, candidates){
        var chatId = await this._connector.chats.add(chatInfo, candidates);
        return chatId;
    }

    async editChatInfo(chatId, chatInfo){
        await this._connector.chats.editInfo(chatId, chatInfo);
    }

    async removeChat(chatId){
        await this._connector.chats.remove(chatId);
    }

    async addParticipant(chatId, userId, chatParticipantType){
        await this._connector.participants.add(chatId, userId, chatParticipantType);
    }

    async inviteParticipant(chatId, userId, chatParticipantType){
        await this._connector.participants.invite(chatId, userId, chatParticipantType);
    }

    async applyToChat(chatId, userId, chatParticipantType){
        await this._connector.participants.apply(chatId, userId, chatParticipantType);
    }

    async changeParticipantType(chatId, userId, chatParticipantType){
        await this._connector.participants.changeType(chatId, userId, chatParticipantType);
    }

    async removeParticipant(chatId, userId){
        await this._connector.participants.remove(chatId, userId);
    }

    async blockParticipant(chatId, userId){
        await this._connector.participants.block(chatId, userId);
    }

    async addMessage(chatId, messageInfo){
        var messageId = await this._connector.messages.add(chatId, null, messageInfo);
        var readers = this._getChatMessageReaders(chatId);
        var message = {
            ...messageInfo,
            messageId: messageId,
            pending: true,
            messageStatus: MessageStatus.actual
        };
        readers.forEach(reader => reader.addOrUpdate(messageId, message, exist => _.merge(exist, message)));
    }

    async editMessage(chatId, messageId, messageInfo){
        await this._connector.messages.edit(chatId, messageId, messageInfo);
        var readers = this._getChatMessageReaders(chatId);
        var updateExt = {
            pending: true,
            messageStatus: MessageStatus.edited
        };
        readers.forEach(reader => reader.addOrUpdate(messageId, {...messageInfo, ...updateExt}, exist => _.merge(exist, messageInfo, updateExt)));
    }

    async removeMessage(chatId, messageId){
        await this._connector.messages.remove(chatId, messageId);
        var readers = this._getChatMessageReaders(chatId);
        var removedExt = {
            pending: true,
            messageStatus: MessageStatus.removed
        };
        readers.forEach(reader => reader.addOrUpdate(messageId, {messageId: messageId, ... removedExt}, exist => _.merge(exist, removedExt)));
    }

    async readMessage(chatId, index){
        await this._connector.messages.read(chatId, index);
        this._getChats(chatId).forEach(chat => {
            var exist = chat.participants.find(r => r.userId === this._userId);
            if(exist){
                exist.readIndex = index;
            }
        });
    }

    _shrinkMessagesReaders(){
        var allChatsMessagesReaders = this._messagesReaders.entries().map(([, value]) => value);
        var namedCount = _.sum(allChatsMessagesReaders, r => r.named.length);
        if(namedCount > this._settings.messagesSettings.maxNamedReaders || allChatsMessagesReaders.length > this._settings.messagesSettings.maxNamedReaders){
            for(let сhatMessagesReaders in allChatsMessagesReaders){
                _.remove(сhatMessagesReaders, r => r.closed)
                .forEach(removed => removed.dispose());
            }
        }
        if(allChatsMessagesReaders.length > this._settings.messagesSettings.maxNamedReaders){
            for(let сhatMessagesReaders in allChatsMessagesReaders){
                if(сhatMessagesReaders.default.closed){
                    сhatMessagesReaders.default.dispose();
                    сhatMessagesReaders.default = null;
                }
            }
        }
    }

    _getChatReaders(){
        var readers = this._chatsReaders.named.entries().map(([, value]) => value).filter(r => !r.filter).map(r => r.reader);
        readers.push(this._chatsReaders.default);
        return readers;
    }

    _getChats(chatId){
        var readers = this._getChatReaders();
        var chats = _.orderBy(readers.map(r => r.get(chatId)).filter(r => r), ['version'], ['desc']);
        var refChat = chats[0];
        if(refChat){
            readers.forEach(r => r.addOrUpdate(chatId, refChat, exist => _.assign(exist, refChat)));
        }
        return chats;
    }

    async _getChatsWithLoad(chatId){
        var chats = this._getChats(chatId);
        if(chats.length === 0){
            var chat;
            try{
                chat = await this._connector.chats.get(chatId);
            }
            catch(error) {
                chat = {
                    chatId: chatId,
                    participants: []
                };
            }
            var readers = this._getChatReaders();
            readers.forEach(r => r.addOrUpdate(chatId, chat, exist => _.assign(exist, chat)));
            return this._getChats(chatId);
        }
        return chats;
    }

    _callCallback(name, notification){
        if(this._callbacks){
            var callback = this._callbacks[name];
            if(callback){
                callback(notification);
            }
        }
    }

    _registerChatsNotifications(){
        this._connector.chats.chatAdded = (notification) => {
            this._callCallback('chatAdded', notification);
            if(this._chatsReaders.default){
                this._chatsReaders.default.addOrUpdate(notification.personalizedChat.chatId, notification.personalizedChat,
                    (exist) => _.assign(exist, notification.personalizedChat));
            }
            for(let namedChatReaderBox in this._chatsReaders.named.entries().map(([,value]) => value)){
                if(!namedChatReaderBox.filter){
                    namedChatReaderBox.reader.addOrUpdate(notification.personalizedChat.chatId, notification.personalizedChat,
                        (exist) => _.assign(exist, notification.personalizedChat));
                }
            }
        };

        this._connector.chats.chatInfoEdited = (notification) => {
            this._callCallback('chatInfoEdited', notification);
            if(this._chatsReaders.default){
                this._chatsReaders.default.update(notification.chatId, (exist) => _.merge(exist, notification.chatInfo));
            }
            for(let namedChatReaderBox in this._chatsReaders.named.entries().map(([,value]) => value)){
                namedChatReaderBox.reader.update(notification.chatId, (exist) => _.merge(exist, notification.chatInfo));
            }
        };

        this._connector.chats.chatRemoved = (notification) => {
            this._callCallback('chatRemoved', notification);
            if(this._chatsReaders.default){
                this._chatsReaders.default.update(notification.chatId, (exist) => exist.removed = true);
            }
            for(let namedChatReaderBox in this._chatsReaders.named.entries().map(([,value]) => value)){
                namedChatReaderBox.reader.update(notification.chatId, (exist) => exist.removed = true);
            }
        };
    }

    _registerChatParticipantsNotifications(){
        var addOrUpdateParticipant = (participant, chat) => {
            var exist = chat.participants.find(r => r.userId === participant.userId);
            if(exist){
                if(exist.version < participant.version){
                    _.assign(exist, participant);
                }else{
                    return;
                }
            }else{
                chat.participants.push(participant);
            }
            if(this._userId === participant.userId){
                chat.lost = participant.chatParticipantStatus === ChatParticipantStatus.removed || 
                    participant.chatParticipantStatus === ChatParticipantStatus.blocked;  
            }
        };

        this._connector.participants.chatParticipantAdded = async (notification) => {
            this._callCallback('chatParticipantAdded', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            chats.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        this._connector.participants.chatParticipantApplied = async (notification) => {
            this._callCallback('chatParticipantApplied', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            chats.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        this._connector.participants.chatParticipantInvited = async (notification) => {
            this._callCallback('chatParticipantInvited', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            chats.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        this._connector.participants.chatParticipantTypeChanged = async (notification) => {
            this._callCallback('chatParticipantTypeChanged', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            chats.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };
        
        this._connector.participants.chatParticipantRemoved = async (notification) => {
            this._callCallback('chatParticipantRemoved', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            chats.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };
        
        this._connector.participants.chatParticipantBlocked = async (notification) => {
            this._callCallback('chatParticipantBlocked', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            chats.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        this._connector.participants.chatParticipantsAppended = async (notification) => {
            this._callCallback('chatParticipantsAppended', notification);
            var chats = await this._getChatsWithLoad(notification.chatId);
            notification.added.concat(notification.invited).forEach(participant => {
                chats.forEach(r => addOrUpdateParticipant(participant, r));
            });
        };
    }

    _getChatMessageReaders(chatId){
        var chatMessagesReaders = this._messagesReaders[chatId];
        if(chatMessagesReaders){
            var readers = chatMessagesReaders.named.entries().map(([, value]) => value).filter(r => !r.filter).map(r => r.reader);
            readers.push(this._chatsReaders.default);
            return readers;
        }else{
            return [];
        }
    }

    _registerChatMessagesNotifications(){
        this._connector.messages.chatMessageAdded = (notification) => {
            this._callCallback('chatMessageAdded', notification);
            var readers = this._getChatMessageReaders(notification.chatId);
            readers.forEach(reader => {
                reader.addOrUpdate(notification.message.messageId, notification.message, (exist) => _.assign(exist, notification.message));
            });
        };
        this._connector.messages.chatMessageEdited = (notification) => {
            this._callCallback('chatMessageEdited', notification);
            var readers = this._getChatMessageReaders(notification.chatId);
            readers.forEach(reader => {
                reader.addOrUpdate(notification.message.messageId, notification.message, (exist) => _.assign(exist, notification.message));
            });
        };
        this._connector.messages.chatMessageRemoved = (notification) => {
            this._callCallback('chatMessageRemoved', notification);
            var readers = this._getChatMessageReaders(notification.chatId);
            readers.forEach(reader => {
                reader.addOrUpdate(notification.message.messageId, notification.message, (exist) => _.assign(exist, notification.message));
            });
        };
        this._connector.messages.chatMessagesRead = async (notification) => {
            this._callCallback('chatMessagesRead', notification);
            var participants = await this._getChatsWithLoad(notification.chatId)
                .map(chat => chat.participants.find(p => p.userId === notification.initiatorUserId))
                .filter(r => r);
            participants.forEach(p => p.readIndex = notification.index);
        };
    }

    _register(){
        this._registerChatsNotifications();
        this._registerChatParticipantsNotifications();
        this._registerChatMessagesNotifications();
    }
}
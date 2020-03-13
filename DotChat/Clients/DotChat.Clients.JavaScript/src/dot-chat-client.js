import _ from 'lodash';
import {BufferedPagedReader} from './buffered-paged-reader';
import {ChatParticipantStatus} from './enums/participant-status';
import {MessageStatus} from './enums/message-status';
import {dotChatSettings} from './settings/dot-chat-settings';
import {CleanableValueSetter} from './cleanable-value-setter';

export default class DotChatClient{
    /*
    userId
    connector
    callbacks
    .onSummaryChanged
    settings
    .chatsSettings
    ..maxNamedReaders: number 
    ..pageSize: number
    ..frameSize: number
    ..maxBufferSize: number
    ..minBufferSize: number
    ..keyFunction : function(item):any
    ..sortKeyFunction : function(item):int
    .messagesSettings
    ..maxReaders: number 
    ..maxNamedReaders: number 
    ..pageSize: number
    ..frameSize: number
    ..maxBufferSize: number
    ..minBufferSize: number
    ..keyFunction : function(item):any
    ..sortKeyFunction : function(item):int
    */
    constructor(userId, connector, settings, callbacks){
        this._userId = userId;
        this._connector = connector;
        this._callbacks = callbacks || {};
        this._settings = _.merge({}, dotChatSettings, settings);
        this._initReadersBoxes();
    }

    async ready(){
        if(!this._releaser){
            this._releaser = this._register();
        }
        await this._connector.start();
        return this;
    }

    dispose(){
        if(this._releaser){
            this._releaser();
        }
        this._getChatReaders(true).forEach(r => r.dispose());
        this._getChatsMessageReaders(true).forEach(r => r.dispose());
        this._initReadersBoxes();
    }

    get summary(){
        return this._summary;
    }

    async loadSummary(reload){
        if(reload || !this.summary){
            await this._loadSummary();
        }
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
            this._messagesReaders[chatId] = chatMessagesReaders;
        }

        this._shrinkMessagesReaders();

        var createPageOptions = (cursor) => ({
                cursor: cursor,
                limit: this._settings.messagesSettings.pageSize
        });
        var loadPage = filter ? 
            ((cursor) => this._connector.messages.getPage(chatId, filter, createPageOptions(cursor))): 
            ((cursor) => this._connector.messages.getPage(chatId, createPageOptions(cursor)));
        var reader = new BufferedPagedReader(loadPage, this._settings.messagesSettings);
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
            messageStatus: MessageStatus.actual,
            index: Number.MAX_SAFE_INTEGER
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

    async readMessages(chatId, index){
        await this._connector.messages.read(chatId, index);
        this._getChats(chatId).forEach(chat => {
            var exist = chat.participants.find(r => r.userId === this._userId);
            if(exist){
                exist.readIndex = index;
            }
        });
    }

    async _loadSummary(){
        if(this._summaryPromiseDelay){
            await this._summaryPromiseDelay;
        }
        if(this._summaryPromise){
            return await this._summaryPromise;
        }
        this._summaryPromise = this._connector.chats.getSummary();
        this._summary = await this._summaryPromise;
        if(this._callbacks.onSummaryChanged){
            this._callbacks.onSummaryChanged(this._summary);
        }
        this._summaryPromise = null;
        this._summaryPromiseDelay = new Promise(r => setTimeout(r, this._settings.chatsSettings.loadSummaryDelay));
    }

    _initReadersBoxes(){
        this._chatsReaders = {
            default: null,
            named: {}
        };
        this._messagesReaders = {};
    }

    _shrinkMessagesReaders(){
        var allChatsMessagesReaders = Object.entries(this._messagesReaders).map(([, value]) => value);
        var namedCount = _.sum(allChatsMessagesReaders, r => r.named.length);
        if(namedCount > this._settings.messagesSettings.maxNamedReaders || allChatsMessagesReaders.length > this._settings.messagesSettings.maxNamedReaders){
            allChatsMessagesReaders.forEach(сhatMessagesReaders => {
                _.remove(сhatMessagesReaders, r => r.closed)
                .forEach(removed => removed.dispose());
            });
        }
        if(allChatsMessagesReaders.length > this._settings.messagesSettings.maxNamedReaders){
            allChatsMessagesReaders.forEach(сhatMessagesReaders => {
                if(сhatMessagesReaders.default.closed){
                    сhatMessagesReaders.default.dispose();
                    сhatMessagesReaders.default = null;
                }
            });
        }
    }

    _getChatReaders(includeFiltered){
        var readers = Object.entries(this._chatsReaders.named).map(([, value]) => value).filter(r => !r.filter || includeFiltered).map(r => r.reader);
        if(this._chatsReaders.default){
            readers.push(this._chatsReaders.default);
        }
        return readers;
    }

    _getChatsWithReaders(chatId){
        var readers = this._getChatReaders();
        var chatsWithReaders = _.orderBy(readers.map(r => ({ chat: r.get(chatId), reader: r })).filter(r => r.chat), 'chat.version', 'desc');
        var refChatWithReaders = chatsWithReaders[0];
        if(refChatWithReaders){
            readers.filter(r => r != refChatWithReaders.reader).forEach(r => r.addOrUpdate(chatId, refChatWithReaders.chat, exist => _.assign(exist, refChatWithReaders.chat)));
        }
        return chatsWithReaders;
    }

    _getChats(chatId){
        return this._getChatsWithReaders(chatId).map(cwr => cwr.chat);
    }

    async _getOrLoadChatsWithReaders(chatId){
        var chatsWithReaders = this._getChatsWithReaders(chatId);
        if(chatsWithReaders.length === 0){
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
            chatsWithReaders = this._getChatsWithReaders(chatId);
        }
        return chatsWithReaders;
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
        var chatAdded = (notification) => {
            this._loadSummary();
            this._callCallback('chatAdded', notification);
            if(this._chatsReaders.default){
                this._chatsReaders.default.addOrUpdate(notification.personalizedChat.chatId, notification.personalizedChat,
                    (exist) => _.assign(exist, notification.personalizedChat));
            }
            Object.entries(this._chatsReaders.named).map(([,value]) => value).forEach(namedChatReaderBox => {
                if(!namedChatReaderBox.filter){
                    namedChatReaderBox.reader.addOrUpdate(notification.personalizedChat.chatId, notification.personalizedChat,
                        (exist) => _.assign(exist, notification.personalizedChat));
                }
            });
        };

        var chatInfoEdited = (notification) => {
            this._loadSummary();
            this._callCallback('chatInfoEdited', notification);
            if(this._chatsReaders.default){
                this._chatsReaders.default.update(notification.chatId, (exist) => _.merge(exist, notification.chatInfo));
            }
            Object.entries(this._chatsReaders.named).map(([,value]) => value).forEach(namedChatReaderBox => {
                namedChatReaderBox.reader.update(notification.chatId, (exist) => _.merge(exist, notification.chatInfo));
            });
        };

        var chatRemoved = (notification) => {
            this._loadSummary();
            this._callCallback('chatRemoved', notification);
            if(this._chatsReaders.default){
                this._chatsReaders.default.update(notification.chatId, (exist) => exist.removed = true);
            }
            Object.entries(this._chatsReaders.named).map(([,value]) => value).forEach(namedChatReaderBox => {
                namedChatReaderBox.reader.update(notification.chatId, (exist) => exist.removed = true);
            });
        };

        var cleanableValueSetter = new CleanableValueSetter(this._connector.chats, null);
        cleanableValueSetter.add('chatAdded', chatAdded);
        cleanableValueSetter.add('chatInfoEdited', chatInfoEdited);
        cleanableValueSetter.add('chatRemoved', chatRemoved);

        return () => cleanableValueSetter.clean();
    }

    _registerChatParticipantsNotifications(){
        var addOrUpdateParticipant = (participant, chatWithReader) => {
            chatWithReader.reader.update(chatWithReader.chat.id, Number.MAX_SAFE_INTEGER, (chat) => {
                var exist = chat.participants.find(r => r.userId === participant.userId);
                if(exist){
                    if(exist.version == undefined || exist.version < participant.version){
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
            });
        };

        var chatParticipantAdded = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantAdded', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            chatsWithReaders.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantApplied = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantApplied', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            chatsWithReaders.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantInvited = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantInvited', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            chatsWithReaders.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantTypeChanged = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantTypeChanged', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            chatsWithReaders.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };
        
        var chatParticipantRemoved = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantRemoved', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            chatsWithReaders.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };
        
        var chatParticipantBlocked = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantBlocked', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            chatsWithReaders.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantsAppended = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantsAppended', notification);
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(notification.chatId);
            notification.added.concat(notification.invited).forEach(participant => {
                chatsWithReaders.forEach(r => addOrUpdateParticipant(participant, r));
            });
        };

        var cleanableValueSetter = new CleanableValueSetter(this._connector.participants, null);
        cleanableValueSetter.add('chatParticipantAdded', chatParticipantAdded);
        cleanableValueSetter.add('chatParticipantApplied', chatParticipantApplied);
        cleanableValueSetter.add('chatParticipantInvited', chatParticipantInvited);
        cleanableValueSetter.add('chatParticipantTypeChanged', chatParticipantTypeChanged);
        cleanableValueSetter.add('chatParticipantRemoved', chatParticipantRemoved);
        cleanableValueSetter.add('chatParticipantBlocked', chatParticipantBlocked);
        cleanableValueSetter.add('chatParticipantsAppended', chatParticipantsAppended);

        return () => cleanableValueSetter.clean();
    }

    _getChatsMessageReaders(includeFiltered){
        return Object.getOwnPropertyNames(this._messagesReaders).map(chatId => this._getChatMessageReaders(chatId, includeFiltered)).flat();
    }

    _getChatMessageReaders(chatId, includeFiltered){
        var chatMessagesReaders = this._messagesReaders[chatId];
        if(chatMessagesReaders){
            var readers = Object.entries(chatMessagesReaders.named).map(([, value]) => value).filter(r => !r.filter || includeFiltered).map(r => r.reader);
            if(chatMessagesReaders.default){
                readers.push(chatMessagesReaders.default);
            }
            return readers;
        }else{
            return [];
        }
    }

    async _updateChatLasts(chatId, timestamp, index, self){
        var chatsWithReaders = await this._getOrLoadChatsWithReaders(chatId);
        chatsWithReaders.forEach((chatWithReader) => {
            chatWithReader.reader.update(chatWithReader.chat.chatId, Number.MAX_SAFE_INTEGER, (chat) => {
                if(chat.topIndex <= index){
                    chat.lastTimestamp = timestamp;
                    chat.topIndex = index;
                    var thisParticipant = chat.participants.find(r => r.userId === this._userId);
                    if(self && thisParticipant.readIndex < index){
                        thisParticipant.readIndex = index; 
                        this.readMessages(chatId, index);
                    }
                    this._setChatPersonalization(chat, thisParticipant);
                }
            });
        });
    }

    _setChatPersonalization(chat, thisParticipant){
        chat.readIndex = thisParticipant.readIndex;
        chat.unreadCount = chat.topIndex - thisParticipant.readIndex;
    }

    _registerChatMessagesNotifications(){
        var update = (exist, fromNotification) => _.assign(exist, fromNotification, {pending: false});
        var read = async (chatId, userId, index, force) => {
            var chatsWithReaders = await this._getOrLoadChatsWithReaders(chatId);
            chatsWithReaders.forEach((chatWithReader) => {
                chatWithReader.reader.update(chatWithReader.chat.chatId, Number.MAX_SAFE_INTEGER, (chat) =>{
                    var participant = chat.participants.find(p => p.userId === userId);
                    if(force || index > participant.readIndex){
                        participant.readIndex = index;
                    }
                    if(userId === this._userId){
                        this._setChatPersonalization(chat, participant);
                    }
                });
            });
        };
        var chatMessageAdded = (notification) => {
            this._updateChatLasts(notification.chatId, notification.message.timestamp, notification.message.index, notification.initiatorUserId === this._userId);
            if(notification.initiatorUserId !== this._userId){
                this._loadSummary();
            }
            this._callCallback('chatMessageAdded', notification);
            var readers = this._getChatMessageReaders(notification.chatId);
            readers.forEach(reader => {
                reader.addOrUpdate(notification.message.messageId, notification.message, exist => update(exist, notification.message));
            });
        };
        var chatMessageEdited = (notification) => {
            this._updateChatLasts(notification.chatId, notification.message.timestamp, notification.message.index, notification.initiatorUserId);
            if(notification.initiatorUserId !== this._userId){
                this._loadSummary();
            }
            this._callCallback('chatMessageEdited', notification);
            var readers = this._getChatMessageReaders(notification.chatId);
            readers.forEach(reader => {
                reader.addOrUpdate(notification.message.messageId, notification.message, exist => update(exist, notification.message));
            });
        };
        var chatMessageRemoved = (notification) => {
            if(notification.initiatorUserId !== this._userId){
                this._loadSummary();
            }
            this._callCallback('chatMessageRemoved', notification);
            var readers = this._getChatMessageReaders(notification.chatId);
            readers.forEach(reader => {
                reader.addOrUpdate(notification.messageId, {messageId: notification.messageId}, exist => {
                    exist.messageStatus = MessageStatus.removed;
                });
            });
        };
        var chatMessagesRead = (notification) => {
            if(notification.initiatorUserId === this._userId){
                this._loadSummary();
            }
            this._callCallback('chatMessagesRead', notification);
            read(notification.chatId, notification.initiatorUserId, notification.index, true);
        };

        var cleanableValueSetter = new CleanableValueSetter(this._connector.messages, null);
        cleanableValueSetter.add('chatMessageAdded', chatMessageAdded);
        cleanableValueSetter.add('chatMessageEdited', chatMessageEdited);
        cleanableValueSetter.add('chatMessageRemoved', chatMessageRemoved);
        cleanableValueSetter.add('chatMessagesRead', chatMessagesRead);

        return () => cleanableValueSetter.clean();
    }

    _register(){
        var chatsReleaser = this._registerChatsNotifications();
        var chatParticipantsReleaser = this._registerChatParticipantsNotifications();
        var chatMessagesReleaser = this._registerChatMessagesNotifications();
        return () => {
            chatMessagesReleaser();
            chatParticipantsReleaser();
            chatsReleaser();
        };
    }
}
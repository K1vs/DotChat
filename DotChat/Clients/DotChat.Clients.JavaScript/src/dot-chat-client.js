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
    ..checkFilter: function(filter, item): bool
    .messagesSettings
    ..maxReaders: number 
    ..maxNamedReaders: number 
    ..pageSize: number
    ..frameSize: number
    ..maxBufferSize: number
    ..minBufferSize: number
    ..keyFunction : function(item):any
    ..sortKeyFunction : function(item):int
    ..checkFilter: function(filter, item): bool
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
        this._getChatReaderBoxes().forEach(r => r.reader.dispose());
        this._getChatsMessageReaderBoxes().forEach(r => r.reader.dispose());
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

    getAuthor(chat, message){
        if(!message.author){
            if(chat){
                message.author = chat.participants.find(r => r.userId === message.authorId);
            }
            if(!message.author){
                return {
                    userId: message.authorId
                };
            }
        }
        return message.author;
    }

    async addChat(chatId, chatInfo, candidates){
        var createdChatId = await this._connector.chats.add(chatId, chatInfo, candidates);
        return createdChatId;
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
        var readerBoxes = this._getChatMessageReaderBoxes(chatId, {messageId : messageId, ...messageInfo});
        var message = {
            ...messageInfo,
            messageId: messageId,
            pending: true,
            messageStatus: MessageStatus.actual,
            index: Number.MAX_SAFE_INTEGER
        };
        readerBoxes.forEach(readerBox => readerBox.reader.addOrUpdate(messageId, message, exist => _.merge(exist, message)));
    }

    async editMessage(chatId, messageId, messageInfo){
        await this._connector.messages.edit(chatId, messageId, messageInfo);
        var readerBoxes = this._getChatMessageReaderBoxes(chatId, {messageId : messageId, ...messageInfo});
        var updateExt = {
            pending: true,
            messageStatus: MessageStatus.edited
        };
        readerBoxes.forEach(readerBox => readerBox.reader.addOrUpdate(messageId, {...messageInfo, ...updateExt}, exist => _.merge(exist, messageInfo, updateExt)));
    }

    async removeMessage(chatId, messageId){
        await this._connector.messages.remove(chatId, messageId);
        var readerBoxes = this._getChatMessageReaderBoxes(chatId, {messageId : messageId});
        var removedExt = {
            pending: true,
            messageStatus: MessageStatus.removed
        };
        readerBoxes.forEach(readerBox => readerBox.reader.addOrUpdate(messageId, {messageId: messageId, ... removedExt}, exist => _.merge(exist, removedExt)));
    }

    async readMessages(chatId, index, force){
        await this._connector.messages.read(chatId, index, force);
        this._getChats(chatId).forEach(chat => {
            var exist = chat.participants.find(r => r.userId === this._userId);
            if(exist && (exist.readIndex < index || force)){
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

    _filterChatReaderBoxes(readerBoxes, chat){
        return chat ? readerBoxes.filter(r => !r.filter || r.reader.get(chat.chatId) || this._settings.chatsSettings.checkFilter && this._settings.chatsSettings.checkFilter(r.filter, chat)): readerBoxes;
    }

    _getChatReaderBoxes(chat){
        var readers = Object.entries(this._chatsReaders.named)
        .map(([, value]) => value);
        if(this._chatsReaders.default){
            readers.push({
                reader: this._chatsReaders.default
            });
        }
        return this._filterChatReaderBoxes(readers, chat);
    }

    _getChatsWithReaderBoxes(chatId){
        var readerBoxes = this._getChatReaderBoxes();
        var chatsWithReaderBoxes = _.orderBy(readerBoxes.map(r => ({ chat: r.reader.get(chatId), readerBox: r })).filter(r => r.chat), 'chat.version', 'desc');
        var refChatWithReaderBox = chatsWithReaderBoxes[0];
        if(refChatWithReaderBox){
            this._filterChatReaderBoxes(readerBoxes, refChatWithReaderBox.chat).filter(r => r != refChatWithReaderBox.reader)
            .forEach(r => r.reader.addOrUpdate(chatId, refChatWithReaderBox.chat, exist => _.assign(exist, refChatWithReaderBox.chat)));
            chatsWithReaderBoxes.forEach(chatWithReaderBox => {
                if(!chatWithReaderBox.chat){
                    chatWithReaderBox.chat = chatWithReaderBox.readerBox.reader.get(chatId);
                }
            });
        }
        return chatsWithReaderBoxes;
    }

    _getChats(chatId){
        return this._getChatsWithReaderBoxes(chatId).map(cwrb => cwrb.chat);
    }

    async _getOrLoadChatsWithReaderBoxes(chatId){
        var chatsWithReaderBoxes = this._getChatsWithReaderBoxes(chatId);
        if(chatsWithReaderBoxes.length === 0){
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
            var readers = this._getChatReaderBoxes(chat);
            readers.forEach(r => {
                r.reader.addOrUpdate(chatId, chat, exist => _.assign(exist, chat));
            });
            chatsWithReaderBoxes = this._getChatsWithReaderBoxes(chatId);
        }
        return chatsWithReaderBoxes;
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
        var forEachReader = (readerAction, chat) => {
            this._getChatReaderBoxes(chat)
            .forEach(chatReaderBox => readerAction(chatReaderBox.reader, chat));
        };
        var chatAdded = (notification) => {
            this._loadSummary();
            forEachReader((reader, chat) => {
                reader.addOrUpdate(chat.chatId, chat, (exist) => _.assign(exist, chat));
            }, notification.personalizedChat);
            this._callCallback('chatAdded', notification);
        };

        var chatInfoEdited = (notification) => {
            this._loadSummary();
            forEachReader((reader, chat) => {
                reader.addOrUpdate(chat.chatId, chat, (exist) => _.assign(exist, chat));
            }, {...notification.chatInfo, chatId: notification.chatId});
            this._callCallback('chatInfoEdited', notification);
        };

        var chatRemoved = (notification) => {
            this._loadSummary();
            forEachReader((reader, chat) => {
                reader.addOrUpdate(chat.chatId, chat, (exist) => exist.removed = true);
            }, {chatId: notification.chatId});
            this._callCallback('chatRemoved', notification);
        };

        var cleanableValueSetter = new CleanableValueSetter(this._connector.chats, null);
        cleanableValueSetter.add('chatAdded', chatAdded);
        cleanableValueSetter.add('chatInfoEdited', chatInfoEdited);
        cleanableValueSetter.add('chatRemoved', chatRemoved);

        return () => cleanableValueSetter.clean();
    }

    _registerChatParticipantsNotifications(){
        var addOrUpdateParticipant = (participant, chatWithReaderBox) => {
            chatWithReaderBox.readerBox.reader.update(chatWithReaderBox.chat.chatId, Number.MAX_SAFE_INTEGER, (chat) => {
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
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantApplied = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantApplied', notification);
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantInvited = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantInvited', notification);
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantTypeChanged = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantTypeChanged', notification);
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };
        
        var chatParticipantRemoved = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantRemoved', notification);
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };
        
        var chatParticipantBlocked = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantBlocked', notification);
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(notification.participant, r));
        };

        var chatParticipantsAppended = async (notification) => {
            this._loadSummary();
            this._callCallback('chatParticipantsAppended', notification);
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(notification.chatId);
            notification.added.concat(notification.invited).forEach(participant => {
                chatsWithReaderBoxes.forEach(r => addOrUpdateParticipant(participant, r));
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

    _getChatsMessageReaderBoxes(){
        return Object.getOwnPropertyNames(this._messagesReaders).map(chatId => this._getChatMessageReaderBoxes(chatId)).flat();
    }

    _getChatMessageReaderBoxes(chatId, message){
        var chatMessagesReaderBoxes = this._messagesReaders[chatId];
        if(chatMessagesReaderBoxes){
            var readers = Object.entries(chatMessagesReaderBoxes.named).map(([, value]) => value)
            .map(r => r.reader);
            if(chatMessagesReaderBoxes.default){
                readers.push({
                    reader: chatMessagesReaderBoxes.default
                });
            }
            return readers.filter(r => !r.filter || !message || r.reader.get(message.messageId) || this._settings.messagesSettings.checkFilter && this._settings.messagesSettings.checkFilter(r.filter, message));
        }else{
            return [];
        }
    }

    async _updateChatLasts(chatId, message, self){
        var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(chatId);
        chatsWithReaderBoxes.forEach((chatWithReaderBox) => {
            chatWithReaderBox.readerBox.reader.update(chatWithReaderBox.chat.chatId, Number.MAX_SAFE_INTEGER, (chat) => {
                if(chat.topIndex <= message.index){
                    chat.lastTimestamp = message.timestamp;
                    chat.topIndex = message.index;
                    chat.lastChatMessageId = message.messageId;
                    chat.lastMessageAuthorId = message.authorId;
                    chat.lastChatMessageInfo = message;
                    var thisParticipant = chat.participants.find(r => r.userId === this._userId);
                    if(self && thisParticipant.readIndex < message.index){
                        thisParticipant.readIndex = message.index; 
                        this.readMessages(chatId, message.index, false);
                    }
                    this._setChatPersonalization(chat, thisParticipant);
                }
            });
        });
    }

    _setChatPersonalization(chat, thisParticipant){
        chat.readIndex = thisParticipant.readIndex;
        chat.unreadCount = Math.max(chat.topIndex - thisParticipant.readIndex, 0);
    }

    _registerChatMessagesNotifications(){
        var update = (exist, fromNotification) => _.assign(exist, fromNotification, {pending: false});
        var read = async (chatId, userId, index, force) => {
            var chatsWithReaderBoxes = await this._getOrLoadChatsWithReaderBoxes(chatId);
            chatsWithReaderBoxes.forEach((chatWithReaderBox) => {
                chatWithReaderBox.readerBox.reader.update(chatWithReaderBox.chat.chatId, Number.MAX_SAFE_INTEGER, (chat) =>{
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
        var addOrUpdateMessage = (chatId, message, initiatorUserId) => {
            this._updateChatLasts(chatId, message, initiatorUserId === this._userId);
            if(initiatorUserId !== this._userId){
                this._loadSummary();
            }
            var readers = this._getChatMessageReaderBoxes(chatId, message);
            readers.forEach(readerBox => {
                readerBox.reader.addOrUpdate(message.messageId, message, exist => update(exist, message));
            });
        };
        var chatMessageAdded = (notification) => {
            addOrUpdateMessage(notification.chatId, notification.message, notification.initiatorUserId);
            this._callCallback('chatMessageAdded', notification);
        };
        var chatMessageEdited = (notification) => {
            addOrUpdateMessage(notification.chatId, notification.message, notification.initiatorUserId);
            this._callCallback('chatMessageEdited', notification);
        };
        var chatMessageRemoved = (notification) => {
            addOrUpdateMessage(notification.chatId, notification.message, notification.initiatorUserId);
            this._callCallback('chatMessageRemoved', notification);
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
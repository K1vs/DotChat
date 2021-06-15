import { createChatsConnector } from './chats.js';
import { createParticipantsConnector } from './participants.js';
import { createMessagesConnector } from './messages.js';
import { createDefaultOptions } from './default-options.js';

export default class DotChatSignalRConnector{
    constructor(options) {
        var defaultOptions = createDefaultOptions();
        this._options = options ? Object.assign(options, defaultOptions) : defaultOptions;
        this._chats = createChatsConnector(this._options);
        this._participants = createParticipantsConnector(this._options);
        this._messages = createMessagesConnector(this._options);
    }

    get chats(){
        return this._chats.proxy;
    }
    
    get participants(){
        return this._participants.proxy;
    }

    get messages(){
        return this._messages.proxy;
    }

    start() {
        var chatsStating = this._chats.start();
        var participantsStating = this._participants.start();
        var messagesStating = this._messages.start();
        return Promise.all([chatsStating, participantsStating, messagesStating]);
    }
}
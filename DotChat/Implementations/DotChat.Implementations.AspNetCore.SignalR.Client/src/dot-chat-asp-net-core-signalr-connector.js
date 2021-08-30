import { createChatsConnector } from './chats.js';
import { createParticipantsConnector } from './participants.js';
import { createMessagesConnector } from './messages.js';
import { createDefaultOptions } from './default-options.js';

export default class DotChatAspNetCoreSignalRConnector{
    constructor(optionsConfigurator) {
        const defaultOptions = createDefaultOptions();
        if (optionsConfigurator) {
            this._options = optionsConfigurator(defaultOptions);
        } else {
            this._options = defaultOptions;
        }
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
        if (!this._startingPromise) {
            const chatsStating = this._chats.start();
            const participantsStating = this._participants.start();
            const messagesStating = this._messages.start();
            this._startingPromise = Promise.all([chatsStating, participantsStating, messagesStating]);
        }
        return this._startingPromise;
    }
}
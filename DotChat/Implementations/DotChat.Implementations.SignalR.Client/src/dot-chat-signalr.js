import {createChatsConnector} from './chats.js';
import {createParticipantsConnector} from './participants.js';
import {createMessagesConnector} from './messages.js';

export default class DotChatSignalRConnector{
    constructor(connection, chatsHubName, participantsHubName, messagesHubName){
        this._chats = createChatsConnector(connection, chatsHubName);
        this._participants = createParticipantsConnector(connection, participantsHubName);
        this._messages = createMessagesConnector(connection, messagesHubName);
    }

    get chats(){
        return this._chats;
    }
    
    get participants(){
        return this._participants;
    }

    get messages(){
        return this._messages;
    }
}
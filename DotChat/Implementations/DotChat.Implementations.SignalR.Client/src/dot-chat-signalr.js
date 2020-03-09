import {createChatsConnector} from './chats.js';
import {createParticipantsConnector} from './participants.js';
import {createMessagesConnector} from './messages.js';

export let createConnector = function(connection, chatsHubName, participantsHubName, messagesHubName){
    return {
        chats: createChatsConnector(connection, chatsHubName),
        participants: createParticipantsConnector(connection, participantsHubName),
        messages: createMessagesConnector(connection, messagesHubName)
    };
};
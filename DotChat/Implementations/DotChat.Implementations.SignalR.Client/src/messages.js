import {createProxyHelper} from './helper.js';
export let createMessagesConnector = function(connection, messagesHubName){
    var messagesHubProxy = connection.createHubProxy(messagesHubName || 'messagesHub');
    var result = {};
    var helper = createProxyHelper(messagesHubProxy, result);

    helper.addMethod('getPage');
    helper.addMethod('read');
    helper.addMethod('add');
    helper.addMethod('edit');
    helper.addMethod('remove');

    helper.addHandler('chatMessageAdded');
    helper.addHandler('chatMessageEdited');
    helper.addHandler('chatMessageRemoved');
    helper.addHandler('chatMessagesRead');

    return result;
};
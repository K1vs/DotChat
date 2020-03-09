import {createProxyHelper} from './helper.js';
export let createChatsConnector = function(connection, chatsHubName){
    var chatsHubProxy = connection.createHubProxy(chatsHubName || 'chatsHub');
    var result = {};
    var helper = createProxyHelper(chatsHubProxy, result);

    helper.addMethod('getSummary');
    helper.addMethod('getPage');
    helper.addMethod('get');
    helper.addMethod('add');
    helper.addMethod('editInfo');
    helper.addMethod('remove');

    helper.addHandler('chatAdded');
    helper.addHandler('chatInfoEdited');
    helper.addHandler('chatRemoved');

    return result;
};
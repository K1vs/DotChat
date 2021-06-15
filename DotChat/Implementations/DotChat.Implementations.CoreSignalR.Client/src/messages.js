import { createProxyBuilder } from './proxy-builder.js';
export let createMessagesConnector = function (options) {

    var builder = createProxyBuilder(options, 'chatMessagesHub');

    builder.addMethod('getPage');
    builder.addMethod('read');
    builder.addMethod('add');
    builder.addMethod('edit');
    builder.addMethod('remove');
    
    builder.addHandler('chatMessageAdded');
    builder.addHandler('chatMessageEdited');
    builder.addHandler('chatMessageRemoved');
    builder.addHandler('chatMessagesRead');

    return builder;
};
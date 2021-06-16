import { createProxyBuilder } from './proxy-builder.js';
export let createMessagesConnector = function (options) {

    const builder = createProxyBuilder(options, 'chatMessagesHub');

    builder.addMethod('getPage', function (args) {
        if (args.length === 2) {
            args.splice(1, 0, []);
        }
        return args;
    });
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
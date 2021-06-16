import { createProxyBuilder} from './proxy-builder.js';
export let createChatsConnector = function (options) {

    const builder = createProxyBuilder(options, 'chatsHub');

    builder.addMethod('getSummary');
    builder.addMethod('getPage');
    builder.addMethod('get');
    builder.addMethod('add');
    builder.addMethod('editInfo');
    builder.addMethod('remove');

    builder.addHandler('chatAdded');
    builder.addHandler('chatInfoEdited');
    builder.addHandler('chatRemoved');

    return builder;
};
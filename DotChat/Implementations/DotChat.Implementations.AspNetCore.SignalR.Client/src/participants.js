import { createProxyBuilder } from './proxy-builder.js';
export let createParticipantsConnector = function (options) {

    const builder = createProxyBuilder(options, 'chatParticipantsHub');

    builder.addMethod('add');
    builder.addMethod('invite');
    builder.addMethod('apply');
    builder.addMethod('remove');
    builder.addMethod('block');
    builder.addMethod('changeType');
    builder.addMethod('append');
    
    builder.addHandler('chatParticipantAdded');
    builder.addHandler('chatParticipantApplied');
    builder.addHandler('chatParticipantInvited');
    builder.addHandler('chatParticipantRemoved');
    builder.addHandler('chatParticipantBlocked');
    builder.addHandler('chatParticipantsAppended');
    builder.addHandler('chatParticipantTypeChanged');

    return builder;
};
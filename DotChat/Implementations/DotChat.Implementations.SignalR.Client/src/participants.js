import {createProxyHelper} from './helper.js';
export let createParticipantsConnector = function(connection, participantsHubName){
    var participantsHubProxy = connection.createHubProxy(participantsHubName || 'chatParticipantsHub');
    var result = {};
    var helper = createProxyHelper(participantsHubProxy, result);

    helper.addMethod('add');
    helper.addMethod('invite');
    helper.addMethod('apply');
    helper.addMethod('remove');
    helper.addMethod('block');
    helper.addMethod('changeType');
    helper.addMethod('append');

    helper.addHandler('chatParticipantAdded');
    helper.addHandler('chatParticipantApplied');
    helper.addHandler('chatParticipantInvited');
    helper.addHandler('chatParticipantRemoved');
    helper.addHandler('chatParticipantBlocked');
    helper.addHandler('chatParticipantsAppended');
    helper.addHandler('chatParticipantTypeChanged');

    return result;
};
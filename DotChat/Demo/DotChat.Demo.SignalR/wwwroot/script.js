var connection = $.hubConnection();
var connector = new DotChatSignalRConnector(connection);
var dotChatClient = new DotChatClient('userId', connector);
connection.start().done(function(){
    return dotChatClient.init();
}).done(function(){
    alert(dotChatClient.summary);
});
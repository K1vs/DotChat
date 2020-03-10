var connection = $.hubConnection();
connection.logging = true;
var connector = new DotChatSignalRConnector(connection);
var dotChatClient = new DotChatClient('99C10789-D258-4093-93ED-7DE74A81E3FA', connector);
connection.start().done(function(){
    return dotChatClient.init().then(function(){
        alert(dotChatClient.summary)
    }, function(data){
        alert(data);
    });
});
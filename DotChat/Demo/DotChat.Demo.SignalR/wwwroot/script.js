$( document ).ready(function() {
    var setChats = function(chats){
        if(!chats || chats.length === 0){
            return;
        }
        if(!activeChat){
            activateChat(chats[0]);
        }
        var container = $('.chats-box');
        container.empty();
        for(chatIndex in chats){
            var chatElem = $("<p></p>").text(chats[chatIndex].name);
            if(chats[chatIndex].chatId === activeChat.chatId){
                chatElem.addClass("active");
            }
            container.append(chatElem);
        }
    };

    var activateChat = function(chat){
        activeChat = chat;
        var setMessages = function(){
            
        };
        //var messagesReader = dotChatClient.getMessagesReader(setMessages);
    };

    var connection = $.hubConnection();
    connection.qs = { 'userId': '99C10789-D258-4093-93ED-7DE74A81E3FA' };
    connection.logging = true;
    var connector = new DotChatSignalRConnector(connection);
    var dotChatClient = new DotChatClient('99C10789-D258-4093-93ED-7DE74A81E3FA', connector);
    
    var reader = dotChatClient.getChatsReader();
    reader.aquire(setChats);

    var activeChat = null;
    $('.send-button').click(function(){
        if(!activeChat){
            alert('Select chat');
            return;
        }
        var text = $('.input-box').val();
        dotChatClient.addMessage(activeChat.chatId,{
            type: 1,
            text: {
                content: text
            }
        }).then(function(){
            $('.input-box').val('');
        }, function(){
            alert('sendError');
        });
    });
    
    
    
    
    connection.start().then(function(){
        return dotChatClient.init().then(function(){
            alert(JSON.stringify(dotChatClient.summary));
            return reader.open().then(function(){
                setChats(reader.current);
            });
        });
    }).catch(function(){
        alert(JSON.stringify(arguments));
    });
});








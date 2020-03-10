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
            var chatElem = $("<p></p>").attr('id', chats[chatIndex].chatId).addClass('chat').text(chats[chatIndex].name);
            if(chats[chatIndex].chatId === activeChat.chatId){
                chatElem.addClass("active");
            }
            container.append(chatElem);
        }
        $('.chat').click(function(){
            var id = $(this).attr('id');
            var chat = reader.get(id);
            activateChat(chat);
            setChats(chats);
        });
    };

    var activateChat = function(chat){
        if(activeMessageReaderReleaser){
            activeMessageReaderReleaser();
        }
        activeChat = chat;
        var setMessages = function(messages){
            var container = $('.messages-box');
            container.empty();
            for(messageIndex in messages){
                var message = messages[messageIndex];
                if(message.type === 1){
                    var msgElem = $("<div></div>").text(message.text.content);
                    if(message.pending){
                        msgElem.addClass("active");
                    }
                    container.append(msgElem);
                }
            }
            container.animate({scrollTop: container.height()}, 500);
        };
        var messagesReader = dotChatClient.getMessagesReader(chat.chatId);
        activeMessageReaderReleaser = messagesReader.aquire(setMessages);
        messagesReader.open().then(function(){
            setMessages(messagesReader.current);
        }, function(){
            alert(JSON.stringify(arguments));
        });
    };

    var connection = $.hubConnection();
    connection.qs = { 'userId': '99C10789-D258-4093-93ED-7DE74A81E3FA' };
    connection.logging = true;
    var connector = new DotChatSignalRConnector(connection);
    var dotChatClient = new DotChatClient('99C10789-D258-4093-93ED-7DE74A81E3FA', connector);
    
    var reader = dotChatClient.getChatsReader();
    reader.aquire(setChats);

    var activeChat = null;
    var activeMessageReaderReleaser = null;
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
    
    $()
    
    
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








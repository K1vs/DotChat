$(document).ready(function(){
    $('.user-select').unbind();
    $('.user-select').change(function() {
        var userId = this.value;
        if(!userId){
            return;
        }
    
        var setSummary = function(summary){
            $('.summary').text('Unread chats: ' + summary.unreadChatsCount + ' Unread messages: ' + summary.unreadMessagesCount);
        }

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
                var chatElem = $("<p></p>")
                    .attr('id', chats[chatIndex].chatId)
                    .addClass('chat')
                    .text(chats[chatIndex].unreadCount + ': ' + chats[chatIndex].name);
                if(chats[chatIndex].chatId === activeChat.chatId){
                    chatElem.addClass("active");
                }
                container.append(chatElem);
            }
            $('.chat').unbind();
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
                        var msgElem = $("<div></div>")
                            .text(message.text.content)
                            .attr('index', message.index)
                            .click(function(){
                                dotChatClient.readMessages(chat.chatId, $(this).attr('index'));
                            });
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
        connection.stop();
        connection.qs = { 'userId': userId };
        connection.logging = true;

        var connector = new DotChatSignalRConnector(connection);

        if (window.dotChatClient) {
            window.dotChatClient.dispose();
        }
        var dotChatClient = new DotChatClient(userId, connector, null, {onSummaryChanged: setSummary});
        window.dotChatClient = dotChatClient;
        
        var reader = dotChatClient.getChatsReader('tt');
        reader.aquire(setChats);
    
        var activeChat = null;
        var activeMessageReaderReleaser = null;
        $('.send-button').unbind();
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
            return dotChatClient.ready().then(function(client){
                client.loadSummary().then(setSummary);
                return reader.open().then(function(){
                    setChats(reader.current);
                });
            });
        }).catch(function(){
            alert(JSON.stringify(arguments));
        });
    });
});
$('.user-select').change(function() {
    var userId = this.value;
    if(!userId){
        return;
    }

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
    connection.qs = { 'userId': userId };
    connection.logging = true;
    var connector = new DotChatSignalRConnector(connection);
    var dotChatClient = new DotChatClient(userId, connector);
    
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
            return reader.open().then(function(){
                setChats(reader.current);
            });
        });
    }).catch(function(){
        alert(JSON.stringify(arguments));
    });
});








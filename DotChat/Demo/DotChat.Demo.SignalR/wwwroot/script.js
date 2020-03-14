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
            if(!chats){
                return;
            }
            if(!activeChat){
                activateChat(chats[0]);
            }else{
                activateChat(activeChat);
            }
            var container = $('.chats-box');
            container.empty();
            for(chatIndex in chats){
                var chatElem = $("<p></p>")
                    .attr('id', chats[chatIndex].chatId)
                    .addClass('chat')
                    .text(chats[chatIndex].unreadCount + ': ' + chats[chatIndex].name + (chats[chatIndex].lost ? 'Lost' : ''));
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
                                dotChatClient.readMessages(chat.chatId, $(this).attr('index'), true);
                            });
                        if(message.pending){
                            msgElem.addClass("active");
                        }
                        container.append(msgElem);
                    }
                }
                container.animate({scrollTop: container.height()}, 500);
            };
            var participantsStr = activeChat ? activeChat.participants.filter(function(item){
                return item.chatParticipantStatus === 0;
            }).reduce(function(acc, item){
                return acc + ' ' + item.name;
            }, "Participants: ") : "";
            $('.participants').text(participantsStr);
            if(!activeChat){
                setMessages([]);
                return;
            }
            var messagesReader = dotChatClient.getMessagesReader(chat.chatId);
            activeMessageReaderReleaser = messagesReader.aquire(setMessages);
            messagesReader.open().then(function(){
                setMessages(messagesReader.current);
            }, function(){
                alert(JSON.stringify(arguments));
            });
        };
    
        setChats([]);

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
        
        $('.add-btn').click(function(){
            var userId = $('.user-select-add').val();
            dotChatClient.addParticipant(activeChat.chatId, userId, 2, null, null).then(function(){
                alert('added');
            });
        });
    
        $('.remove-btn').click(function(){
            var userId = $('.user-select-add').val();
            dotChatClient.removeParticipant(activeChat.chatId, userId).then(function(){
                alert('removed');
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








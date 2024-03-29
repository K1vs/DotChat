﻿$(document).ready(function(){
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
                var last = chats[chatIndex].lastChatMessageInfo && chats[chatIndex].lastChatMessageInfo.text && chats[chatIndex].lastChatMessageInfo.text.content;
                var chatElem = $("<div></div>")
                    .attr('id', chats[chatIndex].chatId)
                    .addClass('chat')
                    .append($('<div></div>').text(chats[chatIndex].unreadCount + ': ' + chats[chatIndex].name + (chats[chatIndex].lost ? 'Lost' : '')))
                    .append($('<div></div>').text(chats[chatIndex].lastTimestamp + ': ' + (last || '')));
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
                            .text(dotChatClient.getAuthor(chat, message).name + ' ' + message.timestamp + ': ' + message.text.content)
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

        var connector = new DotChatAspNetCoreSignalRConnector(function (options) {
            options.withUrlOptionsProvider = function () {
                return {
                    accessTokenFactory: function () {
                        return userId;
                    }
                };
            };
            options.hubNames = {
                chatsHub: 'https://localhost:5001/chatsHub',
                chatParticipantsHub: 'https://localhost:5001/chatParticipantsHub',
                chatMessagesHub: 'https://localhost:5001/chatMessagesHub'
            };
            options.configureConnectionBuilder = function (builder) {
                return builder.configureLogging(signalR.LogLevel.Debug);
            };
            return options;
        });

        if (window.dotChatClient) {
            window.dotChatClient.dispose();
        }
        var dotChatClient = new DotChatClient(userId, connector, { chatsSettings:{
            checkFilter: function(filter, chatItem){
                return chatItem.participants.some(function(p){
                    return p.userId === userId && p.chatParticipantStatus === 0;
                })
            }
        } }, {onSummaryChanged: setSummary});
        window.dotChatClient = dotChatClient;
        
        var chatFilter = {
            userFiltersList: [
                {
                    userId: userId,
                    participantStatus: 0
                }
            ]
        };
        var reader = dotChatClient.getChatsReader('tt', chatFilter);
        //var reader = dotChatClient.getChatsReader();
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
            }, function(error){
                console.error(error);
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

        dotChatClient.ready().then(function (client) {
            client.loadSummary().then(setSummary);
            return reader.open().then(function (current) {
                setChats(current);
            });
        }, function () {
            alert(JSON.stringify(arguments));
        });
    });
});








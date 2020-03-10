export const chatsSettings = {
    maxNamedReaders: 5, 
    pageSize: 200,
    frameSize: 100,
    maxBufferSize: 1000,
    minBufferSize: 500,
    keyFunction: (item) => item.chatId,
    sortKeyFunction: (item) => item.lastTimestamp
};
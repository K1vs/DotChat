export const chatsSettings = {
    maxReaders: 10, 
    pageSize: 200,
    frameSize: 200,
    maxBufferSize: 2000,
    minBufferSize: 1000,
    loadSummaryDelay: 5000,
    keyFunction: (item) => item.chatId,
    sortKeyFunction: (item) => item.lastTimestamp,
    reverseFrame: false
};
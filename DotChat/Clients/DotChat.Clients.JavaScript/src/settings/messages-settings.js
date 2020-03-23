export const messagesSettings = {
    maxReaders: 20, 
    pageSize: 200,
    frameSize: 200,
    maxBufferSize: 10000,
    minBufferSize: 5000,
    keyFunction: (item) => item.messageId,
    sortKeyFunction: (item) => item.index,
    reverseFrame: true
};
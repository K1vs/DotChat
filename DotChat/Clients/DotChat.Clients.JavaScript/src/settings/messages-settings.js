export const messagesSettings = {
    maxReaders: 20, 
    maxNamedReaders: 5, 
    pageSize: 200,
    frameSize: 100,
    maxBufferSize: 10000,
    minBufferSize: 5000,
    keyFunction: (item) => item.messageId,
    sortKeyFunction: (item) => item.index,
    reverseFrame: true
};
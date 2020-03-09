export const MessageType = {
    none: 0,
    text: 1 << 0,
    quote: 1 << 1,
    attachment: 1 << 2,
    chatRef: 1 << 3,
    contact: 1 << 4,
    custom: 1 << 16
}
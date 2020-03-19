namespace K1vs.DotChat.Common.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class ErrorInfo
    {
        public ErrorInfo(ErrorCode errorCode, string message, Guid? chatId, Guid? userId, Dictionary<string, object> data)
        {
            TraceId = Guid.NewGuid();
            ErrorCode = errorCode;
            Message = $"[{errorCode.Code}] {message}";
            ChatId = chatId;
            UserId = userId;
            Data = data;
        }

        public Guid TraceId { get; set; }
        public ErrorCode ErrorCode { get; }
        public string Message { get; }
        public Guid? UserId { get; }
        public Guid? ChatId { get; }
        public Dictionary<string, object> Data { get; }
    }
}

using K1vs.DotChat.FrameworkUtils.Attributes;
using K1vs.DotChat.FrameworkUtils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K1vs.DotChat.Exceptions
{
    public class DotChatException: Exception
    {
        public int Code { get; }

        public string Name { get; }

        public string Description { get; }


        public DotChatException(ExceptionCode code, object additionalData = null, Exception innerException = null)
            : this(code, additionalData.ConvertToDictionary(), innerException)
        {

        }

        public DotChatException(ExceptionCode code, IDictionary<string, object> additionalData = null, Exception innerException = null)
            : this((int)code, code.GetCustomAttribute<NameDescriptionAttribute>()?.Name, code.GetCustomAttribute<NameDescriptionAttribute>()?.Description, additionalData, innerException)
        {

        }

        public DotChatException(int code, string name, string description, IDictionary<string, object> additionalData = null, Exception innerException = null)
            : base(GetMessage(code, name, description, additionalData), innerException)
        {
            Code = code;
            Name = name;
            Description = description;
            if(additionalData != null)
            {
                foreach (var item in additionalData)
                {
                    Data.Add(item.Key, item.Value);
                }
            }
        }

        private static string GetMessage(int code, string name, string description, IDictionary<string, object> additionalData)
        {
            var additionalDataString = additionalData == null ? string.Empty : $" Additional data: {string.Join(",", additionalData.Select(r => $"{r.Key}:{r.Value}"))}.";
            return $"DotChat exception: {code} - {name}. {description}.{additionalDataString}";
        }
    }
}

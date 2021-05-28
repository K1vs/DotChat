namespace K1vs.DotChat.Models.Chats
{
    using DotChat.Chats;
    using System;
    using System.Collections.Generic;

    public class ChatInfo: IChatInfo
    {
        public ChatInfo()
        {
        }

        public ChatInfo(string name, string description, ChatPrivacyMode privacyMode, long version, IReadOnlyList<string> styles = null)
        {
            Name = name;
            Description = description;
            PrivacyMode = privacyMode;
            Version = version;
            Styles = styles;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ChatPrivacyMode PrivacyMode { get; set; }
        public long Version { get; set; }
        public Guid ImageId { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}

namespace K1vs.DotChat.Models.Chats
{
    using DotChat.Chats;

    public class ChatInfo: IChatInfo
    {
        public ChatInfo()
        {
        }

        public ChatInfo(string name, string description, ChatPrivacyMode privacyMode, long version, string style = null, string metadata = null)
        {
            Name = name;
            Description = description;
            PrivacyMode = privacyMode;
            Version = version;
            Style = style;
            Metadata = metadata;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ChatPrivacyMode PrivacyMode { get; set; }
        public long Version { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}

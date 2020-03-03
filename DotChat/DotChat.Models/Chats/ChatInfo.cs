namespace K1vs.DotChat.Models.Chats
{
    using DotChat.Chats;

    public class ChatInfo: IChatInfo
    {
        public ChatInfo()
        {
        }

        public ChatInfo(string name, string description, ChatPrivacyMode privacyMode, string style = null, string metadata = null)
        {
            Name = name;
            Description = description;
            PrivacyMode = privacyMode;
            Style = style;
            Metadata = metadata;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ChatPrivacyMode PrivacyMode { get; set; }
        public string Style { get; }
        public string Metadata { get; }
    }
}

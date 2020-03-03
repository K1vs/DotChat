namespace K1vs.DotChat.Basic.Configuration
{
    using DotChat.Configuration;

    public class ChatWorkersConfiguration: IChatWorkersConfiguration
    {
        public ChatWorkersConfiguration()
        {
        }

        public ChatWorkersConfiguration(bool fastMessageMode, bool disableSystemMessages)
        {
            FastMessageMode = fastMessageMode;
            DisableSystemMessages = disableSystemMessages;
        }

        public bool FastMessageMode { get; set; }
        public bool DisableSystemMessages { get; set; }
    }
}

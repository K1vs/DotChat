namespace K1vs.DotChat.Basic.Configuration
{
    using System;
    using DotChat.Configuration;

    public class ChatNotificationsConfiguration: IChatNotificationsConfiguration
    {
        public ChatNotificationsConfiguration()
        {
        }

        public ChatNotificationsConfiguration(TimeSpan cleanUpInterval)
        {
            CleanUpInterval = cleanUpInterval;
        }

        public TimeSpan CleanUpInterval { get; set; }
    }
}

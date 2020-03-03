namespace K1vs.DotChat.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotificationBase: INotificationBase
    {
        public NotificationBase()
        {
        }

        public NotificationBase(Guid initiatorUserId)
        {
            InitiatorUserId = initiatorUserId;
        }

        public Guid InitiatorUserId { get; set; }
    }
}

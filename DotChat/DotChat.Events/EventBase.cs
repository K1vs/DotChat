namespace K1vs.DotChat.Events
{
    using System;

    public class EventBase: IEventBase
    {
        public EventBase()
        {
        }

        public EventBase(Guid initiatorUserId)
        {
            InitiatorUserId = initiatorUserId;
        }

        public Guid InitiatorUserId { get; set; }
    }
}

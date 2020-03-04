namespace K1vs.DotChat.Events
{
    using System;

    public class Event: IEvent
    {
        public Event()
        {
        }

        public Event(Guid initiatorUserId)
        {
            InitiatorUserId = initiatorUserId;
        }

        public Guid InitiatorUserId { get; set; }
    }
}

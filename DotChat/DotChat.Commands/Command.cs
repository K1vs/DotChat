namespace K1vs.DotChat.Commands
{
    using System;

    public class Command: ICommand
    {
        public Command()
        {
        }

        public Command(Guid initiatorUserId)
        {
            InitiatorUserId = initiatorUserId;
        }

        public Guid InitiatorUserId { get; set; }
    }
}

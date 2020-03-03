namespace K1vs.DotChat.Commands
{
    using System;

    public class CommandBase: ICommandBase
    {
        public CommandBase()
        {
        }

        public CommandBase(Guid initiatorUserId)
        {
            InitiatorUserId = initiatorUserId;
        }

        public Guid InitiatorUserId { get; set; }
    }
}

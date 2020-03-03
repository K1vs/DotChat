namespace K1vs.DotChat.Common
{
    using System;

    public interface IHasInitiator
    {
        Guid InitiatorUserId { get; }
    }
}

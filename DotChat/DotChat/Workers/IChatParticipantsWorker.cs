namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using Commands.Participants;
    using Handlers;
    using Participants;

    public interface IChatParticipantsWorker:
        IHandleCommand<IAddChatParticipantCommand>,
        IHandleCommand<IApplyToChatCommand>,
        IHandleCommand<IInviteChatParticipantCommand>,
        IHandleCommand<IRemoveChatParticipantCommand>,
        IHandleCommand<IBlockChatParticipantCommand>,
        IHandleCommand<IChangeChatParticipantTypeCommand>,
        IHandleCommand<IBulkAddInviteChatParticipantsCommand>
    {
    }
}

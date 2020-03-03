namespace K1vs.DotChat.Workers
{
    using System.Collections.Generic;
    using Commands.Participants;
    using Handlers;
    using Participants;

    public interface IChatParticipantsWorker<in TParticipationCandidateCollection, in TParticipationCandidate> :
        IHandleCommand<IAddChatParticipantCommand>,
        IHandleCommand<IApplyToChatCommand>,
        IHandleCommand<IInviteChatParticipantCommand>,
        IHandleCommand<IRemoveChatParticipantCommand>,
        IHandleCommand<IBlockChatParticipantCommand>,
        IHandleCommand<IChangeChatParticipantTypeCommand>,
        IHandleCommand<IAppendChatParticipantsCommand<TParticipationCandidateCollection, TParticipationCandidate>>
        where TParticipationCandidateCollection : IReadOnlyCollection<TParticipationCandidate>
        where TParticipationCandidate : IParticipationCandidate
    {
    }
}

namespace K1vs.DotChat.Stores.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;

    public interface IReadChatParticipantStore<TChatParticipant>
        where TChatParticipant : IChatParticipant
    {
        Task<IReadOnlyCollection<Guid>> RetrieveIds(Guid chatId);
        Task<IReadOnlyCollection<TChatParticipant>> RetrieveList(Guid chatId, IEnumerable<Guid> participantsIds);
        Task<TChatParticipant> Retrieve(Guid chatId, Guid userId);
    }
}

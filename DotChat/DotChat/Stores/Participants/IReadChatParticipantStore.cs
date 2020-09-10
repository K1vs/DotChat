namespace K1vs.DotChat.Stores.Participants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DotChat.Participants;

    public interface IReadChatParticipantStore
    {
        Task<IReadOnlyCollection<Guid>> RetrieveIds(Guid chatId);
        Task<IReadOnlyCollection<IChatParticipant>> RetrieveList(Guid chatId, IEnumerable<Guid> participantsIds);
        Task<IChatParticipant> Retrieve(Guid chatId, Guid userId);
    }
}

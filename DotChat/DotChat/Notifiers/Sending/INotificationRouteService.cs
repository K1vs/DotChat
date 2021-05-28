namespace K1vs.DotChat.Notifiers.Sending
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationRouteService
    { 
        Task<IEnumerable<Guid>> GetChatUsers(Guid chatId);

        Task ConnectUser(Guid userId, IEnumerable<Guid> chats);
        Task DisconnectUser(Guid userId);

        Task AddUserToChat(Guid userId, Guid chatId);
        Task AddUsersToChat(IEnumerable<Guid> userIds, Guid chatId);

        Task RemoveUserFromChat(Guid userId, Guid chatId);
        Task RemoveUsersFromChat(IEnumerable<Guid> userIds, Guid chatId);

        Task RemoveChat(Guid chatId);
    }

}

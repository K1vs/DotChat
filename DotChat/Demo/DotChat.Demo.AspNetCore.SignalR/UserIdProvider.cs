namespace K1vs.DotChat.Demo.AspNetCore.SignalR
{
    using Microsoft.AspNetCore.SignalR;

    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Identity.Name;
        }
    }
}
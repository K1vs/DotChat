namespace K1vs.DotChat.Demo.SignalR
{
    using Microsoft.AspNet.SignalR;

    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.User.Identity.Name;
        }
    }
}
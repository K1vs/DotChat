namespace K1vs.DotChat.Models.Users
{
    using System;
    using System.Collections.Generic;
    using DotChat.Participants;
    using K1vs.DotChat.Users;

    public class ChatUser: IChatUser
    {
        public ChatUser()
        {
        }

        public ChatUser(Guid userId, string name, string details = null, Guid? avatarId = null, bool inviteOnly = false, bool canCreateChat = true, IReadOnlyList<string> styles = null)
        {
            UserId = userId;
            Name = name;
            Details = details;
            AvatarId = avatarId;
            InviteOnly = inviteOnly;
            CanCreateChat = canCreateChat;
            Styles = styles;
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public Guid? AvatarId { get; set; }
        public bool InviteOnly { get; set; }
        public bool CanCreateChat { get; set; }
        public long Version { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}

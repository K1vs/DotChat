namespace K1vs.DotChat.Models.Participants
{
    using System;
    using DotChat.Participants;

    public class ChatUser: IChatUser
    {
        public ChatUser()
        {
        }

        public ChatUser(Guid userId, string name, string details = null, Guid? avatarId = null, bool inviteOnly = false, bool canCreateChat = true, string style = null, string metadata = null)
        {
            UserId = userId;
            Name = name;
            Details = details;
            AvatarId = avatarId;
            InviteOnly = inviteOnly;
            CanCreateChat = canCreateChat;
            Style = style;
            Metadata = metadata;
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public Guid? AvatarId { get; set; }
        public bool InviteOnly { get; set; }
        public bool CanCreateChat { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}

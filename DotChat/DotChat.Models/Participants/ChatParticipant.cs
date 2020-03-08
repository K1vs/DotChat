namespace K1vs.DotChat.Models.Participants
{
    using System;
    using DotChat.Participants;

    public class ChatParticipant: ChatUser, IChatParticipant
    {
        public ChatParticipant()
        {
        }

        public ChatParticipant(ChatParticipantType chatParticipantType, ChatParticipantStatus chatParticipantStatus, DateTime readTimestamp, long readIndex, long version, Guid userId, string name, string details, Guid? avatarId = null, bool inviteOnly = false, bool canCreateChat = true, string style = null, string metadata = null) 
            : base(userId, name, details, avatarId, inviteOnly, canCreateChat, style, metadata)
        {
            ChatParticipantType = chatParticipantType;
            ChatParticipantStatus = chatParticipantStatus;
            ReadTimestamp = readTimestamp;
            ReadIndex = readIndex;
            Version = version;
        }

        public ChatParticipant(ChatParticipantType chatParticipantType, ChatParticipantStatus chatParticipantStatus, DateTime readTimestamp, long readIndex, long version, IChatUser user)
            : this(chatParticipantType, chatParticipantStatus, readTimestamp, readIndex, version, user.UserId, user.Name, user.Details, user.AvatarId, user.InviteOnly, user.CanCreateChat, user.Style, user.Metadata)
        {
        }

        public ChatParticipantType ChatParticipantType { get; set; }
        public ChatParticipantStatus ChatParticipantStatus { get; set; }
        public DateTime ReadTimestamp { get; set; }
        public long ReadIndex { get; set; }
        public long Version { get; set; }
    }
}

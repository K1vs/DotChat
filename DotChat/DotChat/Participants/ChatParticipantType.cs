namespace K1vs.DotChat.Participants
{

    public enum ChatParticipantType
    {
        /// <summary>
        /// Can only view messages
        /// </summary>
        ReadOnlyParticipant = 0,
        /// <summary>
        /// Can read and write messages
        /// </summary>
        MessagingOnlyParticipant = 1,
        /// <summary>
        /// Can read and write messages, add participants and change chat info.
        /// </summary>
        Participant = 2,
        /// <summary>
        /// Can read and write messages, change the composition of participants, change chat info and remove foreign messages.
        /// </summary>
        Moderator = 3,
        /// <summary>
        /// Can perform any action
        /// </summary>
        Admin = 4,
    }
}

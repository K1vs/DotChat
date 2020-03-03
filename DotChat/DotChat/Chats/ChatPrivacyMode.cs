namespace K1vs.DotChat.Chats
{
    public enum ChatPrivacyMode
    {
        /// <summary>
        /// Chat is invisible for users. Can join only via invite
        /// </summary>
        Private = 0,
        /// <summary>
        /// Chat is visible. But user can only join after approve. Via apply
        /// </summary>
        Protected = 1,
        /// <summary>
        /// Chat is visible. User can free join
        /// </summary>
        Public = 2
    }
}

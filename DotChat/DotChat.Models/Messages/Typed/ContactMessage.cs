namespace K1vs.DotChat.Models.Messages.Typed
{
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using K1vs.DotChat.Users;
    using System.Collections.Generic;

    public class ContactMessage: IContactMessage
    {
        public ContactMessage()
        {
        }

        public ContactMessage(IChatUser contact, IReadOnlyList<string> styles = null)
        {
            Contact = contact;
            Styles = styles;
        }

        public IChatUser Contact { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}

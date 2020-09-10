namespace K1vs.DotChat.Models.Messages.Typed
{
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class ContactMessage: IContactMessage
    {
        public ContactMessage()
        {
        }

        public ContactMessage(IChatUser contact, string style = null, string metadata = null)
        {
            Contact = contact;
            Style = style;
            Metadata = metadata;
        }

        public IChatUser Contact { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}

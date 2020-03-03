namespace K1vs.DotChat.Models.Messages.Typed
{
    using DotChat.Messages.Typed;
    using DotChat.Participants;

    public class ContactMessage<TChatUser>: IContactMessage<TChatUser>
        where TChatUser : IChatUser 
    {
        public ContactMessage()
        {
        }

        public ContactMessage(TChatUser contact, string style = null, string metadata = null)
        {
            Contact = contact;
            Style = style;
            Metadata = metadata;
        }

        public TChatUser Contact { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}

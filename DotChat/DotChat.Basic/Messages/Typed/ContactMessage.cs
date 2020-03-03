namespace K1vs.DotChat.Basic.Messages.Typed
{
    using DotChat.Messages.Typed;
    using DotChat.Participants;
    using Models.Messages.Typed;
    using Models.Participants;

    public class ContactMessage: ContactMessage<ChatUser>
    {
        public ContactMessage()
        {
        }

        public ContactMessage(ChatUser contact, string style = null, string metadata = null) : base(contact, style, metadata)
        {
        }
    }
}

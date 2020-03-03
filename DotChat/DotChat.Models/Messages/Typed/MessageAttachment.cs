namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using DotChat.Messages.Typed;

    public class MessageAttachment: IMessageAttachment
    {
        public MessageAttachment()
        {
        }

        public MessageAttachment(Guid id, string name, string type, string style = null, string metadata = null)
        {
            Id = id;
            Name = name;
            Type = type;
            Style = style;
            Metadata = metadata;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}

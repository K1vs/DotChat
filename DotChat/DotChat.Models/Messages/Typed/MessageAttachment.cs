namespace K1vs.DotChat.Models.Messages.Typed
{
    using System;
    using System.Collections.Generic;
    using DotChat.Messages.Typed;

    public class MessageAttachment: IMessageAttachment
    {
        public MessageAttachment()
        {
        }

        public MessageAttachment(Guid id, string name, string type, IReadOnlyList<string> styles = null)
        {
            Id = id;
            Name = name;
            Type = type;
            Styles = styles;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}

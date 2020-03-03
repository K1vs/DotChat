namespace K1vs.DotChat.Models.Messages.Typed
{
    using DotChat.Messages.Typed;

    public class TextMessage: ITextMessage
    {
        public TextMessage()
        {
        }

        public TextMessage(string content, int? collapseIndex = null, string style = null, string metadata = null)
        {
            Content = content;
            CollapseIndex = collapseIndex;
            Style = style;
            Metadata = metadata;
        }

        public string Content { get; set; }
        public int? CollapseIndex { get; set; }
        public string Style { get; set; }
        public string Metadata { get; set; }
    }
}

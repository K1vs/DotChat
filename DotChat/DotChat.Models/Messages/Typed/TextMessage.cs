namespace K1vs.DotChat.Models.Messages.Typed
{
    using DotChat.Messages.Typed;
    using System.Collections.Generic;

    public class TextMessage: ITextMessage
    {
        public TextMessage()
        {
        }

        public TextMessage(string content, int? collapseIndex = null, IReadOnlyList<string> styles = null)
        {
            Content = content;
            CollapseIndex = collapseIndex;
            Styles = styles;
        }

        public string Content { get; set; }
        public int? CollapseIndex { get; set; }
        public IReadOnlyList<string> Styles { get; set; }
    }
}

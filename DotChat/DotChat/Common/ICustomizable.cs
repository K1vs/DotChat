namespace K1vs.DotChat.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomizable
    {
        IReadOnlyList<string> Styles { get; }
    }
}

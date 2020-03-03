namespace K1vs.DotChat.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomizable
    {
        string Style { get; }
        string Metadata { get; }
    }
}

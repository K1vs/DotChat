namespace K1vs.DotChat.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IChatWorkersConfiguration
    {
        bool FastMessageMode { get; }

        bool DisableSystemMessages { get; set; }
    }
}

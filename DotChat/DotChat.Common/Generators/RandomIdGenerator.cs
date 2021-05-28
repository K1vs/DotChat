using K1vs.DotChat.Generators;
using System;

namespace K1vs.DotChat.Common.Generators
{
    public class RandomIdGenerator : IIdGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}

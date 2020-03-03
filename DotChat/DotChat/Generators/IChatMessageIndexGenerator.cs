namespace K1vs.DotChat.Generators
{
    using System;
    using System.Threading.Tasks;

    public interface IChatMessageIndexGenerator
    {
        Task<long> Generate(Guid chatId);
    }
}

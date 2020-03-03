namespace K1vs.DotChat.Bus
{
    using System.Threading.Tasks;
    using Commands;

    public interface IChatCommandSender
    {
        Task Send<TCommand>(TCommand command)
            where TCommand : ICommandBase;
    }
}

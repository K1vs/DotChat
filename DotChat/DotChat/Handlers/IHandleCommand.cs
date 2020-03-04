namespace K1vs.DotChat.Handlers
{
    using System.Threading.Tasks;
    using Bus;
    using Commands;

    public interface IHandleCommand<in TCommand>
        where TCommand: ICommand
    {
        Task Handle(TCommand command, IChatBusContext chatEventPublisher);
    }
}

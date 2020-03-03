namespace K1vs.DotChat.Handlers
{
    using System.Threading.Tasks;
    using Bus;
    using Events;

    public interface IHandleEvent<in TEvent>
        where TEvent: IEventBase
    {
        Task Handle(TEvent @event, IChatBusContext chatBusContext);
    }
}

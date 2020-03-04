namespace K1vs.DotChat.Handlers
{
    using System.Threading.Tasks;
    using Bus;
    using Events;

    public interface IHandleEvent<in TEvent>
        where TEvent: IEvent
    {
        Task Handle(TEvent @event, IChatBusContext chatBusContext);
    }
}

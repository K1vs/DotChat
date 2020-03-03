namespace K1vs.DotChat.Bus
{
    using System.Threading.Tasks;
    using Events;

    public interface IChatEventPublisher
    {
        Task Publish<TEvent>(TEvent @event)
            where TEvent : IEventBase;
    }
}

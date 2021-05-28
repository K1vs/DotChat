namespace K1vs.DotChat.Modules
{
    public interface IChatNotificationModule<TContainer>
    {
        void RegisterChatNotificationModule(TContainer container);
    }
}

namespace K1vs.DotChat.Modules
{
    public interface IChatServiceModule<TContainer>
    {
        void RegisterChatServiceModule(TContainer container);
    }
}

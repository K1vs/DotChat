namespace K1vs.DotChat.Modules
{
    public interface IChatWorkerModule<TContainer>
    {
        void RegisterChatWorkerModule(TContainer container);
    }
}

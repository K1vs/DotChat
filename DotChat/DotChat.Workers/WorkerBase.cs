namespace K1vs.DotChat.Workers
{
    using Configuration;

    public class WorkerBase
    {
        protected readonly string WorkerName;

        protected readonly IChatWorkersConfiguration ChatWorkersConfiguration;

        protected WorkerBase(IChatWorkersConfiguration chatWorkersConfiguration)
        {
            ChatWorkersConfiguration = chatWorkersConfiguration;
            WorkerName = GetType().FullName;
        }
    }
}

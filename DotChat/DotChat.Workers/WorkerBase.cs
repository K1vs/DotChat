namespace K1vs.DotChat.Workers
{
    using Configuration;

    public class WorkerBase<TChatWorkersConfiguration>
        where TChatWorkersConfiguration : IChatWorkersConfiguration
    {
        protected readonly string WorkerName;

        protected readonly TChatWorkersConfiguration ChatWorkersConfiguration;

        protected WorkerBase(TChatWorkersConfiguration chatWorkersConfiguration)
        {
            ChatWorkersConfiguration = chatWorkersConfiguration;
            WorkerName = GetType().FullName;
        }
    }
}

namespace BookGenerator.Core
{
    public interface IEvaluator<TContext>
    {
        TContext Init();

        void AddCustomCommands(string source);

        ICrawler GetCrawler(string source);
    }
}
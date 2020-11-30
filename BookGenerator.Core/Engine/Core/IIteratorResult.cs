namespace BookGenerator.Core.Engine.Core
{
	public interface IIteratorResult
	{
		JSValue value { get; }
		bool done { get; }
	}
}
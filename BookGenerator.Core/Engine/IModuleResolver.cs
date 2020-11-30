namespace BookGenerator.Core.Engine
{
	public interface IModuleResolver
	{
		bool TryGetModule(ModuleRequest moduleRequest, out Module result);
	}
}
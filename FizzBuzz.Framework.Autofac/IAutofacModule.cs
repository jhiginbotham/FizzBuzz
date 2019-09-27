namespace FizzBuzz.Framework.Autofac
{
	public interface IAutofacModule : ISimpleAutofacModule
	{
		OnActivatedHandlerCollection GetOnActivationHandlers();
		OnInitiatingHandlerCollection GetOnInitiatingHandlers();
		OnPreparingHandlerCollection GetOnPreparingHandlers();
	}
}

namespace SubstituteUnitTests
{
    public interface IParameterSetupHelper
    {
        T Get<T>() where T : class;
    }
}
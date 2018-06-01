namespace SubstituteUnitTests
{
    public interface IParameterSetupHelper
    {
        T Get<T>(string name = null) where T : class;
        T Set<T>(T value, string name = null) where T : class;
    }
}
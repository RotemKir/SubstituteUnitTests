using System;
using System.Linq;

namespace SubstituteUnitTests
{
    public class ParameterSetupHelper
    {
        private readonly object[] _parameters;

        public ParameterSetupHelper(object[] parameters)
        {
            _parameters = parameters;
        }

        public T Get<T>() where T : class
        {
            var parameter = _parameters.FirstOrDefault(x => x is T);

            if (parameter == null)
            {
                throw new InvalidOperationException($"Parameter of type {typeof(T).Name} doesn't exist");
            }

            return parameter as T;
        }
    }
}

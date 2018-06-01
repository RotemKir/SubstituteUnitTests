using SubstituteUnitTests.Models;
using System;
using System.Linq;

namespace SubstituteUnitTests
{
    internal class ParameterSetupHelper : IParameterSetupHelper
    {
        private readonly IParameter[] _parameters;

        public ParameterSetupHelper(IParameter[] parameters)
        {
            _parameters = parameters;
        }

        public T Get<T>(string name = null) where T : class
        {
            IParameter parameter = GetParameter<T>(name);

            return parameter.Value as T;
        }

        public T Set<T>(T value, string name = null) where T : class
        {
            IParameter parameter = GetParameter<T>(name);

            parameter.Value = value;

            return value;
        }

        private IParameter GetParameter<T>(string name) where T : class
        {
            IParameter parameter;
            var parameterType = typeof(T);

            if (string.IsNullOrWhiteSpace(name))
            {
                parameter = GetParameterByType(parameterType);
            }
            else
            {
                parameter = GetParameterByName(name, parameterType);
            }

            return parameter;
        }

        private IParameter GetParameterByType(Type parameterType)
        {
            var parameter = _parameters.FirstOrDefault(x => x.Type == parameterType);

            if (parameter == null)
            {
                throw new InvalidOperationException($"Parameter of type {parameterType.Name} doesn't exist");
            }

            return parameter;
        }

        private IParameter GetParameterByName(string name, Type parameterType)
        {
            var parameter = _parameters.FirstOrDefault(x => x.Name == name && x.Type == parameterType);

            if (parameter == null)
            {
                throw new InvalidOperationException($"Parameter of type {parameterType.Name}, named {name} doesn't exist");
            }

            return parameter;
        }

       
    }
}
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SubstituteUnitTests
{
    public abstract class SubstituteUnitTestBase<T> where T : class
    {
        private class ConstructorWithParameters
        {
            public ConstructorWithParameters(ConstructorInfo constructorInfo)
            {
                ConstructorInfo = constructorInfo;
                Parameters = constructorInfo.GetParameters();
            }

            public ConstructorInfo ConstructorInfo { get; }
            public ParameterInfo[] Parameters { get; }
        }

        private ConstructorInfo GetConstructorToCreate()
        {
            var unitType = typeof(T);
            var allConstructors = GetPublicConstructors(unitType);
            var validConstructors = GetValidConstructors(allConstructors);
            var constructorParametersGroup = GetConstructorWithMaxParameters(validConstructors);

            if (constructorParametersGroup == null)
            {
                throw new InvalidOperationException("Found no relevant constructors to substitute");
            }

            if (constructorParametersGroup.Count() > 1)
            {
                throw new InvalidOperationException("Found too many constructors to substitute");
            }

            return constructorParametersGroup.First().ConstructorInfo;
        }

        private IGrouping<int, ConstructorWithParameters> GetConstructorWithMaxParameters(IEnumerable<ConstructorWithParameters> constructors)
        {
            return constructors
                .GroupBy(x => x.Parameters.Length)
                .OrderByDescending(x => x.Key)
                .FirstOrDefault();
        }

        private IEnumerable<ConstructorWithParameters> GetValidConstructors(IEnumerable<ConstructorInfo> constructors)
        {            
            return constructors
                .Select(x => new ConstructorWithParameters(x))
                .Where(IsCtorWithInterfacesOnly);
        }

        private bool IsCtorWithInterfacesOnly(ConstructorWithParameters constructorWithParameters)
        {
            return constructorWithParameters.Parameters.All(x => x.ParameterType.IsInterface);
        }

        private ConstructorInfo[] GetPublicConstructors(Type unitType)
        {
            return unitType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
        }

        protected T CreateUnit(Action<IParameterSetupHelper> parametersSetup = null)
        {
            var constructor = GetConstructorToCreate();
            var parameters = constructor.GetParameters().Select(CreateParameterSubstitute).ToArray();

            SetupParameters(parametersSetup, parameters);

            return constructor.Invoke(parameters) as T;
        }

        private void SetupParameters(Action<ParameterSetupHelper> parametersSetup, object[] parameters)
        {
            if (parametersSetup != null)
            {
                var parametersSetupHelper = new ParameterSetupHelper(parameters);
                parametersSetup(parametersSetupHelper);
            }
        }

        private object CreateParameterSubstitute(ParameterInfo parameterInfo)
        {
            return Substitute.For(new[] { parameterInfo.ParameterType }, new object[0]);
        }
    }
}

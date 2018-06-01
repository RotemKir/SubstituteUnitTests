using NSubstitute;
using SubstituteUnitTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SubstituteUnitTests
{
    public abstract partial class SubstituteUnitTestBase<T> where T : class
    {
        protected T CreateUnit(Action<IParameterSetupHelper> parametersSetup = null)
        {
            var constructor = GetConstructorToSubstitute();
            FillParametersSubstitute(constructor);
            SetupParameters(parametersSetup, constructor);
            var parameters = GetParameterValues(constructor);

            return InvokeConstructor(constructor, parameters);
        }

        private object[] GetParameterValues(Constructor constructor)
        {
            return constructor.Parameters.Select(x => x.Value).ToArray();
        }

        private Constructor GetConstructorToSubstitute()
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

            return constructorParametersGroup.First();
        }

        private IGrouping<int, Constructor> GetConstructorWithMaxParameters(IEnumerable<Constructor> constructors)
        {
            return constructors
                .GroupBy(x => x.Parameters.Length)
                .OrderByDescending(x => x.Key)
                .FirstOrDefault();
        }

        private IEnumerable<Constructor> GetValidConstructors(IEnumerable<ConstructorInfo> constructors)
        {            
            return constructors
                .Select(x => new Constructor(x))
                .Where(IsCtorWithInterfacesOnly);
        }

        private bool IsCtorWithInterfacesOnly(Constructor constructor)
        {
            return constructor.Parameters.All(x => x.Type.IsInterface);
        }

        private ConstructorInfo[] GetPublicConstructors(Type unitType)
        {
            return unitType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
        }
                
        private T InvokeConstructor(Constructor constructorWithParameters, object[] parameters)
        {
            return constructorWithParameters.ConstructorInfo.Invoke(parameters) as T;
        }

        private void FillParametersSubstitute(Constructor constructor)
        {
            foreach (var parameter in constructor.Parameters)
            {
                parameter.Value = CreateParameterSubstitute(parameter);
            }
        }

        private void SetupParameters(Action<IParameterSetupHelper> parametersSetup, Constructor constructor)
        {
            if (parametersSetup != null)
            {
                var parametersSetupHelper = new ParameterSetupHelper(constructor.Parameters);
                parametersSetup(parametersSetupHelper);
            }
        }

        private object CreateParameterSubstitute(IParameter parameter)
        {
            return Substitute.For(new[] { parameter.Type }, new object[0]);
        }
    }
}

using NSubstitute;
using System;
using System.Linq;
using System.Reflection;

namespace SubstituteUnitTests
{
    public abstract class SubstituteUnitTestBase<T> where T : class
    {
        private static ConstructorInfo _defaultConstructorInfo;
        private static ConstructorInfo _interfacesOnlyConstructorInfo;
        private static ParameterInfo[] _interfacesOnlyConstructorParameterInfos;

        static SubstituteUnitTestBase()
        {
            var unitType = typeof(T);
            var constructorInfos = GetPublicConstructors(unitType);

            foreach (var constructorInfo in constructorInfos)
            {
                var parameters = constructorInfo.GetParameters();

                if (IsDefaultCtor(parameters))
                {
                    _defaultConstructorInfo = constructorInfo;
                }
                else if (IsCtorWithInterfacesOnly(parameters))
                {
                    _interfacesOnlyConstructorInfo = constructorInfo;
                    _interfacesOnlyConstructorParameterInfos = constructorInfo.GetParameters();
                }
            }
        }

        private static bool IsCtorWithInterfacesOnly(ParameterInfo[] parameters)
        {
            return parameters.All(x => x.ParameterType.IsInterface);
        }

        private static bool IsDefaultCtor(ParameterInfo[] parameters)
        {
            return !parameters.Any();
        }

        private static ConstructorInfo[] GetPublicConstructors(Type unitType)
        {
            return unitType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
        }

        protected T CreateUnit(Action<IParameterSetupHelper> parametersSetup = null)
        {
            if (_interfacesOnlyConstructorInfo != null)
            {
                return CreateUnitUsingInterfacesOnlyConstructor(parametersSetup);
            }

            if (_defaultConstructorInfo != null)
            {
                return CreateUnitUsingDefaultConstructor();
            }

            throw new InvalidOperationException($"Class {typeof(T).Name} has no valid constructors to substitute");
        }

        private T CreateUnitUsingInterfacesOnlyConstructor(Action<ParameterSetupHelper> parametersSetup)
        {
            object[] parameters = _interfacesOnlyConstructorParameterInfos
                .Select(CreateParameterSubstitute)
                .ToArray();

            SetupParameters(parametersSetup, parameters);

            return _interfacesOnlyConstructorInfo.Invoke(parameters) as T;
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

        private T CreateUnitUsingDefaultConstructor()
        {
            object[] parameters = new object[0];
            return _defaultConstructorInfo.Invoke(parameters) as T;
        }
    }
}

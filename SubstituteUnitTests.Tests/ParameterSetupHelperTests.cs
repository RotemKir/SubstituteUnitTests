using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SubstituteUnitTests.Models;
using SubstituteUnitTests.Tests.TestClasses;
using System;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ParameterSetupHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Get_ByType_TypeDoesntExistInParameters_Throws()
        {
            var parameters = new IParameter[0];
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface1>();
        }

        [TestMethod]
        public void ParameterSetupHelper_Get_ByType_TypeExistsInParameters_ReturnsParameter()
        {
            var interface1 = Substitute.For<Interface1>();
            var interface2 = Substitute.For<Interface2>();
            var interface3 = Substitute.For<Interface3>();
            var parameters = new[]
            {
                CreateParameter(interface1, "param1"),
                CreateParameter(interface2, "param2"),
                CreateParameter(interface3, "param3")
            };
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface1>().Should().Be(interface1);
            helper.Get<Interface2>().Should().Be(interface2);
            helper.Get<Interface3>().Should().Be(interface3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Get_ByName_NameDoesntExistInParameters_Throws()
        {
            var parameters = new IParameter[0];
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface1>("param1");
        }

        [TestMethod]
        public void ParameterSetupHelper_Get_ByName_NameExistsInParameters_ReturnsParameter()
        {
            var interface1 = Substitute.For<Interface1>();
            var interface2 = Substitute.For<Interface2>();
            var interface3 = Substitute.For<Interface3>();
            var parameters = new[]
            {
                CreateParameter(interface1, "param1"),
                CreateParameter(interface2, "param2"),
                CreateParameter(interface3, "param3")
            };
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface1>("param1").Should().Be(interface1);
            helper.Get<Interface2>("param2").Should().Be(interface2);
            helper.Get<Interface3>("param3").Should().Be(interface3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Get_ByName_NameExistsInParameters_IncorrectType_Throws()
        {
            var interface1 = Substitute.For<Interface1>();
            var parameters = new[]
            {
                CreateParameter(interface1, "param1"),
            };
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface2>("param1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Set_ByType_TypeDoesntExistInParameters_Throws()
        {
            var parameters = new IParameter[0];
            var helper = new ParameterSetupHelper(parameters);

            helper.Set<Interface1>(null);
        }

        [TestMethod]
        public void ParameterSetupHelper_Set_ByType_TypeExistsInParameters_ReplacesTheParameterValue()
        {
            var interface1 = Substitute.For<Interface1>();
            var interface1Override = Substitute.For<Interface1>();
            var parameters = new[] { CreateParameter(interface1, "param1") };
            var helper = new ParameterSetupHelper(parameters);

            helper.Set(interface1Override).Should().Be(interface1Override);
            helper.Get<Interface1>().Should().Be(interface1Override);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Set_ByName_NameDoesntExistInParameters_Throws()
        {
            var parameters = new IParameter[0];
            var helper = new ParameterSetupHelper(parameters);

            helper.Set<Interface1>(null, "param1");
        }

        [TestMethod]
        public void ParameterSetupHelper_Set_ByName_NameExistsInParameters_ReplacesTheParameterValue()
        {
            var interface1 = Substitute.For<Interface1>();
            var interface1Override = Substitute.For<Interface1>();
            var parameters = new[] { CreateParameter(interface1, "param1") };
            var helper = new ParameterSetupHelper(parameters);

            helper.Set(interface1Override, "param1").Should().Be(interface1Override);
            helper.Get<Interface1>("param1").Should().Be(interface1Override);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Set_ByName_NameExistsInParameters_IncorrectType_Throws()
        {
            var interface1 = Substitute.For<Interface1>();
            var interface2 = Substitute.For<Interface2>();
            var parameters = new[] { CreateParameter(interface1, "param1") };
            var helper = new ParameterSetupHelper(parameters);

            helper.Set(interface2, "param1");
        }

        private IParameter CreateParameter<T>(T value, string name)
        {
            var parameter = Substitute.For<IParameter>();
            parameter.Type.Returns(typeof(T));
            parameter.Value.Returns(value);
            parameter.Name.Returns(name);

            return parameter;
        }
    }
}

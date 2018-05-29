using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SubstituteUnitTests.Tests.TestClasses;
using System;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ParameterSetupHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ParameterSetupHelper_Get_TypeDoesntExistInParameters_Throws()
        {
            var parameters = new object[0];
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface1>();
        }

        [TestMethod]
        public void ParameterSetupHelper_Get_TypeExistsInParameters_ReturnsParameter()
        {
            var interface1 = Substitute.For<Interface1>();
            var interface2 = Substitute.For<Interface2>();
            var interface3 = Substitute.For<Interface3>();

            var parameters = new object[] 
            {
                interface1,
                interface2,
                interface3,
            };
            var helper = new ParameterSetupHelper(parameters);

            helper.Get<Interface1>().Should().Be(interface1);
            helper.Get<Interface2>().Should().Be(interface2);
            helper.Get<Interface3>().Should().Be(interface3);
        }
    }
}

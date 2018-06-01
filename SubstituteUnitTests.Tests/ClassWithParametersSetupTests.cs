using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SubstituteUnitTests.Tests.TestClasses;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ClassWithParametersSetupTests : SubstituteUnitTestBase<ClassWithSingleInterfaceInCtor>
    {
        [TestMethod]
        public void ClassWithParametersSetup_SetupParameterByType_CreateUnit_ReturnsNewInstanceAndNewSubstitute()
        {
            var unit = CreateUnit(setupHelper =>
            {
                setupHelper.Get<Interface1>().Work().Returns(35);
            });

            unit.Should().NotBeNull();
            unit.Interface1.Should().NotBeNull();
            unit.Interface1.Work().Should().Be(35);
        }

        [TestMethod]
        public void ClassWithParametersSetup_SetupParameterByName_CreateUnit_ReturnsNewInstanceAndNewSubstitute()
        {
            var unit = CreateUnit(setupHelper =>
            {
                setupHelper.Get<Interface1>("interface1").Work().Returns(35);
            });

            unit.Should().NotBeNull();
            unit.Interface1.Should().NotBeNull();
            unit.Interface1.Work().Should().Be(35);
        }

        [TestMethod]
        public void ClassWithParametersSetup_OverrideParameterValueByType_CreateUnit_ReturnsNewInstanceAndNewSubstitute()
        {
            var interface1 = Substitute.For<Interface1>();
            var unit = CreateUnit(setupHelper =>
            {
                setupHelper.Set(interface1).Work().Returns(35);
            });

            unit.Should().NotBeNull();
            unit.Interface1.Should().Be(interface1);
            unit.Interface1.Work().Should().Be(35);
        }

        [TestMethod]
        public void ClassWithParametersSetup_OverrideParameterValueByName_CreateUnit_ReturnsNewInstanceAndNewSubstitute()
        {
            var interface1 = Substitute.For<Interface1>();
            var unit = CreateUnit(setupHelper =>
            {
                setupHelper.Set(interface1, "interface1").Work().Returns(35);
            });

            unit.Should().NotBeNull();
            unit.Interface1.Should().Be(interface1);
            unit.Interface1.Work().Should().Be(35);
        }
    }
}
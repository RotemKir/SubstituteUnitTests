using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SubstituteUnitTests.Tests.TestClasses;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ClassWithSingleInterfaceInCtorTests : SubstituteUnitTestBase<ClassWithSingleInterfaceInCtor>
    {
        [TestMethod]
        public void ClassWithSingleInterfaceInCtor_CreateUnit_ReturnsNewInstanceAndNewSubstitute()
        {
            var unit = CreateUnit();
            var otherUnit = CreateUnit();

            unit.Should().NotBeNull();
            unit.Interface1.Should().NotBeNull();
            unit.Should().NotBe(otherUnit);
            unit.Interface1.Should().NotBe(otherUnit.Interface1);
        }

        [TestMethod]
        public void ClassWithSingleInterfaceInCtor_SetupParameter_CreateUnit_ReturnsNewInstanceAndNewSubstitute()
        {
            var unit = CreateUnit(setupHelper =>
            {
                setupHelper.Get<Interface1>().Work().Returns(35);
            });

            unit.Should().NotBeNull();
            unit.Interface1.Should().NotBeNull();
            unit.Interface1.Work().Should().Be(35);
        }
    }
}

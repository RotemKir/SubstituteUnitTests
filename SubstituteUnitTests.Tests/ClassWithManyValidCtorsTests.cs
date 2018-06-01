using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SubstituteUnitTests.Tests.TestClasses;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ClassWithManyValidCtorsTests : SubstituteUnitTestBase<ClassWithManyValidCtors>
    {
        [TestMethod]
        public void ClassWithManyValidCtors_CreateUnit_ReturnsNewInstanceAndNewSubstituteUsingCtorWithMostParameters()
        {
            var unit = CreateUnit();
            var otherUnit = CreateUnit();

            unit.Should().NotBeNull();
            unit.Interface1.Should().NotBeNull();
            unit.Interface2.Should().NotBeNull();
            unit.Interface3.Should().NotBeNull();
            unit.Should().NotBe(otherUnit);
            unit.Interface1.Should().NotBe(otherUnit.Interface1);
            unit.Interface2.Should().NotBe(otherUnit.Interface2);
            unit.Interface3.Should().NotBe(otherUnit.Interface3);
        }

        [TestMethod]
        public void ClassWithManyValidCtors_SetupParameter_CreateUnit_ReturnsNewInstanceAndNewSubstituteUsingCtorWithMostParameters()
        {
            var unit = CreateUnit(setupHelper =>
            {
                setupHelper.Get<Interface1>().Work().Returns(35);
                setupHelper.Get<Interface2>().Work().Returns(91);
                setupHelper.Get<Interface3>().Work().Returns(46);
            });

            unit.Should().NotBeNull();
            unit.Interface1.Should().NotBeNull();
            unit.Interface2.Should().NotBeNull();
            unit.Interface3.Should().NotBeNull();
            unit.Interface1.Work().Should().Be(35);
            unit.Interface2.Work().Should().Be(91);
            unit.Interface3.Work().Should().Be(46);
        }
    }
}

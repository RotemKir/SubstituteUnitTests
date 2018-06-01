using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
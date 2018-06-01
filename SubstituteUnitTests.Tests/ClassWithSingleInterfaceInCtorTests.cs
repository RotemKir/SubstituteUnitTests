using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
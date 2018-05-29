using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstituteUnitTests.Tests.TestClasses;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ClassWithDefaultCtorTests : SubstituteUnitTestBase<ClassWithDefaultCtor>
    {
        [TestMethod]
        public void ClassWithDefaultCtor_CreateUnit_ReturnsNewInstance()
        {
            var unit = CreateUnit();
            var otherUnit = CreateUnit();

            unit.Should().NotBeNull();
            unit.Should().NotBe(otherUnit);
        }
    }
}

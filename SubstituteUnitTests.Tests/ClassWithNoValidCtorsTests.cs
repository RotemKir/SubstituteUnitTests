using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstituteUnitTests.Tests.TestClasses;
using System;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ClassWithNoValidCtorsTests : SubstituteUnitTestBase<ClassWithNoValidCtors>
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClassWithNoValidCtors_CreateUnit_Throws()
        {
            CreateUnit();
        }
    }
}
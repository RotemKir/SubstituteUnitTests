using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstituteUnitTests.Tests.TestClasses;
using System;

namespace SubstituteUnitTests.Tests
{
    [TestClass]
    public class ClassWithManyValidCtorsWithSameAmountOfParametersTests : SubstituteUnitTestBase<ClassWithManyValidCtorsWithSameAmountOfParameters>
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClassWithManyValidCtorsWithSameAmountOfParameters_CreateUnit_Throws()
        {
            CreateUnit();
        }
    }
}

namespace SubstituteUnitTests.Tests.TestClasses
{
    public class ClassWithSingleInterfaceInCtor
    {
        public ClassWithSingleInterfaceInCtor(Interface1 interface1)
        {
            Interface1 = interface1;
        }

        public Interface1 Interface1 { get; }
    }
}

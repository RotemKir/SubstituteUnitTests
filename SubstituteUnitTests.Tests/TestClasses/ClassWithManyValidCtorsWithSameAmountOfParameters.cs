namespace SubstituteUnitTests.Tests.TestClasses
{
    public class ClassWithManyValidCtorsWithSameAmountOfParameters
    {
        public ClassWithManyValidCtorsWithSameAmountOfParameters(Interface2 interface2, Interface3 interface3)
        {
            Interface2 = interface2;
            Interface3 = interface3;
        }

        public ClassWithManyValidCtorsWithSameAmountOfParameters(Interface1 interface1, Interface2 interface2)
        {
            Interface1 = interface1;
            Interface2 = interface2;
        }

        public Interface1 Interface1 { get; }
        public Interface2 Interface2 { get; }
        public Interface3 Interface3 { get; }
    }
}

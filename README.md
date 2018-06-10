# SubstituteUnitTests
A simple framework for unit tests using substitutes for dependencies

## What is it ?
SubstituteUnitTests aims to reduce the amount of boilerplate code in unit tests.

Consider for example the following code for a unit test using [NSubstitute](http://nsubstitute.github.io/):
```c#
[TestMethod]
public void SomeTestMethod()
{
    // Arrange
    var dependencyA = Substitute.For<IDependencyA>();
    // Fake the Work method:
    dependencyA.Work().Returns(35);
    var dependencyB = Substitute.For<IDependencyB>();
    // Additional setup of mocks goes here
    var myClass = new MyClass(dependencyA, dependencyB);
    
    // Act and Assert goes here 
}
```

Using SubstituteUnitTests we can reduce the code to:
```c#
[TestMethod]
public void SomeTestMethod()
{
    // Arrange
    var myClass = CreateUnit(setupHelper =>
            {
                // Fake the Work method:              
                setupHelper.Get<IDependencyA>().Work().Returns(35);
                // Additional setup of mocks goes here
            });
        
    // Act and Assert goes here 
}
```

## How do I use it ?
Your test class should inherit from SubstituteUnitTestBase<T> - The T stands for the class we wish to perform the unit test on.

```c#
[TestClass]
public class MyClassTests : SubstituteUnitTestBase<MyClass>
{
...
}
```

T can be any class that has only interfaces in its ctor or none at all.
Now you can call CreateUnit to create the object of the test:
This method will find the most relevant Ctor and initialize it by creating substitutes (using NSubstitute) for each interface parameter.

CreateUnit also accepts an Action<IParameterSetupHelper> delegate so you can setup the mocks for your dependencies:
```c#
// The class definition:
public class MyClass
{
    public MyClass(IDependencyA dependencyA, IDependencyB) {...}
}

// Setup the unit test:
CreateUnit(setupHelper =>
{
    // Get ctor parameter by type:              
    setupHelper.Get<IDependencyA>().Work().Returns(35);
    // Get ctor parameter by name:
    setupHelper.Get<IDependencyB>("dependencyB").Work().Returns(49);
});
```

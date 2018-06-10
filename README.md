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

using System;

namespace SubstituteUnitTests.Models
{
    public interface IParameter
    {
        string Name { get; }
        Type Type { get; }
        object Value { get; set; }
    }
}
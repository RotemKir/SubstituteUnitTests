using System;
using System.Reflection;

namespace SubstituteUnitTests.Models
{
    public class Parameter : IParameter
    {
        public Parameter(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name;
            Type = parameterInfo.ParameterType;
        }

        public string Name { get; }
        public Type Type { get; }
        public object Value { get; set; }
    }
}

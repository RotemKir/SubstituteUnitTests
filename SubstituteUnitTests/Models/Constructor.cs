using System.Linq;
using System.Reflection;

namespace SubstituteUnitTests.Models
{
    internal class Constructor
    {
        public Constructor(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;
            Parameters = GetParameters(constructorInfo);
        }

        private Parameter[] GetParameters(ConstructorInfo constructorInfo)
        {
            return constructorInfo
                .GetParameters()
                .Select(x => new Parameter(x))
                .ToArray();
        }

        public ConstructorInfo ConstructorInfo { get; }
        public IParameter[] Parameters { get; }
    }
}
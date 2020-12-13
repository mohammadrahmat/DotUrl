using System.Collections.Generic;
using Xunit.Sdk;
using System.Reflection;

namespace DeeplinkService.Tests.TestData
{
    public class SampleSearchDeeplinksAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "ty://?Page=Search&Query=elbise" };
            yield return new object[] { "ty://?Page=Search&Query=%C3%BCt%C3%BC" };
        }
    }
}

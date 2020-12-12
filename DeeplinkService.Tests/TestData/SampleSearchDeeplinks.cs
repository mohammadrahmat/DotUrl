using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using System.Reflection;

namespace DeeplinkService.Tests.TestData
{
    public class SampleSearchDeeplinks : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { "ty://?Page=Search&Query=elbise" };
            yield return new object[] { "ty://?Page=Search&Query=%C3%BCt%C3%BC" };
        }
    }
}

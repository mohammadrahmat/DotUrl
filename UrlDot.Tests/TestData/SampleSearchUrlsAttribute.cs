using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace UrlService.Tests.TestData
{
    public class SampleSearchUrlsAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "https://www.trendyol.com/tum--urunler/?q=elbise" };
            yield return new object[] { "https://www.trendyol.com/tum--urunler/?q=%C3%BCt%C3%BC" };
        }
    }
}

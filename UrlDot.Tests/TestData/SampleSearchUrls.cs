using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace UrlService.Tests.TestData
{
    public class SampleSearchUrls : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { "https://trendyol.com/tum--urunler/?q=elbise" };
            yield return new object[] { "https://trendyol.com/tum--urunler/?q=%C3%BCt%C3%BC" };
        }
    }
}

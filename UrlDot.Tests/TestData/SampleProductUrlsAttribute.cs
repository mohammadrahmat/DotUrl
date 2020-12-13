using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace UrlService.Tests.TestData
{
    public class SampleProductUrlsAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "https://www.trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064"};
            yield return new object[] { "https://www.trendyol.com/casio/erkek-kol-saati-p-1925865" };
            yield return new object[] { "https://www.trendyol.com/casio/erkek-kol-saati-p-1925865?boutiqueId=439892" };
            yield return new object[] { "https://www.trendyol.com/casio/erkek-kol-saati-p-1925865?merchantId=105064" };
        }
    }
}

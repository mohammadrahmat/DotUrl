using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace DeeplinkService.Tests.TestData
{
    public class SampleDeeplinkServiceTestData : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064",
                "https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892&merchantId=105064" };
            yield return new object[] { "ty://?Page=Product&ContentId=1925865",
                "https://www.trendyol.com/brand/name-p-1925865" };
            yield return new object[] { "ty://?Page=Product&ContentId=1925865&CampaignId=439892",
                "https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892" };
            yield return new object[] { "ty://?Page=Product&ContentId=1925865&MerchantId=105064",
                "https://www.trendyol.com/brand/name-p-1925865?merchantId=105064" };
            yield return new object[] { "ty://?Page=Search&Query=elbise",
                "https://www.trendyol.com/tum--urunler/?q=elbise" };
            yield return new object[] { "ty://?Page=Search&Query=%C3%BCt%C3%BC",
                "https://www.trendyol.com/tum--urunler/?q=%C3%BCt%C3%BC" };
            yield return new object[] { "ty://?Page=Home",
                "https://www.trendyol.com" };
            yield return new object[] { "ty://?Page=Home",
                "https://www.trendyol.com" };
        }
    }
}

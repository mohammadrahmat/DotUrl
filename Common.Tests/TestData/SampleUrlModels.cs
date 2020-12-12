using DotUrl.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace Common.Tests.TestData
{
    class SampleUrlModels : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { new  UrlServiceModel {
                PageType = PageType.Search,
                HasError = false,
                SearchQuery = "elbise"
            }, "ty://?Page=Search&Query=elbise" };

            yield return new object[] { new  UrlServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865
            }, "ty://?Page=Product&ContentId=1925865" };

            yield return new object[] { new  UrlServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865,
                BoutiqueId = 439892
            }, "ty://?Page=Product&ContentId=1925865&CampaignId=439892" };

            yield return new object[] { new  UrlServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865,
                MerchantId = 105064
            }, "ty://?Page=Product&ContentId=1925865&MerchantId=105064" };
            yield return new object[] { new  UrlServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865,
                BoutiqueId = 439892,
                MerchantId = 105064
            }, "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064" };

            yield return new object[] { new  UrlServiceModel {
                PageType = PageType.Other,
                HasError = false
            }, "ty://?Page=Home" };
        }
    }
}

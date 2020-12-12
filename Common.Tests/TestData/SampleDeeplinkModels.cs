using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using System.Reflection;
using DotUrl.Models;

namespace Common.Tests.TestData
{
    class SampleDeeplinkModels : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { new  DeeplinkServiceModel {
                PageType = PageType.Search,
                HasError = false,
                SearchQuery = "elbise"
            }, "https://www.trendyol.com/tum--urunler/?q=elbise" };

            yield return new object[] { new  DeeplinkServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865
            }, "https://www.trendyol.com/brand/name-p-1925865" };

            yield return new object[] { new  DeeplinkServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865,
                CampaignId = 439892
            }, "https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892" };

            yield return new object[] { new  DeeplinkServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865,
                MerchantId = 105064
            }, "https://www.trendyol.com/brand/name-p-1925865?merchantId=105064" };
            yield return new object[] { new  DeeplinkServiceModel {
                PageType = PageType.Product,
                HasError = false,
                ContentId = 1925865,
                CampaignId = 439892,
                MerchantId = 105064
            }, "https://www.trendyol.com/brand/name-p-1925865?boutiqueId=439892&merchantId=105064" };

            yield return new object[] { new  DeeplinkServiceModel {
                PageType = PageType.Other,
                HasError = false
            }, "https://www.trendyol.com" };
        }
    }
}

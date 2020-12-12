using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace DeeplinkService.Tests.TestData
{
    public class SampleProductDeeplinks : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064" };
            yield return new object[] { "ty://?Page=Product&ContentId=1925865" };
            yield return new object[] { "ty://?Page=Product&ContentId=1925865&CampaignId=439892" };
            yield return new object[] { "ty://?Page=Product&ContentId=1925865&MerchantId=105064" };
        }
    }
}

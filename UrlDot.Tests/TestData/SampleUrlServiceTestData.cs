using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace UrlService.Tests
{
    public class SampleUrlServiceTestData : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { "https://trendyol.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064",
                "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064" };
            yield return new object[] { "https://trendyol.com/casio/erkek-kol-saati-p-1925865",
                "ty://?Page=Product&ContentId=1925865" };
            yield return new object[] { "https://trendyol.com/casio/erkek-kol-saati-p-1925865?boutiqueId=439892",
                "ty://?Page=Product&ContentId=1925865&CampaignId=439892" };
            yield return new object[] { "https://trendyol.com/casio/erkek-kol-saati-p-1925865?merchantId=105064",
                "ty://?Page=Product&ContentId=1925865&MerchantId=105064" };
            yield return new object[] { "https://trendyol.com/tum--urunler/?q=elbise",
                "ty://?Page=Search&Query=elbise" };
            yield return new object[] { "https://trendyol.com/tum--urunler/?q=%C3%BCt%C3%BC",
                "ty://?Page=Search&Query=%C3%BCt%C3%BC" };
            yield return new object[] { "https://trendyol.com/Hesabim/Favoriler",
                "ty://?Page=Home" };
            yield return new object[] { "https://trendyol.com/Hesabim/#/Siparislerim",
                "ty://?Page=Home" };
        }
    }
}

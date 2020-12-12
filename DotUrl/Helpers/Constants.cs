using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Helpers
{
    public class Constants
    {
        public const string URL_DOMAIN = "trendyol.com";
        public const string DL_DOMAIN = "ty";
        public const string PRODUCT = "Product";
        public const string SEARCH = "Search";
        public const string HOME = "Home";
        public const string DEEPLINK_PRODUCT_TEMPLATE = "ty://?Page=Product&ContentId={0}";
        public const string DEEPLINK_SEARCH_TEMPLATE = "ty://?Page=Search&Query={0}";
        public const string DEEPLINK_HOME_TEMPLATE = "ty://?Page=Home";
        public const string DEEPLINK_CAMPAIGN_TEMPLATE = "&CampaignId={0}";
        public const string DEEPLINK_MERCHANT_TEMPLATE = "&MerchantId={0}";
        public const string URL_HOME_TEMPLATE = "https://www.trendyol.com";
        public const string URL_SEARCH_TEMPLATE = "https://www.trendyol.com/tum--urunler/?q={0}";
        public const string URL_PRODUCT_TEMPLATE = "https://www.trendyol.com/brand/name-p-{0}";
        public const string URL_BOUTIQUE_TEMPLATE = "boutiqueId={0}";
        public const string URL_MERCHANT_TEMPLATE = "merchantId={0}";
    }
}

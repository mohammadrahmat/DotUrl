using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Helpers
{
    public class Constants
    {
        public static readonly string DOMAIN = "trendyol.com";
        public static readonly string DEEPLINK_PRODUCT_TEMPLATE = "ty://?Page=Product&ContentId={0}";
        public static readonly string DEEPLINK_SEARCH_TEMPLATE = "ty://?Page=Search&Query={0}";
        public static readonly string DEEPLINK_HOME_TEMPLATE = "ty://?Page=Home";
        public static readonly string DEEPLINK_CAMPAIGN_TEMPLATE = "&CampaignId={0}";
        public static readonly string DEEPLINK_MERCHANT_TEMPLATE = "&MerchantId={0}";
    }
}

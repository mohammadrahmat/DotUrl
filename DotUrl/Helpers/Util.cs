using DotUrl.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotUrl.Helpers
{
    public class Util
    {
        public static Dictionary<string, string> ParseQueryString(string queryString)
        {
            var queries = new Dictionary<string, string>();
            var regexPattern = new Regex(@"[?&](\w[\w.]*)=([^?&]+)");
            var matches = regexPattern.Match(queryString);
            while (matches.Success)
            {
                queries.Add(matches.Groups[1].Value, matches.Groups[2].Value);
                matches = matches.NextMatch();
            }
            return queries;
        }

        public static string NormalizeText(string input)
        {
            var normalizedString = string.Join("", input.Normalize(NormalizationForm.FormD)
                                         .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
            //ugh, for some reason this does not normalize ı, time to get the big guns out:

            return normalizedString.Replace('ı', 'i');
        }

        public static string DeeplinkGenerator(UrlServiceModel url)
        {
            switch(url.PageType)
            {
                case PageType.Other:
                    return Constants.DEEPLINK_HOME_TEMPLATE;
                case PageType.Search:
                    return string.Format(Constants.DEEPLINK_SEARCH_TEMPLATE, url.SearchQuery);
                case PageType.Product:
                    var deepLink = string.Format(Constants.DEEPLINK_PRODUCT_TEMPLATE, url.ContentId);
                    if (url.BoutiqueId > 0)
                    {
                        deepLink = $"{deepLink}{string.Format(Constants.DEEPLINK_CAMPAIGN_TEMPLATE, url.BoutiqueId)}";
                    }
                    if (url.MerchantId > 0)
                    {
                        deepLink = $"{deepLink}{string.Format(Constants.DEEPLINK_MERCHANT_TEMPLATE, url.MerchantId)}";
                    }
                    return deepLink;
                default:
                    throw new ArgumentException($"Unknown Page Type: {url.PageType}");
            }
        }

        public static string UrlGenerator(DeeplinkServiceModel deeplink)
        {
            switch(deeplink.PageType)
            {
                case PageType.Other:
                    return Constants.URL_HOME_TEMPLATE;
                case PageType.Search:
                    return string.Format(Constants.URL_SEARCH_TEMPLATE, deeplink.SearchQuery);
                case PageType.Product:
                    var url = string.Format(Constants.URL_PRODUCT_TEMPLATE, deeplink.ContentId);
                    var seperatorFlag = "?";
                    if (deeplink.CampaignId > 0)
                    {
                        url = $"{url}{seperatorFlag}{string.Format(Constants.URL_BOUTIQUE_TEMPLATE, deeplink.CampaignId)}";
                        seperatorFlag = "&";
                    }
                    if (deeplink.MerchantId > 0)
                    {
                        url = $"{url}{seperatorFlag}{string.Format(Constants.URL_MERCHANT_TEMPLATE, deeplink.MerchantId)}";
                    }
                    return url;
                default:
                    throw new ArgumentException($"Unknown Page Type: {deeplink.PageType}");
            }
        }
    }
}

using DotUrl.Helpers;
using DotUrl.Interfaces;
using DotUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotUrl.Actions
{
    public class UrlAction : IAction<UrlServiceModel>
    {
        private readonly Regex _productPattern = new Regex(@"[\ ]?-p-[\ ]?", RegexOptions.None);
        private readonly Regex _searchPattern = new Regex(@"[\ ]?tum--urunler[\ ]?", RegexOptions.None);

        public string Execute(string input)
        {
            if (!VerifyInput(input))
            {
                return "Invalid Input";
            }

            var parsedInput = InputParser(input);

            parsedInput.Response = Util.DeeplinkGenerator(parsedInput);

            return parsedInput.Response;
        }

        public UrlServiceModel InputParser(string input)
        {
            var uri = new Uri(input);

            if (_productPattern.Match(input).Success)
            {
                return ParseProductUrl(uri);
            } 
            else if (_searchPattern.Match(input).Success)
            {
                return ParseSearchUrl(uri);
            }
            else
            {
                return ParseOtherUrl(uri);
            }
        }

        public UrlServiceModel ParseOtherUrl(Uri uri)
        {

            return new UrlServiceModel
            {
                PageType = PageType.Other,
                Request = uri.AbsoluteUri,
                Type = ServiceType.UrlToDeep,
                RecordDate = DateTime.Now,
                HasError = false
            };
        }

        public UrlServiceModel ParseSearchUrl(Uri uri)
        {
            var parsedModel = new UrlServiceModel
            {
                PageType = PageType.Search,
                Request = uri.AbsoluteUri,
                Type = ServiceType.UrlToDeep,
                RecordDate = DateTime.Now
            };
            var queryString = Util.ParseQueryString(uri.Query);
            if (!queryString.ContainsKey("q"))
            {
                parsedModel.HasError = true;
                parsedModel.ErrorDetails = $"Search term not found in url: {uri.AbsolutePath}";
                return parsedModel;
            }

            try
            {
                var searchQuery = queryString["q"];
                parsedModel.SearchQuery = Util.NormalizeText(searchQuery);
            }
            catch (Exception ex)
            {
                parsedModel.HasError = true;
                parsedModel.ErrorDetails = $"Error When Parsing Search Query in url: {uri.AbsolutePath}";
                parsedModel.ExceptionMessage = ex.Message;
            }
            return parsedModel;
        }

        public UrlServiceModel ParseProductUrl(Uri uri)
        {
            var parsedModel = new UrlServiceModel 
            { 
                PageType = PageType.Product,
                Request = uri.AbsoluteUri,
                Type = ServiceType.UrlToDeep,
                RecordDate = DateTime.Now
            };
            var segments = uri.Segments;
            if (segments.Length < 3)
            {
                parsedModel.HasError = true;
                parsedModel.ErrorDetails = $"Segment Count Less Than 3: {segments.Length}";
                return parsedModel;
            }

            try
            {
                var breakpoint = segments[2].IndexOf("-p-");
                parsedModel.BrandCategoryName = segments[1].Trim('/');
                parsedModel.BrandCategoryName = segments[2].Substring(0, breakpoint);
                parsedModel.ContentId = int.Parse(segments[2].Substring(breakpoint + 3));

                var queries = Util.ParseQueryString(uri.Query);
                parsedModel.BoutiqueId = queries.ContainsKey("boutiqueId") ? int.Parse(queries["boutiqueId"]) : 0;
                parsedModel.MerchantId = queries.ContainsKey("merchantId") ? int.Parse(queries["merchantId"]) : 0;
            }
            catch (Exception ex)
            {
                parsedModel.HasError = true;
                parsedModel.ExceptionMessage = ex.Message;
                parsedModel.ErrorDetails = $"Error While Parsing Model From Uri: {uri.AbsoluteUri}";
            }

            return parsedModel;
        }

        public bool VerifyInput(string input)
        {
            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                var uri = new Uri(input);
                return string.Equals(uri.Authority, Constants.DOMAIN, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}

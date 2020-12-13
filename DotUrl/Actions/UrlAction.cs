using DotUrl.Helpers;
using DotUrl.Interfaces;
using DotUrl.Models;
using Nest;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotUrl.Actions
{
    public class UrlAction : IAction<UrlServiceModel>
    {
        private readonly Regex _productPattern = new Regex(@"[\ ]?-p-[\ ]?", RegexOptions.None);
        private readonly Regex _searchPattern = new Regex(@"[\ ]?tum--urunler[\ ]?", RegexOptions.None);
        private readonly ElasticClient _client;

        public UrlAction(ElasticClient client)
        {
            _client = client;
        }

        public string Execute(string input)
        {
            if (!VerifyInput(input))
            {
                return "Invalid Input";
            }

            var searchedResponse = SearchIndex(input);
            if (!string.IsNullOrEmpty(searchedResponse))
            {
                return searchedResponse;
            }

            var parsedInput = InputParser(input);

            parsedInput.Response = Util.DeeplinkGenerator(parsedInput);

            if (parsedInput.HasError)
            {
                return parsedInput.ErrorDetails;
            }

            Task.Run(() => IndexRequest(parsedInput));

            return parsedInput.Response;
        }

        public UrlServiceModel InputParser(string input)
        {
            var uri = new Uri(input);

            if (_productPattern.Match(input).Success)
            {
                return ParseProductPageInput(uri);
            } 
            else if (_searchPattern.Match(input).Success)
            {
                return ParseSearchPageInput(uri);
            }
            else
            {
                return ParseOtherPageInput(uri);
            }
        }

        public UrlServiceModel ParseOtherPageInput(Uri uri)
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

        public UrlServiceModel ParseSearchPageInput(Uri uri)
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

        public UrlServiceModel ParseProductPageInput(Uri uri)
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
                return string.Equals(uri.Authority, Constants.URL_DOMAIN, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public async Task IndexRequest(UrlServiceModel serviceModel)
        {
            await _client.IndexDocumentAsync(serviceModel);
        }

        public string SearchIndex(string input)
        {
            var resp = string.Empty;
            var search = _client.Search<BaseServiceModel>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Request)
                        .Query(input)
                    )
                 )
                .Take(10)
            );

            if (search.Hits.Count > 0)
            {
                var docs = search.Documents.Where(d => d.Request == input);
                resp = docs.Any() ? docs.First().Response : string.Empty;
            }

            return resp;
        }
    }
}

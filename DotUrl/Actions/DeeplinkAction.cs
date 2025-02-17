﻿using DotUrl.Helpers;
using DotUrl.Interfaces;
using DotUrl.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotUrl.Actions
{
    public class DeeplinkAction : IAction<DeeplinkServiceModel>
    {
        private Dictionary<string, string> _parsedQueryString;
        private readonly ElasticClient _client;

        public DeeplinkAction(ElasticClient client)
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

            parsedInput.Response = Util.UrlGenerator(parsedInput);

            if (parsedInput.HasError)
            {
                return parsedInput.ErrorDetails;
            }

            Task.Run(() => IndexRequest(parsedInput));

            return parsedInput.Response;
        }

        public DeeplinkServiceModel InputParser(string input)
        {
            var uri = new Uri(input);
            var pageType = _parsedQueryString["Page"];
            
            if (pageType == Constants.PRODUCT)
            {
                return ParseProductPageInput(uri);
            }
            else if (pageType == Constants.SEARCH)
            {
                return ParseSearchPageInput(uri);
            }
            else
            {
                return ParseOtherPageInput(uri);
            }
        }

        public DeeplinkServiceModel ParseOtherPageInput(Uri uri)
        {
            return new DeeplinkServiceModel
            {
                PageType = PageType.Other,
                Request = uri.AbsoluteUri,
                Type = ServiceType.DeepToUrl,
                RecordDate = DateTime.Now,
                HasError = false
            };
        }

        public DeeplinkServiceModel ParseProductPageInput(Uri uri)
        {
            var parsedModel = new DeeplinkServiceModel
            {
                PageType = PageType.Product,
                Request = uri.AbsoluteUri,
                Type = ServiceType.DeepToUrl,
                RecordDate = DateTime.Now
            };

            if (!_parsedQueryString.ContainsKey("ContentId"))
            {
                parsedModel.HasError = true;
                parsedModel.ErrorDetails = $"Content id not found in deeplink: {parsedModel.Request}";
                return parsedModel;
            }

            try
            {
                parsedModel.ContentId = int.Parse(_parsedQueryString["ContentId"]);
                parsedModel.MerchantId = _parsedQueryString.ContainsKey("MerchantId") ? int.Parse(_parsedQueryString["MerchantId"]) : 0;
                parsedModel.CampaignId = _parsedQueryString.ContainsKey("CampaignId") ? int.Parse(_parsedQueryString["CampaignId"]) : 0;
            }
            catch (Exception ex)
            {
                parsedModel.HasError = true;
                parsedModel.ExceptionMessage = ex.Message;
                parsedModel.ErrorDetails = $"Error While Parsing Model From Deeplink: {parsedModel.Request}";
            }

            return parsedModel;
        }

        public DeeplinkServiceModel ParseSearchPageInput(Uri uri)
        {
            var parsedInput = new DeeplinkServiceModel
            {
                PageType = PageType.Search,
                Request = uri.AbsoluteUri,
                Type = ServiceType.DeepToUrl,
                RecordDate = DateTime.Now,
                HasError = false
            };

            if (!_parsedQueryString.ContainsKey("Query"))
            {
                parsedInput.HasError = true;
                parsedInput.ErrorDetails = $"Query not found in search deeplink: {parsedInput.Request}";
                return parsedInput;
            }

            parsedInput.SearchQuery = _parsedQueryString["Query"];

            return parsedInput;
        }

        public bool VerifyInput(string input)
        {
            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                var uri = new Uri(input);
                
                _parsedQueryString = Util.ParseQueryString(uri.Query);
                
                return string.Equals(uri.Scheme, Constants.DL_DOMAIN, StringComparison.OrdinalIgnoreCase)
                        && _parsedQueryString.ContainsKey("Page");
            }

            return false;
        }

        public async Task IndexRequest(DeeplinkServiceModel serviceModel)
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

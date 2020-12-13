using DotUrl.Actions;
using DotUrl.Controllers;
using DotUrl.Interfaces;
using DotUrl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using UrlService.Tests;
using UrlService.Tests.TestData;
using Xunit;

namespace UrlDot.Tests
{
    public class UrlServiceTest : IClassFixture<TestFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IAction<UrlServiceModel> _action;
        private readonly UrlServiceController _controller;
        private readonly ElasticClient _client;

        public UrlServiceTest(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _client = _serviceProvider.GetService<ElasticClient>();
            _action = new UrlAction(_client);
            _controller = new UrlServiceController(_action);
        }

        [Fact]
        public void Status_ShouldReturnOk()
        {
            var resp = _controller.Status();

            Assert.IsType<OkResult>(resp);
        }

        [Theory]
        [SampleUrlServiceTestDataAttribute]
        public void ExecuteUrlActionTest(string url, string deeplink)
        {
            var resp = _action.Execute(url);

            Assert.Equal(resp, deeplink);
        }

        [Theory]
        [SampleProductUrlsAttribute]
        public void VerifyInput_CorrectInput_ShouldReturnTrue(string url)
        {   
            var res = _action.VerifyInput(url);

            Assert.True(res);
        }

        [Theory]
        [InlineData("somefakeurltext")]
        [InlineData("http://test.trendyol.com")]
        [InlineData("https://hepsiburada.com/casio/saat-p-1925865?boutiqueId=439892&merchantId=105064")]
        public void VerifyInput_IncorrectInput_ShouldReturnFalse(string url)
        {
            var res = _action.VerifyInput(url);

            Assert.False(res);
        }

        [Theory]
        [SampleUrlServiceTestDataAttribute]
        public void ConvertToDeepLink_ShouldReturnDeepLink(string url, string deeplink)
        {
            var resp = _controller.ConvertToDeeplink(url);
            Assert.Equal(resp, deeplink);
        }

        [Theory]
        [SampleProductUrlsAttribute]
        public void InputParser_CorrectInput_ShouldReturnUrlServiceModel(string url)
        {
            var resp = _action.InputParser(url);

            Assert.False(resp.HasError);
        }

        [Theory]
        [SampleProductUrlsAttribute]
        public void ParseProductUrl_CorrectInput_ShouldHaveNoError(string url)
        {
            var uri = new Uri(url);

            var resp = _action.ParseProductPageInput(uri);

            Assert.False(resp.HasError);
        }

        [Theory]
        [InlineData("http://www.trendyol.com/tum--urunler/?q=elbise")]
        public void ParseProductUrl_IncorrectInput_ShouldHaveError(string url)
        {
            var uri = new Uri(url);

            var resp = _action.ParseProductPageInput(uri);

            Assert.True(resp.HasError);
        }

        [Theory]
        [SampleSearchUrlsAttribute]
        public void ParseSearchUrl_CorrectInput_ShouldHaveNoError(string url)
        {
            var uri = new Uri(url);

            var resp = _action.ParseSearchPageInput(uri);

            Assert.False(resp.HasError);
        }

        [Theory]
        [InlineData("http://www.trendyol.com/tum--urunler/?t=elbise")]
        public void ParseSearchUrl_IncorrectInput_ShouldHaveError(string url)
        {
            var uri = new Uri(url);

            var resp = _action.ParseSearchPageInput(uri);

            Assert.True(resp.HasError);
        }

        [Theory]
        [InlineData("http://www.trendyol.com/Hesabim/Favoriler")]
        public void ParseOtherUrlTest(string url)
        {
            var uri = new Uri(url);

            var resp = _action.ParseOtherPageInput(uri);

            Assert.NotNull(resp);
        }

        [Theory]
        [SampleSearchUrlsAttribute]
        public void SearchIndexTest(string url)
        {
            var resp = _action.SearchIndex(url);
            
            Assert.IsType<string>(resp);
        }
    }
}

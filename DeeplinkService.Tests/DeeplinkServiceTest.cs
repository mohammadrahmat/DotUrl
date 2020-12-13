using DeeplinkService.Tests.TestData;
using DotUrl.Actions;
using DotUrl.Controllers;
using DotUrl.Interfaces;
using DotUrl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using Xunit;

namespace DeeplinkService.Tests
{
    public class DeeplinkServiceTest : IClassFixture<TestFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IAction<DeeplinkServiceModel> _action;
        private readonly DeeplinkServiceController _controller;
        private readonly ElasticClient _client;

        public DeeplinkServiceTest(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _client = _serviceProvider.GetService<ElasticClient>();
            _action = new DeeplinkAction(_client);
            _controller = new DeeplinkServiceController(_action);
        }

        [Fact]
        public void Status_ShouldReturnOk()
        {
            var resp = _controller.Status();

            Assert.IsType<OkResult>(resp);
        }

        [Theory]
        [SampleDeeplinkServiceTestDataAttribute]
        public void ExecuteDeeplinkActionTest(string deeplink, string url)
        {
            var resp = _action.Execute(deeplink);

            Assert.Equal(resp, url);
        }

        [Theory]
        [SampleProductDeeplinksAttribute]
        public void VerifyInput_CorrectInput_ShouldReturnTrue(string deeplink)
        {
            var res = _action.VerifyInput(deeplink);

            Assert.True(res);
        }

        [Theory]
        [InlineData("somefaketext")]
        [InlineData("td://example")]
        public void VerifyInput_IncorrectInput_ShouldReturnFalse(string deeplink)
        {
            var res = _action.VerifyInput(deeplink);

            Assert.False(res);
        }

        [Theory]
        [SampleProductDeeplinksAttribute]
        public void InputParser_CorrectInput_ShouldNotHaveError(string deeplink)
        {
            var verified = _action.VerifyInput(deeplink);

            var resp = _action.InputParser(deeplink);

            Assert.False(verified && resp.HasError);
        }

        [Theory]
        [SampleDeeplinkServiceTestDataAttribute]
        public void ConvertToUrl_CorrectInput_ShouldReturnUrl(string deeplink, string url)
        {
            var resp = _controller.ConvertToUrl(deeplink);

            Assert.Equal(resp, url);
        }

        [Theory]
        [InlineData("ty://?Page=Orders")]
        public void ParseOtherDeeplinkInput(string deeplink)
        {
            var uri = new Uri(deeplink);

            var resp = _action.ParseOtherPageInput(uri);

            Assert.NotNull(resp);
        }

        [Theory]
        [SampleSearchDeeplinksAttribute]
        public void ParseSearchDeeplinks_CorrectInput_ShouldNotHaveError(string deeplink)
        {
            _action.VerifyInput(deeplink);

            var uri = new Uri(deeplink);

            var resp = _action.ParseSearchPageInput(uri);

            Assert.False(resp.HasError);
        }

        [Theory]
        [InlineData("ty://?Page=Search")]
        public void ParseSearchDeeplinks_IncorrectInput_ShouldHaveError(string deeplink)
        {
            _action.VerifyInput(deeplink);

            var uri = new Uri(deeplink);

            var resp = _action.ParseSearchPageInput(uri);

            Assert.True(resp.HasError);
        }

        [Theory]
        [SampleProductDeeplinksAttribute]
        public void ParseProductDeeplink_CorrectInput_ShouldNotHaveError(string deeplink)
        {
            _action.VerifyInput(deeplink);

            var uri = new Uri(deeplink);

            var resp = _action.ParseProductPageInput(uri);

            Assert.False(resp.HasError);
        }

        [Theory]
        [InlineData("ty://?Page=Product")]
        public void ParseProductDeeplink_IncorrectInput_ShouldHaveError(string deeplink)
        {
            _action.VerifyInput(deeplink);

            var uri = new Uri(deeplink);

            var resp = _action.ParseProductPageInput(uri);

            Assert.True(resp.HasError);
        }

        [Theory]
        [SampleSearchDeeplinksAttribute]
        public void ParseSearchDeeplink_CorrectInput_ShouldNotHaveError(string deeplink)
        {
            _action.VerifyInput(deeplink);

            var uri = new Uri(deeplink);

            var resp = _action.ParseSearchPageInput(uri);

            Assert.False(resp.HasError);
        }

        [Theory]
        [InlineData("ty://?Page=Search")]
        public void ParseSearchDeeplink_IncorrectInput_ShouldHaveError(string deeplink)
        {
            _action.VerifyInput(deeplink);

            var uri = new Uri(deeplink);

            var resp = _action.ParseSearchPageInput(uri);

            Assert.True(resp.HasError);
        }
    }
}

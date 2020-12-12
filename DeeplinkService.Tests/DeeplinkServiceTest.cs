using DeeplinkService.Tests.TestData;
using DotUrl.Actions;
using DotUrl.Controllers;
using DotUrl.Interfaces;
using DotUrl.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace DeeplinkService.Tests
{
    public class DeeplinkServiceTest
    {
        private readonly IAction<DeeplinkServiceModel> _action;
        private readonly DeeplinkServiceController _controller;

        public DeeplinkServiceTest()
        {
            _action = new DeeplinkAction();
            _controller = new DeeplinkServiceController(_action);
        }

        [Fact]
        public void Status_ShouldReturnOk()
        {
            var resp = _controller.Status();

            Assert.IsType<OkResult>(resp);
        }

        [Theory]
        [SampleDeeplinkServiceTestData]
        public void ExecuteDeeplinkActionTest(string deeplink, string url)
        {
            var resp = _action.Execute(deeplink);

            Assert.Equal(resp, url);
        }

        [Theory]
        [SampleProductDeeplinks]
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
        [SampleProductDeeplinks]
        public void InputParser_CorrectInput_ShouldNotHaveError(string deeplink)
        {
            var verified = _action.VerifyInput(deeplink);

            var resp = _action.InputParser(deeplink);

            Assert.False(verified && resp.HasError);
        }

        [Theory]
        [SampleDeeplinkServiceTestData]
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

            Assert.NotNull(deeplink);
        }

        [Theory]
        [SampleSearchDeeplinks]
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
        [SampleProductDeeplinks]
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
        [SampleSearchDeeplinks]
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

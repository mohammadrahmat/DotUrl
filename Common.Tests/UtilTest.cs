using Common.Tests.TestData;
using DotUrl.Helpers;
using DotUrl.Models;
using Xunit;

namespace Common.Tests
{
    public class UtilTest
    {
        [Theory]
        [InlineData("ığüşöçĞÜŞİÖÇ", "igusocGUSIOC")]
        public void NormalizeStringTest(string input, string output)
        {
            var normalizedString = Util.NormalizeText(input);

            Assert.Equal(normalizedString, output);
        }

        [Theory]
        [InlineData("?param1=value1&param2=value2", 2)]
        public void ParseQueryStringTest(string queryString, int paramCount)
        {
            var parsedQueryString = Util.ParseQueryString(queryString);

            Assert.Equal(parsedQueryString.Count, paramCount);
        }

        [Theory]
        [SampleUrlModelsAttribute]
        public void DeeplinkGenerator_ShouldReturn_Deeplink(UrlServiceModel urlModel, string deeplink)
        {
            var resp = Util.DeeplinkGenerator(urlModel);

            Assert.Equal(resp, deeplink);
        }

        [Theory]
        [SampleDeeplinkModelsAttribute]
        public void UrlGenerator_ShouldReturn_Url(DeeplinkServiceModel deeplinkMode, string url)
        {
            var resp = Util.UrlGenerator(deeplinkMode);

            Assert.Equal(resp, url);
        }
    }
}

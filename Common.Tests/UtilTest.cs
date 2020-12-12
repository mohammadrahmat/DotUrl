using DotUrl.Helpers;
using System;
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

        
    }
}

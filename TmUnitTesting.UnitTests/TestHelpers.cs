using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TmUnitTesting.UnitTests
{
    public class TestHelpers
    {
        [Fact]
        public void ConstructQueryString_ReturnsQueryString_WhenParametersProvided()
        {
            var pair1 = new KeyValuePair<string, string>("key1", "value1");
            var pair2 = new KeyValuePair<string, string>("key2", "value2");
            var parameters = new List<KeyValuePair<string, string>>()
            {
                pair1, pair2
            };
            var result = Helpers.ConstructQueryString(parameters);
            Assert.NotNull(result);
            Assert.Equal("?key1=value1&key2=value2", result.ToString());
        }

        [Fact]
        public void ConstructQueryString_ReturnsEmptyQueryString_WhenNoParametersProvided()
        {
            var parameters = new List<KeyValuePair<string, string>>();
            var result = Helpers.ConstructQueryString(parameters);
            Assert.NotNull(result);
            Assert.Equal("", result.ToString());
        }
    }
}

using Autofac.Extras.Moq;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TmUnitTesting.Models;
using TmUnitTesting.Repositories;
using Xunit;

namespace TmUnitTesting.UnitTests
{
    public class TestCatFactRepository
    {
        [Fact]
        public async void GetCatFacts_Calls_HttpClient_AndDeserializesResponse_WithNoQueryParameters()
        {
            using var mock = AutoMock.GetLoose();

            var serializedResponse = JsonSerializer.Serialize(new CatFactEntity()
            {
                Data = new List<CatFactDetails>()
                {
                    new CatFactDetails()
                    {
                        Fact = "test"
                    }
                }
            });
            var mockHttpClient = SetupHttpClient(serializedResponse);

            mock.Mock<IHttpClientFactory>()
                .Setup(client => client.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

            var catFactRepository = mock.Create<CatFactRepository>();
            var response = await catFactRepository.GetCatFacts(null, null);

            Assert.Equal("test", response.Data[0].Fact);
        }

        [Fact]
        public async void GetCatFacts_Calls_HttpClient_AndDeserializesResponse_WithQueryParameters()
        {
            using var mock = AutoMock.GetLoose();

            var serializedResponse = JsonSerializer.Serialize(new CatFactEntity()
            {
                Data = new List<CatFactDetails>()
                {
                    new CatFactDetails()
                    {
                        Fact = "test"
                    }
                }
            });
            var mockHttpClient = SetupHttpClient(serializedResponse);

            mock.Mock<IHttpClientFactory>()
                .Setup(client => client.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

            var catFactRepository = mock.Create<CatFactRepository>();
            var response = await catFactRepository.GetCatFacts(1, 1);

            Assert.Equal("test", response.Data[0].Fact);
        }

        private static HttpClient SetupHttpClient(string responseContent)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var mockResponse = new StringContent(responseContent, Encoding.UTF8, "application/json");

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = mockResponse });

            return new HttpClient(mockHttpMessageHandler.Object);
        }
    }
}

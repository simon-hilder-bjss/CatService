using Autofac.Extras.Moq;
using Moq;
using System.Text.Json;
using TmUnitTesting.Controllers;
using TmUnitTesting.Models;
using TmUnitTesting.Services;
using Xunit;

namespace TmUnitTesting.UnitTests
{
    public class TestCatsController
    {
        [Fact]
        public async void CatFacts_Calls_CatFactService()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var entity = new CatFactEntity()
            {
                Data = new List<CatFactDetails>()
            };
            mock.Mock<ICatFactService>()
                .Setup(service => service.GetCatFactsWithNameSubstitution(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync(entity);
            var sut = mock.Create<CatsController>();

            // Act
            await sut.CatFacts("test", 1, 2);

            // Assert
            mock.Mock<ICatFactService>().Verify(svc => svc.GetCatFactsWithNameSubstitution("test", 1, 2), Times.Once);
        }

        [Fact]
        public async void CatFacts_Returns_CatFactResponse_OnSuccess()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var entity = new CatFactEntity()
            {
                Data = new List<CatFactDetails>()
                    {
                        new CatFactDetails()
                        {
                            Fact = "test fact",
                            Length = 9
                        }
                    }
            };
            mock.Mock<ICatFactService>()
                .Setup(service => service.GetCatFactsWithNameSubstitution(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync(entity);

            // Act
            var sut = mock.Create<CatsController>();

            // Assert
            string actual = JsonSerializer.Serialize(await sut.CatFacts("test", 1, 2));
            string expected = JsonSerializer.Serialize(new CatFactResponse(entity, "test"));
            Assert.Equal(expected, actual);
        }
    }
}
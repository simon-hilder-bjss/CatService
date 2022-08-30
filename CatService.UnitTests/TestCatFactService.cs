using Autofac.Extras.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatService.Models;
using CatService.Repositories;
using CatService.Services;
using Xunit;

namespace CatService.UnitTests
{
    public class TestCatFactService
    {
        [Fact]
        public async void GetCatFactsWithNameSubstitution_Calls_CatFactRepository()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            var entity = new CatFactEntity()
            {
                Data = new List<CatFactDetails>()
            };
            mock.Mock<ICatFactRepository>()
                .Setup(service => service.GetCatFacts(It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync(entity);
            var sut = mock.Create<CatFactService>();

            // Act
            await sut.GetCatFactsWithNameSubstitution("test", 1, 2);

            // Assert
            mock.Mock<ICatFactRepository>().Verify(svc => svc.GetCatFacts(1, 2), Times.Once);
        }

        [Fact]
        public async void GetCatFactsWithNameSubstitution_Substitutes_CatWithName()
        {
            // Arrange
            var mock = AutoMock.GetLoose();
            var entity = new CatFactEntity()
            {
                Data = new List<CatFactDetails>()
                {
                    new CatFactDetails()
                    {
                        Fact = "replace word 'cat' but don't replace word 'dog'"
                    }
                }
            };
            mock.Mock<ICatFactRepository>()
                .Setup(service => service.GetCatFacts(It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync(entity);
            var sut = mock.Create<CatFactService>();

            // Act
            var result = await sut.GetCatFactsWithNameSubstitution("test", 1, 2);

            // Assert
            Assert.Equal("replace word 'test' but don't replace word 'dog'", result.Data[0].Fact);
        }
    }
}

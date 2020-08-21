using ApplicationLayer.Business;
using ApplicationLayer.Persistence;
using BrandDomain;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationLayerUnitTests
{
    public class BrandServiceUnitTests
    {
        [Fact]
        public async Task CreateBeer_GivenAValidBeer_ShouldReturnTheGuidCreatedByBrandPersistance()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var beer = new Beer();

            var mockedBrandPersistance = new Mock<IBrandPersistence>();
            mockedBrandPersistance.Setup(persistance => persistance.CreateBeer(It.IsAny<Beer>()))
                .ReturnsAsync(guid);

            var brandService = new BrandService(null, mockedBrandPersistance.Object);

            //Act
            var result = await brandService.CreateBeer(beer);

            //Assert
            Assert.Equal(guid.ToString(), result.ToString());
            mockedBrandPersistance.Verify(persistance => persistance.CreateBeer(It.IsAny<Beer>()), Times.Once);
        }

        [Fact]
        public async Task MarkBeerAsObsolete_GivenAValidBeerId_ShouldReturnTrue()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var mockedBrandPersistance = new Mock<IBrandPersistence>();
            mockedBrandPersistance.Setup(persistance => persistance.MarkBeerAsObsolete(It.IsAny<Guid>()))
                .ReturnsAsync(guid);

            var brandService = new BrandService(null, mockedBrandPersistance.Object);

            //Act
            var result = await brandService.MarkBeerAsObsolete(guid);

            //Assert
            Assert.True(result);
            mockedBrandPersistance.Verify(persistance => persistance.MarkBeerAsObsolete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task MarkBeerAsObsolete_GivenABeerId_WhenBrandPersistanceThrowsAnError_ShouldReturnFalse()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var mockedBrandPersistance = new Mock<IBrandPersistence>();
            mockedBrandPersistance.Setup(persistance => persistance.MarkBeerAsObsolete(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            var brandService = new BrandService(null, mockedBrandPersistance.Object);

            //Act
            var result = await brandService.MarkBeerAsObsolete(guid);

            //Assert
            Assert.False(result);
            mockedBrandPersistance.Verify(persistance => persistance.MarkBeerAsObsolete(It.IsAny<Guid>()), Times.Once);
        }
    }
}

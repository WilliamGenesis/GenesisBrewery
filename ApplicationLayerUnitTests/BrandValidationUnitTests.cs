using ApplicationLayer.Queries;
using ApplicationLayer.Validations;
using BrandDomain;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationLayerUnitTests
{
    public class BrandValidationUnitTests
    {
        [Fact]
        public async Task BeerExists_GivenAnExistingBeerId_ShouldReturnTrue()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var mockedBrandQuery = new Mock<IBrandQuery>();
            mockedBrandQuery.Setup(query => query.GetBeer(It.IsAny<Guid>()))
                .ReturnsAsync(new Beer());

            var brandValidation = new BrandValidation(mockedBrandQuery.Object);

            //Act
            var result = await brandValidation.BeerExists(guid);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BeerExists_GivenAnNonExistingBeerId_ShouldReturnFalse()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var mockedBrandQuery = new Mock<IBrandQuery>();
            mockedBrandQuery.Setup(query => query.GetBeer(It.IsAny<Guid>()))
                .ReturnsAsync((Beer) null);

            var brandValidation = new BrandValidation(mockedBrandQuery.Object);

            //Act
            var result = await brandValidation.BeerExists(guid);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateBeer_GivenValidBreweryId_ShouldReturnAllValidationResultsSuccessful()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var beer = new Beer
            {
                BreweryId = guid
            };

            var mockedBrandQuery = new Mock<IBrandQuery>();
            mockedBrandQuery.Setup(query => query.GetBrewery(It.IsAny<Guid>()))
                .ReturnsAsync(new Brewery());

            var brandValidation = new BrandValidation(mockedBrandQuery.Object);

            //Act
            var results = await brandValidation.ValidateBeer(beer);

            //Assert
            Assert.True(results.All(result => result == ValidationResult.Success));
        }

        [Fact]
        public async Task ValidateBeer_GivenAnInvalidBreweryId_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var beer = new Beer
            {
                BreweryId = guid
            };

            var expectedErrorMessage = "A beer must always be linked to an existing brewery";

            var mockedBrandQuery = new Mock<IBrandQuery>();
            mockedBrandQuery.Setup(query => query.GetBrewery(It.IsAny<Guid>()))
                .ReturnsAsync((Brewery) null);

            var brandValidation = new BrandValidation(mockedBrandQuery.Object);

            //Act
            var results = await brandValidation.ValidateBeer(beer);

            //Assert
            Assert.Contains(results, result => result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateBeer_GivenNoBreweryId_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var beer = new Beer
            {
                BreweryId = Guid.Empty
            };

            var expectedErrorMessage = "A beer must always be linked to a brewery";

            var brandValidation = new BrandValidation(null);

            //Act
            var results = await brandValidation.ValidateBeer(beer);

            //Assert
            Assert.Contains(results, result => result.ErrorMessage.Equals(expectedErrorMessage));
        }
    }
}

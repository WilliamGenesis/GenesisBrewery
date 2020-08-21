using ApplicationLayer.Business;
using ApplicationLayer.Queries;
using Moq;
using System;
using System.Threading.Tasks;
using WholesalerDomain;
using Xunit;

namespace ApplicationLayerUnitTests
{
    public class WholesalerServiceUnitTests
    {
        [Fact]
        public async Task GenerateQuote_GivenAQuoteRequestWithLessThan10Items_ShouldReturnARawPriceAndPriceEqualToExpectedPrice()
        {
            //Arrange
            var requestedItemId = Guid.NewGuid();

            var wholesalerStock = new[] {
                new StockItem{ItemId = requestedItemId, Quantity = 5, UnitPrice = 1.0f}
            };

            var expectedPrice = 5.0f;

            var quoteRequest = new QuoteRequest { WholesalerId = Guid.NewGuid(),RequestItems = new[] { new RequestItem { ItemId = requestedItemId, Quantity = 5 } } };

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerService = new WholesalerService(mockedWholesalerQuery.Object, null);

            //Act
            var result = await wholesalerService.GenerateQuote(quoteRequest);

            //Assert
            Assert.Equal(expectedPrice, result.Price);
            Assert.Equal(expectedPrice, result.RawPrice);
            Assert.Equal(0, result.Discount);
        }

        [Fact]
        public async Task GenerateQuote_GivenAQuoteRequestWithMoreThan10Items_ShouldReturnAPriceWith10PorcentDiscountFromRawPrice()
        {
            //Arrange
            var requestedItemId = Guid.NewGuid();

            var wholesalerStock = new[] {
                new StockItem{ItemId = requestedItemId, Quantity = 15, UnitPrice = 1.0f}
            };

            var expectedRawPrice = 15.0f;
            var expectedDiscout = 1.5f;
            var expectedPrice = 13.5f;

            var quoteRequest = new QuoteRequest { WholesalerId = Guid.NewGuid(), RequestItems = new[] { new RequestItem { ItemId = requestedItemId, Quantity = 15 } } };

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerService = new WholesalerService(mockedWholesalerQuery.Object, null);

            //Act
            var result = await wholesalerService.GenerateQuote(quoteRequest);

            //Assert
            Assert.Equal(expectedPrice, result.Price);
            Assert.Equal(expectedRawPrice, result.RawPrice);
            Assert.Equal(expectedDiscout, result.Discount);
        }

        [Fact]
        public async Task GenerateQuote_GivenAQuoteRequestWithMoreThan20Items_ShouldReturnAPriceWith20PorcentDiscountFromRawPrice()
        {
            //Arrange
            var requestedItemId = Guid.NewGuid();

            var wholesalerStock = new[] {
                new StockItem{ItemId = requestedItemId, Quantity = 25, UnitPrice = 1.0f}
            };

            var expectedRawPrice = 25.0f;
            var expectedDiscout = 5.0f;
            var expectedPrice = 20.0f;

            var quoteRequest = new QuoteRequest { WholesalerId = Guid.NewGuid(), RequestItems = new[] { new RequestItem { ItemId = requestedItemId, Quantity = 25 } } };

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerService = new WholesalerService(mockedWholesalerQuery.Object, null);

            //Act
            var result = await wholesalerService.GenerateQuote(quoteRequest);

            //Assert
            Assert.Equal(expectedPrice, result.Price);
            Assert.Equal(expectedRawPrice, result.RawPrice);
            Assert.Equal(expectedDiscout, result.Discount);
        }
    }
}

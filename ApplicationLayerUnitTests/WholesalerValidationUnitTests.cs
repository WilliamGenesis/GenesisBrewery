using ApplicationLayer.Queries;
using ApplicationLayer.Validations;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WholesalerDomain;
using Xunit;

namespace ApplicationLayerUnitTests
{
    public class WholesalerValidationUnitTests
    {
        #region ValidateStockItem
        [Fact]
        public async Task ValidateStockItem_GivenAValidStockItem_ShouldReturnAllSuccessValidationResults()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var stockItem = new StockItem
            {
                WholesalerId = guid,
                UnitPrice = 1.0f,
                Quantity = 10
            };

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateStockItem(stockItem);

            //Assert
            Assert.True(results.All(result => result == ValidationResult.Success));
        }

        [Fact]
        public async Task ValidateStockItem_GivenAStockItemWithInvalidWholesaler_ShouldReturAValidationResultWithExpectedMessage()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var stockItem = new StockItem
            {
                WholesalerId = guid,
                UnitPrice = 1.0f,
                Quantity = 10
            };

            var expectedMessage = "The wholesaler does not exist";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync((Wholesaler) null);

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateStockItem(stockItem);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedMessage));
        }

        [Fact]
        public async Task ValidateStockItem_GivenAStockItemWithAnEmptyWholesalerId_ShouldReturAValidationResultWithExpectedMessage()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var stockItem = new StockItem
            {
                WholesalerId = Guid.Empty,
                UnitPrice = 1.0f,
                Quantity = 10
            };

            var expectedMessage = "The wholesaler cannot be null or empty";

            var wholesalerValidation = new WholesalerValidation(null);

            //Act
            var results = await wholesalerValidation.ValidateStockItem(stockItem);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedMessage));
        }

        [Fact]
        public async Task ValidateStockItem_GivenAStockItemWithNegativeQuantity_ShouldReturAValidationResultWithExpectedMessage()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var stockItem = new StockItem
            {
                WholesalerId = guid,
                UnitPrice = 1.0f,
                Quantity = -1
            };

            var expectedMessage = "The quantity of a stock item cannot be negative";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateStockItem(stockItem);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedMessage));
        }

        [Fact]
        public async Task ValidateStockItem_GivenAStockItemWithNegativePrice_ShouldReturAValidationResultWithExpectedMessage()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var stockItem = new StockItem
            {
                WholesalerId = guid,
                UnitPrice = -1.0f,
                Quantity = 1
            };

            var expectedMessage = "The price of a stock item cannot be negative";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateStockItem(stockItem);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedMessage));
        }

        #endregion

        #region ValidateQuoteRequest

        [Fact]
        public async Task ValidateQuoteRequest_GivenAValidQuoteRequest_ShouldReturnAllSuccessValidationResults()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var requestedItems = new[]
            {
                new RequestItem{ItemId = itemId, Quantity = 1}
            };

            var wholesalerStock = new[]
            {
                new StockItem{ ItemId = itemId, Quantity = 1}
            };

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.True(results.All(result => result == ValidationResult.Success));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithoutWholesalerId_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.Empty;
            var itemId = Guid.NewGuid();

            var requestedItems = new[]
            {
                new RequestItem{ItemId = itemId, Quantity = 1}
            };

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = "The wholesaler cannot be null or empty";


            var wholesalerValidation = new WholesalerValidation(null);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithNonExistingWholesaler_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var requestedItems = new[]
            {
                new RequestItem{ItemId = itemId, Quantity = 1}
            };

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = "The wholesaler does not exist";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync((Wholesaler) null);

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithoutRequestItems_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var requestedItems = new RequestItem[0];

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = "Quote request cannot be empty";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithANullRequestItems_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            RequestItem[] requestedItems = null;

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = "Quote request cannot be empty";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithDuplicateElement_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var requestedItems = new[]
            {
                new RequestItem{ItemId = itemId, Quantity = 1},
                new RequestItem{ItemId = itemId, Quantity = 1}
            };

            var wholesalerStock = new[]
            {
                new StockItem{ ItemId = itemId, Quantity = 1}
            };

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = "All request items should be unique";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithItemNotSoldByWholesaler_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemRequestedId = Guid.NewGuid();
            var itemSoldId = Guid.NewGuid();

            var requestedItems = new[]
            {
                new RequestItem{ItemId = itemRequestedId, Quantity = 1}
            };

            var wholesalerStock = new[]
            {
                new StockItem{ ItemId = itemSoldId, Quantity = 1}
            };

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = "Requested Item is not sold by this wholesaler";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        [Fact]
        public async Task ValidateQuoteRequest_GivenAQuoteRequestWithItemLargerQuantityThanSoldQuantity_ShouldReturnAValidationResultWithExpectedErrorMessage()
        {
            //Arrange
            var wholesalerId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var requestedItems = new[]
            {
                new RequestItem{ItemId = itemId, Quantity = 2}
            };

            var wholesalerStock = new[]
            {
                new StockItem{ ItemId = itemId, Quantity = 1}
            };

            var quoteRequest = new QuoteRequest
            {
                WholesalerId = wholesalerId,
                RequestItems = requestedItems
            };

            var expectedErrorMessage = $"Requested quantity for item {itemId} is not available at this wholesaler";

            var mockedWholesalerQuery = new Mock<IWholesalerQuery>();
            mockedWholesalerQuery.Setup(query => query.GetWholesaler(It.IsAny<Guid>()))
                .ReturnsAsync(new Wholesaler());
            mockedWholesalerQuery.Setup(query => query.GetWholesalerStock(It.IsAny<Guid>()))
                .ReturnsAsync(wholesalerStock);

            var wholesalerValidation = new WholesalerValidation(mockedWholesalerQuery.Object);

            //Act
            var results = await wholesalerValidation.ValidateQuoteRequest(quoteRequest);

            //Assert
            Assert.Contains(results, result => result != null && result.ErrorMessage.Equals(expectedErrorMessage));
        }

        #endregion
    }
}

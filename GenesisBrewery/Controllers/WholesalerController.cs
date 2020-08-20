﻿using System;
using System.Threading.Tasks;
using ApplicationLayer.Business;
using ApplicationLayer.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WholesalerDomain;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace GenesisBrewery.Controllers
{
    [Route("v1/{Controller}")]
    public class WholesalerController : Controller
    {
        private IWholesalerService _wholesalerService;
        private IWholesalerValidation _wholesalerValidation;
        public WholesalerController(IWholesalerService wholesalerService, IWholesalerValidation wholesalerValidation)
        {
            _wholesalerService = wholesalerService;
            _wholesalerValidation = wholesalerValidation;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{wholesalerId}")]
        public async Task<IActionResult> GetWholesalerStock(Guid wholesalerId)
        {
            if(!await _wholesalerValidation.WholesalerExists(wholesalerId))
            {
                return new NotFoundObjectResult("Wholesaler not found");
            }

            return new OkObjectResult(await _wholesalerService.GetWholesalerStock(wholesalerId));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("StockItem/{itemId}")]
        public async Task<IActionResult> GetWholesalersByItem(Guid itemId)
        {
            return new OkObjectResult(await _wholesalerService.GetWholesalersByItem(itemId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CreateStockItem")]
        public async Task<IActionResult> CreateStockItem(StockItem stockItem)
        {
            var validationResults = await _wholesalerValidation.ValidateStockItem(stockItem);
            if (validationResults.Any(validationResult => validationResult != ValidationResult.Success))
            {
                return new BadRequestObjectResult(validationResults.Select(result => result.ErrorMessage));
            }

            return new OkObjectResult(await _wholesalerService.CreateStockItem(stockItem));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("UpdateStockItem")]
        public async Task<IActionResult> UpdateStockItem(StockItem stockItem)
        {
            if (!await _wholesalerValidation.StockItemExist(stockItem.Id))
            {
                return new NotFoundObjectResult("Stock item not found");
            }

            var validationResults = await _wholesalerValidation.ValidateStockItem(stockItem);
            if (validationResults.Any(validationResult => validationResult != ValidationResult.Success))
            {
                return new BadRequestObjectResult(validationResults.Select(result => result.ErrorMessage));
            }

            return new OkObjectResult(await _wholesalerService.UpdateStockItem(stockItem));
        }
    }
}

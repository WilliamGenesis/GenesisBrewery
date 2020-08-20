using ApplicationLayer.Business;
using ApplicationLayer.Validations;
using BrandDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace GenesisBrewery.Controllers
{
    [Route("v1/brand")]
    public class BrandController : Controller
    {
        private IBrandService _brandService;
        private IBrandValidation _brandValidation;
        public BrandController(IBrandService brandService, IBrandValidation brandValidation)
        {
            _brandService = brandService;
            _brandValidation = brandValidation;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetBreweries")]
        public async Task<IActionResult> GetBreweries()
        {
            return new OkObjectResult(await _brandService.GetBreweries());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("Brewery/{breweryId}/beers")]
        public async Task<IActionResult> GetBreweryBeers(Guid breweryId)
        {
            if (!await _brandValidation.BreweryExists(breweryId))
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(await _brandService.GetBeers(breweryId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBeer(Beer beer)
        {
            var validationResults = await _brandValidation.ValidateBeer(beer);
            if (validationResults.Any(validationResult => validationResult != ValidationResult.Success))
            {
                return new BadRequestObjectResult(validationResults.Select(result => result.ErrorMessage));
            }

            return new OkObjectResult(await _brandService.CreateBeer(beer));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkBeerAsObsolete(Guid beerId)
        {
            if(!await _brandValidation.BeerExists(beerId))
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(await _brandService.MarkBeerAsObsolete(beerId));
        }
    }
}

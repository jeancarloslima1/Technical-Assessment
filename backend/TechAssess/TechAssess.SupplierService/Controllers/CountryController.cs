using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechAssess.SupplierService.Models;
using TechAssess.SupplierService.Services;

namespace TechAssess.SupplierService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            var countries = await _countryService.GetAsync();
            return Ok(countries);
        }
    }
}

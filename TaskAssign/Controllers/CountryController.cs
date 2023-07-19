using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryBL _bl;

        public CountryController(CountryBL bl) 
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Country> GetCountryList()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public Country? GetCountry(Guid id)
        {
            return _bl.GetCountry(new Country() { Id = id });
        }

        [HttpPost]
        public Country? AddCountry(Country country)
        {
            return _bl.AddCountry(country);
        }

        [HttpPut]
        public Country? UpdateCountry(Country country)
        {
            return _bl.UpdateCountry(country);
        }

        [HttpDelete]
        public bool? DeleteCountry(Guid id)
        {
            return _bl.DeleteCountry(id);
        } 

    }
}

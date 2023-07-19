using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityBL _bl;

        public CityController(CityBL bl) 
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<City> GetAll()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public City? GetCity(Guid id)
        {
            return _bl.GetCity(new City() { Id = id });
        }

        [HttpPost]
        public City? AddCity(City city)
        {
            return _bl.AddCity(city);
        }

        [HttpPut]
        public City? UpdateCity(City city)
        {
            return _bl.UpdateCity(city);
        }

        [HttpDelete]
        public bool? DeleteCity(Guid id)
        {
            return _bl.DeleteCity(id);
        }

    }
}

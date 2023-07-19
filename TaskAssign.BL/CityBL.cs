using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class CityBL
    {
        private readonly CityRepository _repository;

        public CityBL(CityRepository repository) 
        {
            _repository = repository;
        }

        public List<City> GetAll()
        {
            return _repository.GetAll();
        }

        public City? AddCity(City city)
        {
            var existingCity = GetCity(new City() { CityName = city.CityName });
            if(existingCity != null)
            {
                throw new Exception("City Already Exist");
            }

            var id = _repository.AddCity(city);
            if(id != null)
            {
                city.Id= id;
            }
            return city;
        }

        public City? GetCity(City city) 
        {
            return _repository.GetCity(city);
        }

        public City? UpdateCity(City city)
        {
            var incorrectId = GetCity(new City() { Id = city.Id });
            if(incorrectId == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            return _repository.UpdateCity(city);
        }

        public bool? DeleteCity(Guid id)
        {
            return _repository.DeleteCity(id);
        }

    }
}

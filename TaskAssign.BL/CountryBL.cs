using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class CountryBL
    {
        private readonly CountryRepository _repository;

        public CountryBL(CountryRepository repository) 
        {
            _repository = repository;
        }
        public List<Country> GetAll()
        {
            return _repository.GetAll();
        }

        public Country? AddCountry(Country country)
        {
            var existingcountry = GetCountry(new Country() { CountryName = country.CountryName });
            if (existingcountry != null)
            {
                throw new Exception("Country Already Exists");
            }

            var id = _repository.AddCountry(country);
            if(id != null)
            {
                country.Id = id;
            }
            return country;
        }

        public Country? GetCountry(Country country)
        {
            return _repository.GetCountry(country);
        }

        public Country? UpdateCountry(Country country)
        {
            var existingcountry = GetCountry(new Country() { Id = country.Id });
            if (existingcountry == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            return _repository.UpdateCountry(country);
        }

        public bool? DeleteCountry(Guid id)
        {
            return _repository.DeleteCountry(id);
        }

    }
}

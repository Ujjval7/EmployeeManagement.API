using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class AddressTypeBL
    {
        private readonly AddressTypeRepository _repository;

        public AddressTypeBL(AddressTypeRepository repository) 
        {
            _repository = repository;
        }

        public List<AddressType> GetAll()
        {
            return _repository.GetAll();
        }

        public AddressType? AddAddressType(AddressType addressType)
        {
            var existingAddressType = GetAddressType(new AddressType() { AddressTypes = addressType.AddressTypes });
            if (existingAddressType != null)
            {
                throw new Exception("AddressType Already Exist");
            }

            var id = _repository.AddAddressType(addressType);
            if(id != null)
            {
                addressType.Id = id;
            }
            return addressType;
        }

        public AddressType? GetAddressType(AddressType addressType)
        {
            return _repository.GetAddressType(addressType);
        }

        public AddressType? UpdateAddressType(AddressType addressType)
        {
            var incorrectId = GetAddressType(new AddressType() { Id = addressType.Id });
            if(incorrectId == null)
            {
                throw new Exception("Pleasr Enter Correct Id");
            }

            var existingaddressType = GetAddressType(new AddressType() { AddressTypes = addressType.AddressTypes });
            if (existingaddressType != null)
            {
                throw new Exception("AddressType Already Exist");
            }

            return _repository.UpdateAddressType(addressType);
        }

        public bool? DeleteAddressType(Guid id)
        {
            return _repository.DeleteAddressType(id);
        }


    }
}

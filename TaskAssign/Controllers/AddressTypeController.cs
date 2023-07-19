using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypeController : ControllerBase
    {
        private readonly AddressTypeBL _bl;

        public AddressTypeController(AddressTypeBL bl) 
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<AddressType> GetAll()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public AddressType? GetAddressType(Guid id)
        {
            return _bl.GetAddressType(new AddressType() { Id = id });
        }

        [HttpPost]
        public AddressType? AddAddressType(AddressType addressType)
        {
            return _bl.AddAddressType(addressType);
        }

        [HttpPut]
        public AddressType? UpdateAddressType(AddressType addressType)
        {
            return _bl.UpdateAddressType(addressType);
        }

        [HttpDelete]
        public bool? DeleteAddressType(Guid id)
        {
            return _bl.DeleteAddressType(id);
        }

    }
}

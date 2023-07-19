using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly DesignationBL _bl;
        public DesignationController(DesignationBL bl) 
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Designation> GetAll()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public Designation? GetDesignation(Guid id)
        {
            return _bl.GetDesignation(new Designation() { Id = id });
        }

        [HttpPost]
        public Designation? AddDesignation(Designation designation)
        {
            return _bl.AddDesignation(designation);
        }

        [HttpPut]
        public Designation? UpdateDesignation(Designation designation)
        {
            return _bl.UpdateDesignation(designation);
        }

        [HttpDelete]
        public bool? DeleteDesignation(Guid id)
        {
            return _bl.DeleteDesignation(id);
        }

    }
}

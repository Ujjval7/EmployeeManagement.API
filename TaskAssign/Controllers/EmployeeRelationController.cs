using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRelationController : ControllerBase
    {
        private readonly EmployeeRelationBL _bl;

        public EmployeeRelationController(EmployeeRelationBL bl)
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<EmployeeRelation> GetRelations()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public EmployeeRelation? GetEmployeeRelation(Guid id)
        {
            return _bl.GetEmployeeRelation(new EmployeeRelation() { id = id });
        }

        [HttpPost]
        public EmployeeRelation? AddRelation(EmployeeRelation relation)
        {
            return _bl.AddRelation(relation);
        }

        [HttpPut]
        public EmployeeRelation? UpdateRelation(EmployeeRelation relation)
        {
            return _bl.UpdateRelation(relation);
        }

        [HttpDelete]
        public bool? DeleteRelation(Guid id)
        {
            return _bl.DeleteRelation(id);
        }

    }
}

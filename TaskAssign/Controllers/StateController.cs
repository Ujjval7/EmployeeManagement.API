using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateBL _bl;

        public StateController(StateBL bl) 
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<State> GetAll()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public State? GetState(Guid id)
        {
            return _bl.GetState(new State() { Id = id });
        }

        [HttpGet]
        [Route("GetByCountryId")]
        public List<State> GetStates(Guid id)
        {
            return _bl.GetStates(id);
        }

        [HttpPost]
        public State? AddState(State state) 
        {
            return _bl.AddState(state);
        }

        [HttpPut]
        public State? UpdateState(State state)
        {
            return _bl.UpdateState(state);
        }

        [HttpDelete]
        public bool? DeleteState(Guid id)
        {
            return _bl.DeleteState(id);
        }
    }
}

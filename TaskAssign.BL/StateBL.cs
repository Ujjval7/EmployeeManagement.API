using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class StateBL
    {
        private readonly StateRepository _repository;

        public StateBL(StateRepository repository) 
        {
            _repository = repository;
        }

        public List<State> GetAll() 
        {
            return _repository.GetAll();
        }

        public State? AddState(State state)
        {
            var existingState = GetState(new State() { StateName = state.StateName });
            if(existingState != null)
            {
                throw new Exception("State Already Exist");
            }

            var id = _repository.AddState(state);
            if(id != null)
            {
                state.Id = id;
            }
            return state;
        }

        public State? GetState(State state)
        {
            return _repository.GetState(state);
        }

        public List<State> GetStates(Guid id)
        {
            return _repository.getStates(id);
        }

        public State? UpdateState(State state)
        {
            var existingState = GetState(new State() { Id = state.Id });
            if(existingState == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            return _repository.UpdateState(state);
        }

        public bool? DeleteState(Guid id)
        {
            return _repository.DeleteState(id);
        }

    }
}

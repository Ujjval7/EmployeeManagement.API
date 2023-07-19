using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class EmployeeRelationBL
    {
        private readonly EmployeeRelationRepository _repository;

        public EmployeeRelationBL(EmployeeRelationRepository repository) 
        {
            _repository = repository;
        }

        public List<EmployeeRelation> GetAll()
        {
            return _repository.GetAll();
        }

        public EmployeeRelation? AddRelation(EmployeeRelation relation)
        {
            var existingRelation = GetEmployeeRelation(new EmployeeRelation() { Relation = relation.Relation });
            if (existingRelation != null)
            {
                throw new Exception("Relation Already Exists");
            }

            var id = _repository.AddRelation(relation);
            if(id != null)
            {
                relation.id = id;
            }
            return relation;
        }

        public EmployeeRelation? GetEmployeeRelation(EmployeeRelation relation)
        {
            return _repository.GetRelation(relation);
        }

        public EmployeeRelation? UpdateRelation(EmployeeRelation relation)
        {
            var incorrectId = GetEmployeeRelation(new EmployeeRelation() { id = relation.id });
            if(incorrectId == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            var existingRelation = GetEmployeeRelation(new EmployeeRelation() { Relation = relation.Relation });
            if(existingRelation != null)
            {
                throw new Exception("Relation Already Exists");
            }

            return _repository.UpdateRelation(relation);
        }

        public bool? DeleteRelation(Guid id)
        {
            return _repository.DeleteRelation(id);
        }

    }
}

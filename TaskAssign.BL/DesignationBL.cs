using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class DesignationBL
    {
        private readonly DesignationRepository _repository;

        public DesignationBL(DesignationRepository repository) 
        {
            _repository = repository;
        }

        public List<Designation> GetAll()
        {
            return _repository.GetAll();
        }

        public Designation? AddDesignation(Designation designation)
        {
            var existingdesignation = GetDesignation(new Designation() { DesignationName = designation.DesignationName });
            if (existingdesignation != null)
            {
                throw new Exception("Designation Already Exist");
            }

            var id = _repository.AddDesignation(designation);
            if (id != null)
            {
                designation.Id = id;
            }
            return designation;
        }

        public Designation? GetDesignation(Designation designation)
        {
            return _repository.GetDesignation(designation);
        }

        public Designation? UpdateDesignation(Designation designation)
        {
            var incorrectId = GetDesignation(new Designation() { Id = designation.Id });
            if(incorrectId == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            return _repository.UpgateDesignation(designation);
        }

        public bool? DeleteDesignation(Guid id)
        {
            return _repository.DeleteDesignation(id);
        }

    }
}

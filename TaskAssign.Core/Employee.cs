namespace TaskAssign.Core
{
    public class Employee
    {
        public Guid? Id { get; set; }
        public string EmpCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DOJ { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; } = string.Empty;
        public Guid DesignationId { get; set; } 
        public string Designation { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string EmergencyContactNo { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public bool IsDelete { get; set; } = false;
    }
}
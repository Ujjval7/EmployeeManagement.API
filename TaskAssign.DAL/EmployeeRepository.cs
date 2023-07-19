using Microsoft.Data.SqlClient;
using System.Data;
using TaskAssign.Core;

namespace TaskAssign.DAL
{
    public class EmployeeRepository
    {
        string connectionString;
        public EmployeeRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllEmployee
        public List<Employee> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<Employee> employeelist = new List<Employee>();
                string query = "Select [Employee].Id, EmpCode, FirstName, MiddleName, LastName, Email, DOJ, DOB, Gender, Phone, Mobile, EmergencyContactName, EmergencyContactNo, IsDelete, DesignationId, Designation.DesignationName as Designation From Employee" +
                                " inner join Designation on Employee.DesignationId = Designation.Id Where IsDelete = 0";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Guid.Parse(dataReader["Id"].ToString());
                    employee.EmpCode = dataReader["EmpCode"].ToString();
                    employee.FirstName = dataReader["FirstName"].ToString();
                    employee.MiddleName = dataReader["MiddleName"].ToString();
                    employee.LastName = dataReader["LastName"].ToString();
                    employee.Email = dataReader["Email"].ToString();
                    employee.Gender = dataReader["Gender"].ToString();
                    employee.DOB = Convert.ToDateTime(dataReader["DOB"]);
                    employee.DOJ = Convert.ToDateTime(dataReader["DOJ"]);
                    employee.DesignationId = Guid.Parse(dataReader["DesignationId"].ToString());
                    employee.Designation= dataReader["Designation"].ToString();
                    employee.Mobile = dataReader["Mobile"].ToString();
                    employee.Phone = dataReader["Phone"].ToString();
                    employee.EmergencyContactName = dataReader["EmergencyContactName"].ToString();
                    employee.EmergencyContactNo = dataReader["EmergencyContactNo"].ToString();
                    employee.IsDelete = Convert.ToBoolean(dataReader["IsDelete"]);

                    employeelist.Add(employee);
                }
                cmd.Connection.Close();

                return employeelist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddEmployee
        public Guid? AddEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [Employee](Id, EmpCode, FirstName, MiddleName, LastName, Email, DOJ, DOB, Gender, DesignationId, Phone, Mobile, EmergencyContactNo, EmergencyContactName, IsDelete) values(@Id, @EmpCode, @FirstName, @MiddleName, @LastName, @Email, @DOJ, @DOB, @Gender, @DesignationId, @Phone, @Mobile, @EmergencyContactNo, @EmergencyContactName, @IsDelete)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("EmpCode", SqlDbType.VarChar).Value = employee.EmpCode;
                cmd.Parameters.Add("FirstName", SqlDbType.VarChar).Value = employee.FirstName;
                cmd.Parameters.Add("MiddleName", SqlDbType.VarChar).Value = employee.MiddleName;
                cmd.Parameters.Add("LastName", SqlDbType.VarChar).Value = employee.LastName;
                cmd.Parameters.Add("Email", SqlDbType.VarChar).Value = employee.Email;
                cmd.Parameters.Add("DOJ", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("DOB", SqlDbType.Date).Value = employee.DOB;
                cmd.Parameters.Add("Gender", SqlDbType.VarChar).Value = employee.Gender;
                cmd.Parameters.Add("DesignationId", SqlDbType.UniqueIdentifier).Value = employee.DesignationId;
                cmd.Parameters.Add("Mobile", SqlDbType.VarChar).Value = employee.Mobile;
                cmd.Parameters.Add("Phone", SqlDbType.VarChar).Value = employee.Phone;
                cmd.Parameters.Add("EmergencyContactNo", SqlDbType.VarChar).Value = employee.EmergencyContactNo;
                cmd.Parameters.Add("EmergencyContactName", SqlDbType.VarChar).Value = employee.EmergencyContactName;
                cmd.Parameters.Add("IsDelete", SqlDbType.Bit).Value = false;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                if (Result > 0)
                    return id;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //GetEmployee
        public Employee? GetEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Employee? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select [Employee].Id, EmpCode, FirstName, MiddleName, LastName, Email, DOJ, DOB, Gender, Phone, Mobile, EmergencyContactName, EmergencyContactNo, IsDelete, DesignationId, Designation.DesignationName as Designation From Employee" +
                                " inner join Designation on Employee.DesignationId = Designation.Id";

                if (employee != null)
                {
                    if(employee.Id != null)
                    {
                        query += " Where [Employee].Id = @Id And IsDelete = 0";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = employee.Id
                        });
                    }

                    if(!string.IsNullOrEmpty(employee.Email))
                    {
                        query += " Where Email = @Email";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Email",
                            Value = employee.Email
                        });
                    }

                    if (!string.IsNullOrEmpty(employee.EmpCode))
                    {
                        query += " Where EmpCode = @EmpCode";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "EmpCode",
                            Value = employee.EmpCode
                        });
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                foreach(SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    returnValue = new Employee();
                    returnValue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnValue.EmpCode = dataReader["EmpCode"].ToString();
                    returnValue.FirstName = dataReader["FirstName"].ToString();
                    returnValue.MiddleName = dataReader["MiddleName"].ToString();
                    returnValue.LastName = dataReader["LastName"].ToString();
                    returnValue.Email = dataReader["Email"].ToString();
                    returnValue.Gender = dataReader["Gender"].ToString();
                    returnValue.DOB = Convert.ToDateTime(dataReader["DOB"]);
                    returnValue.DOJ = Convert.ToDateTime(dataReader["DOJ"]);
                    returnValue.DesignationId = Guid.Parse(dataReader["DesignationId"].ToString());
                    returnValue.Designation= dataReader["Designation"].ToString();
                    returnValue.Phone = dataReader["Phone"].ToString();
                    returnValue.Mobile = dataReader["Mobile"].ToString();
                    returnValue.EmergencyContactNo = dataReader["EmergencyContactNo"].ToString();
                    returnValue.EmergencyContactName = dataReader["EmergencyContactName"].ToString();

                    break;
                }
                cmd.Connection.Close();

                return returnValue;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //UpdateEmployee
        public Employee? UpdateEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update Employee set DesignationId=@DesignationId, Phone=@Phone, Mobile=@Mobile, EmergencyContactNo=@EmergencyContactNo, EmergencyContactName=@EmergencyContactName Where id=@id";

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = employee.Id;
                //cmd.Parameters.Add("Email", SqlDbType.VarChar).Value = employee.Email;
                cmd.Parameters.Add("DesignationId", SqlDbType.UniqueIdentifier).Value = employee.DesignationId;
                cmd.Parameters.Add("Phone", SqlDbType.VarChar).Value = employee.Phone;
                cmd.Parameters.Add("Mobile", SqlDbType.VarChar).Value = employee.Mobile;
                cmd.Parameters.Add("EmergencyContactNo", SqlDbType.VarChar).Value = employee.EmergencyContactNo;
                cmd.Parameters.Add("EmergencyContactName", SqlDbType.VarChar).Value = employee.EmergencyContactName;

                cmd.Connection.Open();
                var result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return employee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //DeleteEmployee
        public bool? DeleteEmployee(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Update Employee Set IsDelete=1 Where id=@id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected >0? true:false;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;

namespace TaskAssign.DAL
{
    public class EmployeeFamilyDetailRepository
    {
        string connectionString;
        public EmployeeFamilyDetailRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllEmployeeFamily
        public List<EmployeeFamilyDetail> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<EmployeeFamilyDetail> employeefamilydetaillist = new List<EmployeeFamilyDetail>();
                string query = "select EmployeeFamilyDetail.Id, EmployeeFamilyDetail.FirstName, EmployeeFamilyDetail.MiddleName, EmployeeFamilyDetail.LastName, EmployeeFamilyDetail.DOB, EmployeeId, RelationId, Employee.FirstName as EmployeeName, EmployeeRelation.Relation as Relation From EmployeeFamilyDetail" +
                    " inner join Employee on EmployeeFamilyDetail.EmployeeId = Employee.Id" +
                    " inner join EmployeeRelation on EmployeeFamilyDetail.RelationId = EmployeeRelation.Id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader(); 

                while (dataReader.Read())
                {
                    EmployeeFamilyDetail employeefamilydetail = new EmployeeFamilyDetail();
                    employeefamilydetail.Id = Guid.Parse(dataReader["Id"].ToString());
                    employeefamilydetail.EmployeeId = Guid.Parse(dataReader["EmployeeId"].ToString());
                    employeefamilydetail.FirstName = dataReader["FirstName"].ToString();
                    employeefamilydetail.MiddleName = dataReader["MiddleName"].ToString();
                    employeefamilydetail.LastName = dataReader["LastName"].ToString();
                    employeefamilydetail.DOB = Convert.ToDateTime(dataReader["DOB"]);
                    employeefamilydetail.RelationId = Guid.Parse(dataReader["RelationId"].ToString());
                    employeefamilydetail.EmployeeName = dataReader["EmployeeName"].ToString();
                    employeefamilydetail.Relation = dataReader["Relation"].ToString();

                    employeefamilydetaillist.Add(employeefamilydetail);
                }
                cmd.Connection.Close();

                return employeefamilydetaillist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddEmployeeFamily
        public Guid? AddEmployeeFamily(EmployeeFamilyDetail employeeFamilyDetail)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [EmployeeFamilyDetail](Id, EmployeeId, FirstName, MiddleName, LastName, DOB, RelationId) values(@Id, @EmployeeId, @FirstName, @MiddleName, @LastName, @DOB, @RelationId)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("EmployeeId", SqlDbType.UniqueIdentifier).Value = employeeFamilyDetail.EmployeeId;
                cmd.Parameters.Add("FirstName", SqlDbType.VarChar).Value = employeeFamilyDetail.FirstName;
                cmd.Parameters.Add("MiddleName", SqlDbType.VarChar).Value = employeeFamilyDetail.MiddleName;
                cmd.Parameters.Add("LastName", SqlDbType.VarChar).Value = employeeFamilyDetail.LastName;
                cmd.Parameters.Add("DOB", SqlDbType.Date).Value = employeeFamilyDetail.DOB;
                cmd.Parameters.Add("RelationId", SqlDbType.UniqueIdentifier).Value = employeeFamilyDetail.RelationId;

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

        //GetEmployeeFamily
        public EmployeeFamilyDetail? GetEmployeeFamily(EmployeeFamilyDetail employeeFamilyDetail)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                EmployeeFamilyDetail? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "select EmployeeFamilyDetail.Id, EmployeeFamilyDetail.FirstName, EmployeeFamilyDetail.MiddleName, EmployeeFamilyDetail.LastName, EmployeeFamilyDetail.DOB, EmployeeId, RelationId, Employee.FirstName as EmployeeName, EmployeeRelation.Relation as Relation From EmployeeFamilyDetail" +
                    " inner join Employee on EmployeeFamilyDetail.EmployeeId = Employee.Id" +
                    " inner join EmployeeRelation on EmployeeFamilyDetail.RelationId = EmployeeRelation.Id";

                if (employeeFamilyDetail != null)
                {
                    if(employeeFamilyDetail.Id != null)
                    {
                        query += " Where EmployeeFamilyDetail.Id = @Id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = employeeFamilyDetail.Id
                        });
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    returnValue = new EmployeeFamilyDetail();
                    returnValue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnValue.EmployeeId = Guid.Parse(dataReader["EmployeeId"].ToString());
                    returnValue.FirstName = dataReader["FirstName"].ToString();
                    returnValue.MiddleName = dataReader["MiddleName"].ToString();
                    returnValue.LastName = dataReader["LastName"].ToString();
                    returnValue.DOB = Convert.ToDateTime(dataReader["DOB"]);
                    returnValue.RelationId = Guid.Parse(dataReader["RelationId"].ToString());
                    returnValue.EmployeeName = dataReader["EmployeeName"].ToString();
                    returnValue.Relation = dataReader["Relation"].ToString();

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

        //UpdateEmployeeFamily
        public EmployeeFamilyDetail? UpdateEmployeeFamily(EmployeeFamilyDetail employeeFamilyDetail)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update EmployeeFamilyDetail set FirstName = @FirstName where id = @id";

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = employeeFamilyDetail.Id;
                cmd.Parameters.Add("FirstName", SqlDbType.VarChar).Value = employeeFamilyDetail.FirstName;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return employeeFamilyDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //DeleteEmployeeFamily
        public bool? DeleteEmployeeFamily(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From EmployeeFamilyDetail where id = @id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                
                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected > 0? true: false;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

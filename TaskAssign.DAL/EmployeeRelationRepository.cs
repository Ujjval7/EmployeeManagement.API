using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;

namespace TaskAssign.DAL
{
    public class EmployeeRelationRepository
    {
        string connectionString;

        public EmployeeRelationRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllRelation
        public List<EmployeeRelation> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<EmployeeRelation> employerelatiolist = new List<EmployeeRelation>();
                string query = "Select * From EmployeeRelation";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    EmployeeRelation employeeRelation = new EmployeeRelation();
                    employeeRelation.id = Guid.Parse(dataReader["id"].ToString());
                    employeeRelation.Relation = dataReader["Relation"].ToString();

                    employerelatiolist.Add(employeeRelation);
                }
                cmd.Connection.Close();

                return employerelatiolist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddRelation
        public Guid? AddRelation(EmployeeRelation employeeRelation)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [EmployeeRelation](Id, Relation) Values (@Id, @Relation)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("Relation", SqlDbType.VarChar).Value = employeeRelation.Relation;

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

        //GetRelation
        public EmployeeRelation? GetRelation(EmployeeRelation employeeRelation)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                EmployeeRelation? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select * From EmployeeRelation";

                if (employeeRelation != null)
                {
                    if (employeeRelation.id != null)
                    {
                        query += " Where id = @id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "id",
                            Value = employeeRelation.id
                        });
                    }

                    if (!string.IsNullOrEmpty(employeeRelation.Relation))
                    {
                        query += " Where Relation=@Relation";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Relation",
                            Value = employeeRelation.Relation
                        });
                    }

                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    returnValue = new EmployeeRelation();
                    returnValue.id = Guid.Parse(dataReader["Id"].ToString());
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

        //UpdateRelation
        public EmployeeRelation? UpdateRelation(EmployeeRelation employeeRelation)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update EmployeeRelation set Relation = @Relation where id = @id";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = employeeRelation.id;
                cmd.Parameters.Add("Relation", SqlDbType.VarChar).Value = employeeRelation.Relation;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return employeeRelation;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //DeleteRelation
        public bool? DeleteRelation(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From EmployeeRelation where id = @id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected > 0? true:false ;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

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
    public class DesignationRepository
    {
        string connectionString;
        public DesignationRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllDesignation
        public List<Designation> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<Designation> designationlist = new List<Designation>();
                string query = "Select * From Designation";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Designation designation = new Designation();
                    designation.Id = Guid.Parse(dataReader["Id"].ToString());
                    designation.DesignationName = dataReader["DesignationName"].ToString();

                    designationlist.Add(designation);
                }
                cmd.Connection.Close();

                return designationlist;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddDesignation
        public Guid? AddDesignation(Designation designation)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [Designation](Id, DesignationName) Values(@Id, @DesignationName)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("DesignationName", SqlDbType.VarChar).Value = designation.DesignationName;

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

        //GetDesignation
        public Designation? GetDesignation(Designation designation)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Designation? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select * From Designation";

                if (designation != null)
                {
                    if (designation.Id != null)
                    {
                        query += " Where Id = @Id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = designation.Id
                        });
                    }

                    if (!string.IsNullOrEmpty(designation.DesignationName))
                    {
                        query += " Where DesignationName = @DesignationName";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "DesignationName",
                            Value = designation.DesignationName
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
                    returnValue = new Designation();
                    returnValue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnValue.DesignationName = dataReader["DesignationName"].ToString();

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

        //UpdateDesignation
        public Designation? UpgateDesignation(Designation designation)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update Designation Set DesignationName = @DesignationName where id = @id";

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = designation.Id;
                cmd.Parameters.Add("designationName", SqlDbType.VarChar).Value = designation.DesignationName;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return designation;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Delete
        public bool? DeleteDesignation(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From Designation Where Id= @Id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected > 0 ? true : false;

            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}

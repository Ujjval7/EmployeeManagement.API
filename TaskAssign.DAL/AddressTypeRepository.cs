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
    public class AddressTypeRepository
    {
        string connectionString;
        public AddressTypeRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllAddressType
        public List<AddressType> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<AddressType> addresstypelist = new List<AddressType>();
                string query = "Select * From AddressType";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    AddressType addressType = new AddressType();
                    addressType.Id = Guid.Parse(dataReader["Id"].ToString());
                    addressType.AddressTypes = dataReader["AddressType"].ToString();

                    addresstypelist.Add(addressType);
                }
                cmd.Connection.Close();

                return addresstypelist;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddAddressType
        public Guid? AddAddressType(AddressType addressType)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [AddressType](id, AddressType) Values (@id, @AddressType)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("AddressType", SqlDbType.VarChar).Value = addressType.AddressTypes;

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

        //GetAddressType
        public AddressType? GetAddressType(AddressType addressType)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                AddressType? returnvalue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select * From AddressType";

                if(addressType != null)
                {
                    if(addressType.Id != null)
                    {
                        query += " Where Id = @Id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = addressType.Id
                        });
                    }

                    if(!string.IsNullOrEmpty(addressType.AddressTypes))
                    {
                        query += " Where AddressType = @AddressType";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "AddressType",
                            Value = addressType.AddressTypes
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

                while(dataReader.Read())
                {
                    returnvalue = new AddressType();
                    returnvalue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnvalue.AddressTypes = dataReader["AddressType"].ToString();

                    break;
                }
                cmd.Connection.Close();

                return returnvalue;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //UpdateAddressType
        public AddressType? UpdateAddressType(AddressType addressType)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update AddressType Set AddressType = @AddressType Where id = @id";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = addressType.Id;
                cmd.Parameters.Add("AddressType", SqlDbType.VarChar).Value = addressType.AddressTypes;

                cmd.Connection.Open();
                var result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return addressType;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //DeleteAddressType
        public bool? DeleteAddressType(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From AddressType Where id= @id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected > 0 ? true: false;

            }
            catch (Exception)
            {

                throw;
            }
        }  
    }
}

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
    public class CityRepository
    {
        string connectionString;
        public CityRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllCity
        public List<City> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<City> citylist = new List<City>();
                string query = "select [City].Id , City, StateId, State.State as State From City" +
                    " inner join State on City.StateId = State.Id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    City city = new City();
                    city.Id = Guid.Parse(dataReader["Id"].ToString());
                    city.CityName = dataReader["City"].ToString();
                    city.StateId = Guid.Parse(dataReader["StateId"].ToString());
                    city.State = dataReader["State"].ToString();

                    citylist.Add(city);
                }
                cmd.Connection.Close();

                return citylist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddCity
        public Guid? AddCity(City city)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [City](Id, City, StateId) Values(@Id, @City, @StateId)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("City", SqlDbType.VarChar).Value = city.CityName;
                cmd.Parameters.Add("StateId", SqlDbType.UniqueIdentifier).Value = city.StateId;

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

        //GetCity
        public City? GetCity(City city)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                City? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "select [City].Id , City, StateId, State.State as State From City" +
                    " inner join State on City.StateId = State.Id";

                if (city != null)
                {
                    if (city.Id != null)
                    {
                        query += " Where [City].Id = @Id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = city.Id
                        });
                    }

                    if (!string.IsNullOrEmpty(city.CityName))
                    {
                        query += " Where City = @City";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "City",
                            Value = city.CityName
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
                    returnValue = new City();
                    returnValue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnValue.CityName = dataReader["City"].ToString();
                    returnValue.StateId = Guid.Parse(dataReader["StateId"].ToString());
                    returnValue.State = dataReader["State"].ToString();

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

        //UpdateCity
        public City? UpdateCity(City city)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update City Set City = @City Where Id = @Id";

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = city.Id;
                cmd.Parameters.Add("City", SqlDbType.VarChar).Value = city.CityName;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return city;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //DeleteCity
        public bool? DeleteCity(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From City Where Id = @ID";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("Id",SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected > 0? true:false;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

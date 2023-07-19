using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskAssign.DAL
{
    public class CountryRepository
    {
        string connectionString;

        public CountryRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllCountry
        public List<Country> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<Country> countrylist = new List<Country>();
                string query = "Select * From Country";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Country country = new Country();
                    country.Id = Guid.Parse(dataReader["Id"].ToString());
                    country.CountryName = dataReader["CountryName"].ToString();

                    countrylist.Add(country);
                }
                cmd.Connection.Close();

                return countrylist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddCountry
        public Guid? AddCountry(Country country)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [Country](Id, CountryName) Values(@Id, @CountryName)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("CountryName", SqlDbType.VarChar).Value = country.CountryName;

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

        //GetCountry
        public Country? GetCountry(Country country)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Country? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select * From Country";

                if (country != null)
                {
                    if (country.Id != null)
                    {
                        query += " Where Id = @Id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = country.Id
                        });
                    }

                    if (!string.IsNullOrEmpty(country.CountryName))
                    {
                        query += " Where CountryName=@CountryName";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "CountryName",
                            Value = country.CountryName
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
                    returnValue = new Country();
                    returnValue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnValue.CountryName = dataReader["CountryName"].ToString();

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

        //UpdateCountry
        public Country? UpdateCountry(Country country)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update Country Set CountryName = @CountryName Where id = @id";

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = country.Id;
                cmd.Parameters.Add("CountryName", SqlDbType.VarChar).Value = country.CountryName;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return country;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //DeleteCountry
        public bool? DeleteCountry(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From Country Where id = @id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;

                cmd.Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return rowAffected > 0? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

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
    public class EmployeeAddressRepository
    {
        string connectionString;
        public EmployeeAddressRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllEmployeeAddress
        public List<EmployeeAddress> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<EmployeeAddress> employeeaddresslist = new List<EmployeeAddress>();
                string query = "Select [EmployeeAddress].Id, [EmployeeAddress].Address, [EmployeeAddress].EmployeeId, [EmployeeAddress].CountryId, [EmployeeAddress].StateId, [EmployeeAddress].CityId, [EmployeeAddress].AddressTypeId,Employee.FirstName as EmployeeName, Country.CountryName as Country, State.State as State,City.City as City, AddressType.AddressType as AddressType From [EmployeeAddress]" +
                                " inner join Employee on EmployeeAddress.EmployeeId = Employee.Id" +
                                " inner join Country on EmployeeAddress.CountryId = Country.Id" +
                                " inner join State on EmployeeAddress.StateId = State.Id" +
                                " inner join City on EmployeeAddress.CityId = City.Id" +
                                " inner join AddressType on EmployeeAddress.AddressTypeId = AddressType.Id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    EmployeeAddress employeeaddress = new EmployeeAddress();
                    employeeaddress.Id = Guid.Parse(dataReader["Id"].ToString());
                    employeeaddress.EmployeeId = Guid.Parse(dataReader["EmployeeId"].ToString());
                    employeeaddress.Address = dataReader["Address"].ToString();
                    employeeaddress.CityId = Guid.Parse(dataReader["CityId"].ToString());
                    employeeaddress.StateId = Guid.Parse(dataReader["StateId"].ToString());
                    employeeaddress.CountryId = Guid.Parse(dataReader["CountryId"].ToString());
                    employeeaddress.AddressTypeId = Guid.Parse(dataReader["AddressTypeId"].ToString());
                    employeeaddress.EmployeeName = dataReader["EmployeeName"].ToString();
                    employeeaddress.Country = dataReader["Country"].ToString();
                    employeeaddress.State = dataReader["State"].ToString();
                    employeeaddress.City = dataReader["City"].ToString();
                    employeeaddress.AddressType = dataReader["AddressType"].ToString();

                    employeeaddresslist.Add(employeeaddress);
                }
                cmd.Connection.Close();

                return employeeaddresslist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddEmployeeAddress
        public Guid? AddEmployeeAddress(EmployeeAddress employeeAddress)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [EmployeeAddress](Id, EmployeeId, Address, CountryId, StateId, CityId, AddressTypeId) Values(@Id, @EmployeeId, @Address, @CountryId, @StateId, @CityId, @AddressTypeId)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("EmployeeId", SqlDbType.UniqueIdentifier).Value = employeeAddress.EmployeeId;
                cmd.Parameters.Add("Address", SqlDbType.VarChar).Value = employeeAddress.Address;
                cmd.Parameters.Add("CountryId", SqlDbType.UniqueIdentifier).Value = employeeAddress.CountryId;
                cmd.Parameters.Add("StateId", SqlDbType.UniqueIdentifier).Value = employeeAddress.StateId;
                cmd.Parameters.Add("CityId", SqlDbType.UniqueIdentifier).Value = employeeAddress.CityId;
                cmd.Parameters.Add("AddressTypeId", SqlDbType.UniqueIdentifier).Value = employeeAddress.AddressTypeId;

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

        //GetEmployeeAddress
        public EmployeeAddress? GetEmployeeAddress(EmployeeAddress employeeAddress)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                EmployeeAddress? returnValue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "Select [EmployeeAddress].Id, [EmployeeAddress].Address, [EmployeeAddress].EmployeeId, [EmployeeAddress].CountryId, [EmployeeAddress].StateId, [EmployeeAddress].CityId, [EmployeeAddress].AddressTypeId,Employee.FirstName as EmployeeName, Country.CountryName as Country, State.State as State,City.City as City, AddressType.AddressType as AddressType From [EmployeeAddress]" +
                                " inner join Employee on EmployeeAddress.EmployeeId = Employee.Id" +
                                " inner join Country on EmployeeAddress.CountryId = Country.Id" +
                                " inner join State on EmployeeAddress.StateId = State.Id" +
                                " inner join City on EmployeeAddress.CityId = City.Id" +
                                " inner join AddressType on EmployeeAddress.AddressTypeId = AddressType.Id";

                if (employeeAddress != null)
                {
                    if(employeeAddress.Id != null)
                    {
                        query += " Where EmployeeAddress.id = @id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "id",
                            Value = employeeAddress.Id
                        });
                    }

                    //if(employeeAddress.EmployeeId != null)
                    //{
                    //    query += " And EmployeeId = @EmployeeId";
                    //    parameters.Add(new SqlParameter()
                    //    {
                    //        ParameterName = "EmployeeId",
                    //        Value = employeeAddress.EmployeeId
                    //    });
                    //}

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
                    returnValue = new EmployeeAddress();
                    returnValue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnValue.EmployeeId = Guid.Parse(dataReader["EmployeeId"].ToString());
                    returnValue.Address = dataReader["Address"].ToString();
                    returnValue.CityId = Guid.Parse(dataReader["CityId"].ToString());
                    returnValue.StateId = Guid.Parse(dataReader["StateId"].ToString());
                    returnValue.CountryId = Guid.Parse(dataReader["CountryId"].ToString());
                    returnValue.AddressTypeId = Guid.Parse(dataReader["AddressTypeId"].ToString());
                    returnValue.EmployeeName = dataReader["EmployeeName"].ToString();
                    returnValue.Country = dataReader["Country"].ToString();
                    returnValue.State = dataReader["State"].ToString();
                    returnValue.City = dataReader["City"].ToString();
                    returnValue.AddressType = dataReader["AddressType"].ToString();

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

        //UpdateEmployeeAddress
        public EmployeeAddress? UpdateEmployeeAddress(EmployeeAddress employeeAddress)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update EmployeeAddress set Address = @Address, AddressTypeId = @AddressTypeId Where id  = @id";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = employeeAddress.Id;
                cmd.Parameters.Add("Address", SqlDbType.VarChar).Value = employeeAddress.Address;
                cmd.Parameters.Add("AddressTypeId", SqlDbType.UniqueIdentifier).Value = employeeAddress.AddressTypeId;

                cmd.Connection.Open();
                var result = cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                return employeeAddress;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //DeleteEmployeeAddress
        public bool? DeleteEmployeeAddress(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From EmployeeAddress Where Id = @Id";

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

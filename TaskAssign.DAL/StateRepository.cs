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
    public class StateRepository
    {
        string connectionString;
        public StateRepository()
        {
            connectionString = "Data Source=Ujjval;Initial Catalog=Task;Integrated Security=True;TrustServerCertificate=true";
        }

        //GetAllState
        public List<State> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                List<State> statelist = new List<State>();
                string query = "select [State].Id, [State].State, [State].CountryId, Country.CountryName as CountryName From State"
                                + " inner join Country on State.CountryId = Country.Id";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    State state= new State();
                    state.Id = Guid.Parse(dataReader["Id"].ToString());
                    state.StateName = dataReader["State"].ToString();
                    state.CountryId = Guid.Parse(dataReader["CountryId"].ToString());
                    state.CountryName = dataReader["CountryName"].ToString();

                    statelist.Add(state);
                }
                cmd.Connection.Close();

                return statelist;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //AddState
        public Guid? AddState(State state)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                var id = System.Guid.NewGuid();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Insert Into [State](Id, State, CountryId) Values(@Id, @State, @CountryId)";

                cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
                cmd.Parameters.Add("State", SqlDbType.VarChar).Value = state.StateName;
                cmd.Parameters.Add("CountryId", SqlDbType.UniqueIdentifier).Value = state.CountryId;

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

        //GetState
        public State? GetState(State state)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                State? returnvalue = null;
                List<SqlParameter> parameters = new List<SqlParameter>();
                string query = "select [State].Id, [State].State, [State].CountryId, Country.CountryName as CountryName From State"
                                + " inner join Country on State.CountryId = Country.Id";

                if (state != null)
                {
                    if (state.Id != null)
                    {
                        query += " Where [Country].Id = @Id";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "Id",
                            Value = state.Id
                        });
                    }

                    if (!string.IsNullOrEmpty(state.StateName))
                    {
                        query += " Where State = @State";
                        parameters.Add(new SqlParameter()
                        {
                            ParameterName = "State",
                            Value = state.StateName
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
                    returnvalue = new State();
                    returnvalue.Id = Guid.Parse(dataReader["Id"].ToString());
                    returnvalue.StateName = dataReader["State"].ToString();
                    returnvalue.CountryId = Guid.Parse(dataReader["CountryId"].ToString());
                    returnvalue.CountryName = dataReader["CountryName"].ToString();

                    
                }
                cmd.Connection.Close();

                return returnvalue;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get State By CountryId
        public List<State> getStates(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {

                List<State> stateList = new List<State>();
                string query = "select [State].Id, [State].State, [State].CountryId, Country.CountryName as CountryName From State" +
                    " inner join Country on State.CountryId = Country.Id" +
                    " where CountryId = '" + id + "'";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                cmd.Connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    State state = new State();
                    state.Id = Guid.Parse(dataReader["Id"].ToString());
                    state.StateName = dataReader["State"].ToString();
                    state.CountryId = Guid.Parse(dataReader["CountryId"].ToString());
                    state.CountryName = dataReader["CountryName"].ToString();

                    stateList.Add(state);
                }

                cmd.Connection.Close();

                return stateList;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }

        //UpdateState
        public State? UpdateState(State state)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update State Set State = @State Where id = @id";

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = state.Id;
                cmd.Parameters.Add("State", SqlDbType.VarChar).Value = state.StateName;

                cmd.Connection.Open();
                var Result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                return state;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //DeleteState
        public bool? DeleteState(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                int rowAffected = 0;
                string query = "Delete From State Where id = @id";

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

using DefaultSite.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Data
{
    public class SqlDataProvider : IDataProvider
    {
        private const string LOG_CAT = "DAO";
        private readonly string connectionString;

        public SqlDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ExecuteCmd(
            string storedProc,
            Action<SqlParameterCollection> inputParamMapper,
            Action<IDataReader, short> singleRecordMapper,
            Action<SqlParameterCollection> returnParameters = null
        )
        {
            if (singleRecordMapper == null)
                throw new NullReferenceException("ObjectMapper is required.");

            if (String.IsNullOrEmpty(storedProc))
                throw new Exception("storedProc is required");

            SqlDataReader reader = null;
            SqlCommand cmd = null;
            SqlConnection conn = null;
            short numSets = 0;
            using (conn = GetConnection())
            {
                conn.Open();
                if (conn == null)
                    return;
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProc;

                if (inputParamMapper != null)
                    inputParamMapper(cmd.Parameters);

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (true)
                {
                    while (reader.Read())
                    {
                         singleRecordMapper(reader, numSets);
                    }

                    numSets += 1;

                    if (!reader.NextResult())
                        break;
                }

                reader.Close();

                if (returnParameters != null)
                    returnParameters(cmd.Parameters);
            }

            if (!reader.IsClosed)
                reader.Close();
        }

        public int ExecuteNonQuery(
            string storedProc,
            Action<SqlParameterCollection> paramMapper,
            Action<SqlParameterCollection> returnParameters = null
        )
        {
            SqlCommand cmd = null;
            SqlConnection conn = null;

            if (String.IsNullOrEmpty(storedProc))
            {
                throw new Exception("storedProc is required");
            }

            using (conn = GetConnection())
            {
                conn.Open();
                if (conn == null)
                {
                    return -1;
                }
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProc;

                if (paramMapper != null)
                    paramMapper(cmd.Parameters);

                int result = cmd.ExecuteNonQuery();

                if (returnParameters != null)
                    returnParameters(cmd.Parameters);

                return result;
            };
        }

        #region - Private Methods (Execute, GetCommand) -

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        #endregion - Private Methods (Execute, GetCommand) -
    }
}

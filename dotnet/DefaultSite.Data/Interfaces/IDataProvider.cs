using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Data.Providers
{
    public interface IDataProvider
    {
        void ExecuteCmd(
            string storedProc,
            Action<SqlParameterCollection> inputParamMapper, 
            Action<IDataReader, short> singleRecordMapper,
            Action<SqlParameterCollection> returnParameters = null);

        int ExecuteNonQuery(
            string storedProc,
            Action<SqlParameterCollection> inputParamMapper,
            Action<SqlParameterCollection> returnParams = null);

    }
}

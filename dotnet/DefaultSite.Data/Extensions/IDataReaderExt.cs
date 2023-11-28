using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultSite.Data.Extensions
{
    public static class IDataReaderExt
    {
        public static string GetSafeString(this IDataReader reader, Int32 ordinal, bool trim = true)
        {
            if (reader[ordinal] != null && reader[ordinal] != DBNull.Value)
            {
                if (trim)
                    return reader.GetString(ordinal).Trim();
                else
                    return reader.GetString(ordinal);
            }
            else
            {
                return null;
            }
        }
        public static int GetSafeInt32(this IDataReader reader, Int32 ordinal)
        {
            if (reader[ordinal] != null && reader[ordinal] != DBNull.Value)
            {
                return reader.GetInt32(ordinal);
            }
            else
            {
                return 0;
            }
        }

        public static int? GetSafeNullableInt32(this IDataReader reader, Int32 ordinal)
        {
            if (reader[ordinal] != null && reader[ordinal] != DBNull.Value)
            {
                return reader.GetInt32(ordinal);
            }
            else
            {
                return null;
            }
        }

        public static DateTime GetSafeDateTime(this IDataReader reader, Int32 ordinal)
        {
            if (reader[ordinal] != null && reader[ordinal] != DBNull.Value)
            {
                return reader.GetDateTime(ordinal);
            }
            else
            {
                return default(DateTime);
            }
        }


        public static T DeserializeObject<T>(this IDataReader reader, Int32 ordinal)
        {
            T result = default(T);

            if (reader[ordinal] != null && reader[ordinal] != DBNull.Value)
            {
                string myJson = reader.GetString(ordinal);

                if (!string.IsNullOrEmpty(myJson))
                {
                    result = JsonConvert.DeserializeObject<T>(myJson);
                }
            }
            return result;
        }
    }
}

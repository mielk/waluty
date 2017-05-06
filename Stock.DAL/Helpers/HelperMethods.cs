using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Helpers
{

    public static class HelperMethods
    {

        #region ToDbString (9 methods)

        public static string ToDbString(this int? value)
        {
            if (value == null) return "NULL";
            return value.ToString();
        }

        public static string ToDbString(this int value)
        {
            return value.ToString();
        }

        public static string ToDbString(this double? value)
        {
            if (value == null) return "NULL";
            return Math.Round((double)value, 5).ToString().Replace(',', '.');
        }

        public static string ToDbString(this double value)
        {
            return Math.Round(value, 5).ToString().Replace(',', '.');
        }

        public static string ToDbString(this bool value)
        {
            return (value ? "1" : "0");
        }

        public static string ToDbString(this bool? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            else
            {
                return ((bool)value ? "1" : "0");
            }
        }

        public static string ToDbString(this DateTime value)
        {
            return "'" + value + "'";
        }

        public static string ToDbString(this DateTime? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            else
            {
                return "'" + value.ToString() + "'";
            }
        }

        public static string ToDbString(this string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }

        #endregion ToDbString (9 methods)


        public static void ClearTable(this DbContext context, string dbName, string tableName)
        {
            const string DELETE_SQL_PATTERN = "DELETE FROM {0}.{1}";
            string deleteSql = string.Format(DELETE_SQL_PATTERN, dbName, tableName);
            context.Database.ExecuteSqlCommand(deleteSql);
        }


    }
}

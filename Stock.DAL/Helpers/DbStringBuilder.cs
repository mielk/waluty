using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.DAL.Helpers
{
    public class DbStringBuilder
    {

        private Dictionary<string, string> values = new Dictionary<string, string>();
        private List<string> where = new List<string>();
        public string DbAppendix { get; set; }
        private string TableAppendix = "{0}";

        public string getValue(string key)
        {
            string value = null;
            try
            {
                values.TryGetValue(key, out value);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            return value;

        }

        public void Add(string key, string value)
        {
            if (!values.ContainsKey(key)){
                values.Add(key, value.ToDbString());
            }
        }

        public void Add(string key, int? value)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, value.ToDbString());
            }
        }

        public void Add(string key, DateTime? value)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, value.ToDbString());
            }
        }

        public void Add(string key, bool? value)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, value.ToDbString());
            }
        }

        public void Add(string key, double? value)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, value.ToDbString());
            }
        }

        public void AddExpression(string key, string expression)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, expression);
            }
        }

        public void AddTimestamp()
        {
            var TimestampKey = "Timestamp";
            var TimestampValue = "NOW()";
            AddExpression(TimestampKey, TimestampValue);
        }

        public void AddWhere(string key, string value)
        {
            string expression = key + " = " + value.ToDbString();
            where.Add(expression);
        }

        public void AddWhere(string key, int value)
        {
            string expression = key + " = " + value.ToDbString();
            where.Add(expression);
        }

        public void AddWhere(string key, double value)
        {
            string expression = key + " = " + value.ToDbString();
            where.Add(expression);
        }

        public void AddWhere(string key, DateTime value)
        {
            string expression = key + " = " + value.ToDbString();
            where.Add(expression);
        }

        public void AddWhere(string key, bool value)
        {
            string expression = key + " = " + value.ToDbString();
            where.Add(expression);
        }

        public bool HasWhere(string condition)
        {
            var counter = where.Where(w => w.Equals(condition)).Count();
            return counter > 0;
        }

        public void Clear()
        {
            values.Clear();
        }

        public int CountElements()
        {
            return values.Count;
        }

        private string getAppendix()
        {
            if (DbAppendix != null && DbAppendix.Length > 0)
            {
                return DbAppendix + "." + TableAppendix;
            }
            else
            {
                return TableAppendix;
            }
        }

        private string getFieldsForInsert()
        {
            return string.Join(", ", values.Keys);
        }

        private string getValuesForInsert()
        {
            return string.Join(", ", values.Values);
        }

        private string getFieldValuesForUpdate()
        {
            List<string> expressions = new List<string>();
            foreach (var key in values.Keys)
            {
                string value = string.Empty;
                try
                {
                    values.TryGetValue(key, out value);
                    if (value.Length > 0)
                    {
                        expressions.Add(key + " = " + value);
                    }
                }
                catch (Exception ex)
                {

                }
                
            }
            return string.Join(", ", expressions);
        }

        private string getWhereString()
        {
            return string.Join(" AND ", where);
        }

        public string GenerateInsertSqlString()
        {
            const string SqlPattern = "INSERT INTO {0}({1}) VALUES({2});";
            return string.Format(SqlPattern, getAppendix(), getFieldsForInsert(), getValuesForInsert());
        }

        public string GenerateUpdateSqlString()
        {
            const string SqlPattern = "UPDATE {0} SET {1} WHERE {2};";
            string fieldValues = string.Empty;
            string where = string.Empty;
            string appendix = string.Empty;

            try
            {
                fieldValues = getFieldValuesForUpdate();
                where = getWhereString();
                appendix = getAppendix();
            }
            catch (Exception ex)
            {
            }

            return string.Format(SqlPattern, appendix, fieldValues, where);
        }

    }

}

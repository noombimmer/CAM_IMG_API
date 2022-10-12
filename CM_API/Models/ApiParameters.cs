using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CAPIs.Models
{
    public class ApiParameters
    {
    }
    public class ParameCAPIs
    {
        [Required]
        [MaxLength(255)]
        public string OBJECT { get; set; }
        public List<ParamsAPIColumns> Parameters { get; set; }
    }
    public class ParamsAPIColumns
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool Invert { get; set; }

        public string Condition { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }
    public class Utils
    {
        public  Utils()
        {

        }
        public static IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i).ToUpper());

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }
        public static Dictionary<string, string> CONDITIONS
        {
            get
            {
                var con = new Dictionary<string, string>();

                con.Add("GT", "> {0}");
                con.Add("GE", ">= {0}");

                con.Add("LT", "< {0}");
                con.Add("LE", "<= {0}");

                con.Add("EQ", "= {0}");

                con.Add("CT", "LIKE {0}");

                con.Add("BT", "BETWEEN {0} AND {1}");

                con.Add("IN", "IN ( {0} ) ");

                con.Add("ISNULL", "ISSNULL");

                return con;
            }
        }
        public static string Value1 { get; set; }
        public static string Value2 { get; set; }

        public static string Type { get; set; }
        public static string Name { get; set; }
        public static bool Invert { get; set; }
        public static string conditionStr { get; set; }
        public static string Condition
        {
            get
            {
                var tValue1 = Value1;
                var tValue2 = Value2;
                var tName = Name;
                string condition;
                Type = string.IsNullOrEmpty(Type) ? "TEXT" : Type;

                if (conditionStr == "CT")
                {
                    tValue1 = string.Format("%{0}%", tValue1.Replace('*', '%'));
                }

                var test = new string[] { "TEXT", "DATETIME" };

                if (test.Any(Type.ToUpper().Contains) && conditionStr.Contains("IN") != true)
                {
                    tValue1 = string.Format("'{0}'", tValue1);
                    tValue2 = string.Format("'{0}'", tValue2);
                }
                else if (conditionStr.Contains("IN"))
                {
                    tValue1 = string.Format("{0}", tValue1);
                }

                // Filled Bracket []
                if (Type.ToUpper() == "DATETIME")
                {
                    tName = string.Format("CAST({0} AS DATE)", tName);
                }
                else
                {
                    tName = string.Format("{0}", tName);
                }
                condition = string.Format("{0} {1}"
                    , tName
                    , string.Format(Utils.CONDITIONS[conditionStr], tValue1, tValue2)
                );

                if (Invert)
                {
                    condition = string.Format("NOT( {0} )", condition);
                }

                return condition;
            }
        }

    }

}
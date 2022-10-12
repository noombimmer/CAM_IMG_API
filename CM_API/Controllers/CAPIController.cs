using CAPIs.Areas.HelpPage;
using CAPIs.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Mvc;
using AcceptVerbsAttribute = System.Web.Mvc.AcceptVerbsAttribute;

namespace CAPIs.Controllersnet
{
    public class CAPIController : ApiController
    {
        public static int MAX_ROWS = 1000;
        public static string ConStr = ConfigurationManager.ConnectionStrings["THAPP_TCOMMON"].ConnectionString;
        public string cmdText = "";
        [APIRequest(Descrption = "Request with 1000 rows limited.")]

        public async Task<JsonResult> Post(ParameCAPIs param)
        {
            JsonResult rowData = new JsonResult();
            Regex regex = new Regex(@"^\w*[\(]\W*");

            this.cmdText = string.Format("SELECT TOP({0}) * FROM DBO.[{1}]", MAX_ROWS, param.OBJECT);
            if (regex.IsMatch(param.OBJECT)){
                this.cmdText = string.Format("SELECT TOP({0}) * FROM DBO.{1}", MAX_ROWS, param.OBJECT);
            }
            //regex.Match(param.OBJECT, @"/ ^\w *[\(][\']\w*[\'][\,]\w*[\)]/g) ");

            //if (param.OBJECT.Equals(/ ^\w *[\(][\']\w*[\'][\,]\w*[\)]/g) )
            
            

            if (param.Parameters != null && param.Parameters.Count > 0)
            {
                this.cmdText += Environment.NewLine + "WHERE " + Environment.NewLine;
                List<string> conditions = new List<string>();
                foreach (var lParam in param.Parameters)
                {
                    Utils.Name = lParam.Name;
                    Utils.Type = lParam.Type;
                    Utils.Value1 = lParam.Value1;
                    Utils.Value2 = lParam.Value2;
                    Utils.Invert = lParam.Invert;
                    Utils.conditionStr = lParam.Condition;
                    conditions.Add(Utils.Condition);
                }
                this.cmdText += string.Join(Environment.NewLine + " AND ", conditions);
                rowData.ContentType = this.cmdText;
            }
            using (SqlConnection sqldbConnection = new SqlConnection(ConStr))
            {
                using (var cmd = sqldbConnection.CreateCommand())
                {
                    if (sqldbConnection.State != System.Data.ConnectionState.Open)
                    {
                        await sqldbConnection.OpenAsync();
                    }
                    cmd.CommandText = this.cmdText;
                    DbDataReader reader = await cmd.ExecuteReaderAsync();
                    {
                        var model = Utils.Serialize((SqlDataReader)reader);
                        rowData.ContentEncoding = System.Text.Encoding.UTF8;
                        rowData.ContentType = "application/json";
                        rowData.Data = model;
                        rowData.MaxJsonLength = Int32.MaxValue;
                    }
                }
            }
            this.cmdText = "";
            return rowData;
        }
    }


}

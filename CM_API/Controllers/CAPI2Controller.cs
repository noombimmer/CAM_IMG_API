using CAPIs.Areas.HelpPage;
using CAPIs.Areas.HelpPage.ModelDescriptions;
using CAPIs.Controllersnet;
using CAPIs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace CAPIs.Controllers
{
    [Authorize]

    public class CAPI2Controller : ApiController
    {
        public static string ConStr = ConfigurationManager.ConnectionStrings["THAPP_TCOMMON"].ConnectionString;
        
        string cmdText = "";
        [APIRequest(Descrption = "Request unlimit rows.")]
        public async Task<JsonResult> Post(ParameCAPIs param)
        {
            JsonResult rowData = new JsonResult();
//            this.cmdText = string.Format("SELECT  * FROM [{0}]", param.OBJECT);
            Regex regex = new Regex(@"^\w*[\(]\W*");

            this.cmdText = string.Format("SELECT  * FROM DBO.[{0}]", param.OBJECT);
            if (regex.IsMatch(param.OBJECT))
            {
                this.cmdText = string.Format("SELECT * FROM DBO.{0}", param.OBJECT);
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
            cmdText = "";
            return rowData;
        }
    }

}

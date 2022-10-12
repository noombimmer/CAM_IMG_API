using CAPIs.Areas.HelpPage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using java.sql;
using sun.misc;
using sun.jdbc.odbc;
using ikvm.runtime;
using java.lang;
using java.util;
using Exception = java.lang.Exception;
using Microsoft.Ajax.Utilities;
using System.Collections;
using String = java.lang.String;
using RestSharp;
using CAPIs.Models;

namespace CAPIs.Controllers
{
    [Authorize]
    public class RAPI2Controller : ApiController
    {
        public static string ConStr = ConfigurationManager.ConnectionStrings["THAPP_TCOMMON"].ConnectionString;
        public static string ConStrJDBC = ConfigurationManager.AppSettings["SQLJDBC"].ToString();
        private Driver drvOpenEdgeJdbc = new com.ddtek.jdbc.openedge.OpenEdgeDriver();
        //private Driver drvSQLServerJdbc = new net.sourceforge.jtds.jdbc.Driver();
        //private string SQLConSTr = "jdbc:jtds:sqlserver://localhost:1433/MPLTDB;User=sa;Password=P@ssw0rd";
        private static string OpenEdgeConSTr = ConfigurationManager.AppSettings["OpenEdgeJdbcConnectionString"].ToString();

        private static string OpenEdgeServer = ConfigurationManager.AppSettings["OpenEdgeServer"].ToString();
        private static string OpenEdgeUser = ConfigurationManager.AppSettings["OpenEdgeUser"].ToString();
        private static string OpenEdgePassword = ConfigurationManager.AppSettings["OpenEdgePassword"].ToString();
        private static string OpenEdgePort = ConfigurationManager.AppSettings["OpenEdgePort"].ToString();
        private static string OpenEdgeDatabase = ConfigurationManager.AppSettings["OpenEdgeDatabase"].ToString();

        private Connection lConn;
        string cmdText = "";

        [APIRequest(Descrption = "Request unlimit rows.")]
        public async Task<JsonResult> Post(ParameCAPIs param)
        {
            JsonResult rowData = new JsonResult();
            Properties props = new Properties();

            string qadConnectionString = string.Format(OpenEdgeConSTr, OpenEdgeServer, OpenEdgePort, OpenEdgeDatabase);
            rowData.Data = qadConnectionString;
            props.setProperty("User", OpenEdgeUser);
            props.setProperty("Password", OpenEdgePassword);
            try
            {
                /*
                if (lConn != null)
                {
                    if (lConn.isClosed())
                    {
                        lConn = drvOpenEdgeJdbc.connect(qadConnectionString, props);
                    }
                }
                else
                {
                    lConn = drvOpenEdgeJdbc.connect(qadConnectionString, props);
                }
                */
                try
                {

                    Statement stmt = lConn != null ? lConn.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY) : null;


                    if (!param.OBJECT.ToUpper().Contains("QAD_ZKBUD_DET"))
                    {
                        rowData.ContentType = "Please specific object name to QAD_ZKBUD_DET only";
                        rowData.Data = new List<string>();
                        return rowData;
                    }
                    else
                    {
                        string baseCommandSQL = "SELECT \n" +
                            "  zkbud_det.zkbud_domain, \n" +
                            "  zkbud_det.zkbud_site, \n" +
                            "  zkbud_det.zkbud_bc_id, \n" +
                            "  zkbud_det.zkbud_predefine, \n" +
                            "  zkbud_det.zkbud_part, \n" +
                            "  zkbud_det.zkbud_lot, \n" +
                            "  zkbud_det.zkbud_ref, \n" +
                            "  zkbud_det.zkbud_qty, \n" +
                            "  zkbud_det.zkbud_pallet_id, \n" +
                            "  {fn CONVERT(SUBSTRING(zkbud_tr_type, 1, 100), SQL_VARCHAR)} as zkbud_tr_type,\n" +
                            "  zkbud_det.zkbud_order, \n" +
                            "  zkbud_det.zkbud_lnid, \n" +
                            "  zkbud_det.zkbud_date, \n" +
                            "  zkbud_det.zkbud_remark, \n" +
                            "  zkbud_det.zkbud_userid, \n" +
                            "  zkbud_det.zkbud_mod_date, \n" +
                            "  zkbud_det.zkbud_batch_no, \n" +
                            "  zkbud_det.zkbud_supp_ref, \n" +
                            "  zkbud_det.zkbud_pack_no, \n" +
                            "  zkbud_det.zkbud_shipper_no, \n" +
                            "  zkbud_det.zkbud__chr01, \n" +
                            "  zkbud_det.zkbud__chr02, \n" +
                            "  zkbud_det.zkbud__chr03, \n" +
                            "  zkbud_det.zkbud__chr04, \n" +
                            "  zkbud_det.zkbud__chr05, \n" +
                            "  zkbud_det.zkbud__chr06, \n" +
                            "  zkbud_det.zkbud__chr07, \n" +
                            "  zkbud_det.zkbud__chr08, \n" +
                            "  zkbud_det.zkbud__chr09, \n" +
                            "  zkbud_det.zkbud__chr10, \n" +
                            "  zkbud_det.zkbud__int01, \n" +
                            "  zkbud_det.zkbud__int02, \n" +
                            "  zkbud_det.zkbud__int03, \n" +
                            "  zkbud_det.zkbud__int04, \n" +
                            "  zkbud_det.zkbud__int05, \n" +
                            "  zkbud_det.zkbud__int06, \n" +
                            "  zkbud_det.zkbud__int07, \n" +
                            "  zkbud_det.zkbud__int08, \n" +
                            "  zkbud_det.zkbud__int09, \n" +
                            "  zkbud_det.zkbud__int10, \n" +
                            "  zkbud_det.zkbud__dec01, \n" +
                            "  zkbud_det.zkbud__dec02, \n" +
                            "  zkbud_det.zkbud__dec03, \n" +
                            "  zkbud_det.zkbud__dec04, \n" +
                            "  zkbud_det.zkbud__dec05, \n" +
                            "  zkbud_det.zkbud__dec06, \n" +
                            "  zkbud_det.zkbud__dec07, \n" +
                            "  zkbud_det.zkbud__dec08, \n" +
                            "  zkbud_det.zkbud__dec09, \n" +
                            "  zkbud_det.zkbud__dec10, \n" +
                            "  zkbud_det.zkbud__dte01, \n" +
                            "  zkbud_det.zkbud__dte02, \n" +
                            "  zkbud_det.zkbud__dte03, \n" +
                            "  zkbud_det.zkbud__dte04, \n" +
                            "  zkbud_det.zkbud__dte05, \n" +
                            "  zkbud_det.zkbud__dte06, \n" +
                            "  zkbud_det.zkbud__dte07, \n" +
                            "  zkbud_det.zkbud__dte08, \n" +
                            "  zkbud_det.zkbud__dte09, \n" +
                            "  zkbud_det.zkbud__dte10, \n" +
                            "  zkbud_det.zkbud__log01, \n" +
                            "  zkbud_det.zkbud__log02, \n" +
                            "  zkbud_det.zkbud__log03, \n" +
                            "  zkbud_det.zkbud__log04, \n" +
                            "  zkbud_det.zkbud__log05, \n" +
                            "  {fn CONVERT(SUBSTRING(zkbud_transno, 1, 200), SQL_VARCHAR)} as zkbud_transno,\n" +
                            "  zkbud_det.zkbud_loc, \n" +
                            "  zkbud_det.zkbud_cust, \n" +
                            "  zkbud_det.zkbud_pallet_type, \n" +
                            "  zkbud_det.zkbud_time\n" +
                            " FROM pub.{0}\n ";
                        param.OBJECT = "zkbud_det";
                        //this.cmdText = string.Format("SELECT  * FROM PUB.{0} ", param.OBJECT);
                        this.cmdText = baseCommandSQL;
                    }

                    if (param.Parameters != null && param.Parameters.Count > 0)
                    {
                        this.cmdText += " WHERE ";
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
                        this.cmdText += string.Join(" AND ", conditions);
                        rowData.ContentType = this.cmdText;
                    }

                    if (!this.cmdText.ToUpper().Contains("ZKBUD_DOMAIN"))
                    {
                        rowData.ContentType = "Please specific filter name : zkbud_domain ";
                        rowData.Data = new List<string>();
                        return rowData;
                    }
                    if (!this.cmdText.ToUpper().Contains("ZKBUD_SITE"))
                    {
                        rowData.ContentType = "Please specific filter name : zkbud_site ";
                        rowData.Data = new List<string>();
                        return rowData;
                    }
                    if (!this.cmdText.ToUpper().Contains("ZKBUD_SHIPPER_NO"))
                    {
                        rowData.ContentType = "Please specific filter name : zkbud_shipper_no ";
                        rowData.Data = new List<string>();
                        return rowData;
                    }
                    this.cmdText = this.cmdText.Replace("[", "");
                    this.cmdText = this.cmdText.Replace("]", "");
                    //ResultSet rs = stmt.executeQuery(cmdText);

                    try
                    {
                        //rowData.MaxJsonLength = rs.last() ? rs.getRow() : 0;
                        //rs.beforeFirst();
                        rowData.Data = this.cmdText;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.TraceError(ex.Message);
                        System.Diagnostics.Trace.TraceError(ex.getStackTrace().ToString());

                        rowData.MaxJsonLength = 0;
                    }

                }
                catch (SQLException ex)
                {
                    rowData.Data = qadConnectionString;
                    rowData.ContentType = ex.getMessage() + ": (" + qadConnectionString + ")";
                    //rowData.Data = this.cmdText;

                }
                catch (Exception exp)
                {
                    rowData.Data = qadConnectionString;
                    rowData.ContentType = exp.getMessage() + ": (" + qadConnectionString + ")";
                    //rowData.Data = this.cmdText;
                }
            }
            catch (Exception exp1)
            {
                rowData.Data = qadConnectionString;
                rowData.ContentType = exp1.getMessage() + ": (" + qadConnectionString + ")";
                rowData.Data = new List<string>();
            }
            /*
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
                        var model = Utils2.Serialize((SqlDataReader)reader);
                        rowData.ContentEncoding = System.Text.Encoding.UTF8;
                        rowData.ContentType = "application/json";
                        rowData.Data = model;

                    }
                }
            }
            cmdText = "";
            */
            return rowData;
        }

    }

}

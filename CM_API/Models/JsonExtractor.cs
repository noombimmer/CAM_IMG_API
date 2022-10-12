using java.sql;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAPIs.Models
{
    public static class JsonExtractor
    {
        public static List<JsonObject> getFormattedResult(ResultSet rs)
        {
            List<JsonObject> resList = new List<JsonObject>();
            try
            {
                // get column names
                ResultSetMetaData rsMeta = rs.getMetaData();
                int columnCnt = rsMeta.getColumnCount();
                List<string> columnNames = new List<string>();
                for (int i = 1; i <= columnCnt; i++)
                {
                    columnNames.Add(rsMeta.getColumnName(i).ToUpper());
                }

                while (rs.next())
                { // convert each object to an human readable JSON object
                    JsonObject obj = new JsonObject();
                    for (int i = 1; i <= columnCnt; i++)
                    {
                        string key = columnNames[i - 1].ToUpper();
                        string value = rs.getString(i);
                        obj.Add(key, value);
                    }
                    resList.Add(obj);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message);
                System.Diagnostics.Trace.TraceError(e.StackTrace.ToString());

            }
            finally
            {
                try
                {
                    rs.close();
                }
                catch (SQLException e)
                {
                    //e.printStackTrace();
                    System.Diagnostics.Trace.TraceError(e.Message);
                    System.Diagnostics.Trace.TraceError(e.getStackTrace().ToString());
                }
            }
            return resList;
        }
    }
}
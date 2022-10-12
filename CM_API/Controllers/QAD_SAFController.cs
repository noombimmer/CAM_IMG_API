using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace CAPIs.Controllers
{
    public class QAD_SAFController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_SAF param)
        //public IEnumerable<JsonResult> Post(QAD_SAF param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_SAF WHERE 1 = 1 ";
            PropertyInfo[]  tstRet = param.GetType().GetProperties();

            List<QAD_SAF> qAD_SAF = null;
            foreach (PropertyInfo info in tstRet)
            {
                string lName = info.Name;
                var objectValue = info.GetValue(param, null);
                if(objectValue != null)
                {
                    if (withCondition == false)
                    {
                        withCondition = true;
                    }
                    sqlSTring += "AND " + lName + "='"+ objectValue .ToString() + "' " ;
                }
            }
            if (withCondition == true)
            {
                qAD_SAF = db.QAD_SAF.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_SAF = db.QAD_SAF.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_SAF == null)
            {
                return Ok(qAD_SAF);
            }

            return Ok(qAD_SAF);
        }
    }
}

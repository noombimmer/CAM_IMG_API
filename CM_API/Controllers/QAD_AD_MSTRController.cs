using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using CAPIs;

namespace CAPIs.Controllers
{
    public class QAD_AD_MSTRController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_AD_MSTR param)
        //public IEnumerable<JsonResult> Post(QAD_SAF param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_AD_MSTR WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_AD_MSTR> qAD_AD_MSTR = null;
            foreach (PropertyInfo info in tstRet)
            {
                string lName = info.Name;
                var objectValue = info.GetValue(param, null);
                if (objectValue != null)
                {
                    if (withCondition == false)
                    {
                        withCondition = true;
                    }
                    sqlSTring += "AND " + lName + "='" + objectValue.ToString() + "' ";
                }
            }
            if (withCondition == true)
            {
                qAD_AD_MSTR = db.QAD_AD_MSTR.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_AD_MSTR = db.QAD_AD_MSTR.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_AD_MSTR == null)
            {
                return Ok(qAD_AD_MSTR);
            }

            return Ok(qAD_AD_MSTR);
        }
    }
}
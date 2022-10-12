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
    public class QAD_ITEMMASTERController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_ITEMMASTER param)
        //public IEnumerable<JsonResult> Post(QAD_SAF param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_ITEMMASTER WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_ITEMMASTER> qAD_ITEMMASTER = null;
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
                qAD_ITEMMASTER = db.QAD_ITEMMASTER.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_ITEMMASTER = db.QAD_ITEMMASTER.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_ITEMMASTER == null)
            {
                return Ok(qAD_ITEMMASTER);
            }

            return Ok(qAD_ITEMMASTER);
        }
    }
}
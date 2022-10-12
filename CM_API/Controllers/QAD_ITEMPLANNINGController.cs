using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CAPIs;

namespace CAPIs.Controllers
{
    public class QAD_ITEMPLANNINGController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_ITEMPLANNING param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_ITEMPLANNING WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_ITEMPLANNING> qAD_ITEMPLANNING = null;
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
                qAD_ITEMPLANNING = db.QAD_ITEMPLANNING.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_ITEMPLANNING = db.QAD_ITEMPLANNING.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_ITEMPLANNING == null)
            {
                return Ok(qAD_ITEMPLANNING);
            }

            return Ok(qAD_ITEMPLANNING);
        }

    }
}
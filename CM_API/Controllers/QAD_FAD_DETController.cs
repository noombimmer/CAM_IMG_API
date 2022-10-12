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
    public class QAD_FAD_DETController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_FAD_DET param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_FAD_DET WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_FAD_DET> qAD_FAD_DET = null;
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
                qAD_FAD_DET = db.QAD_FAD_DET.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_FAD_DET = db.QAD_FAD_DET.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_FAD_DET == null)
            {
                return Ok(qAD_FAD_DET);
            }

            return Ok(qAD_FAD_DET);
        }

    }
}
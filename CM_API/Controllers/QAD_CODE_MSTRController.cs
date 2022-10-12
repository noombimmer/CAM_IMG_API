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
    public class QAD_CODE_MSTRController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_CODE_MSTR param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_CODE_MSTR WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_CODE_MSTR> qAD_CODE_MSTR = null;
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
                qAD_CODE_MSTR = db.QAD_CODE_MSTR.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_CODE_MSTR = db.QAD_CODE_MSTR.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_CODE_MSTR == null)
            {
                return Ok(qAD_CODE_MSTR);
            }

            return Ok(qAD_CODE_MSTR);
        }

    }
}
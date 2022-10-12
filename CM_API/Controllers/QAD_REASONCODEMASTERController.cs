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
    public class QAD_REASONCODEMASTERController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_REASONCODEMASTER param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_REASONCODEMASTER WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_REASONCODEMASTER> qAD_REASONCODEMASTER = null;
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
                qAD_REASONCODEMASTER = db.QAD_REASONCODEMASTER.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_REASONCODEMASTER = db.QAD_REASONCODEMASTER.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_REASONCODEMASTER == null)
            {
                return Ok(qAD_REASONCODEMASTER);
            }

            return Ok(qAD_REASONCODEMASTER);
        }

    }
}
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
    public class QAD_INVENTORYLOCMASTERController : ApiController
    {
        private THAPP_TCOMMON_DEVEntities db = new THAPP_TCOMMON_DEVEntities();

        public IHttpActionResult Post(QAD_INVENTORYLOCMASTER param)
        {
            bool withCondition = false;
            string sqlSTring = "SELECT * FROM QAD_INVENTORYLOCMASTER WHERE 1 = 1 ";
            PropertyInfo[] tstRet = param.GetType().GetProperties();

            List<QAD_INVENTORYLOCMASTER> qAD_INVENTORYLOCMASTER = null;
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
                qAD_INVENTORYLOCMASTER = db.QAD_INVENTORYLOCMASTER.SqlQuery(sqlSTring).ToList();
            }
            else
            {
                qAD_INVENTORYLOCMASTER = db.QAD_INVENTORYLOCMASTER.SqlQuery(sqlSTring).ToList();
            }

            if (qAD_INVENTORYLOCMASTER == null)
            {
                return Ok(qAD_INVENTORYLOCMASTER);
            }

            return Ok(qAD_INVENTORYLOCMASTER);
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace CAPIs.Controllers
{
    public class COPCAMUploadImageController : ApiController
    {
        // GET: COPCAMUploadImage
        [HttpPost]
        public async Task<JsonResult> PostFile(HttpPostedFileBase image)
        {
            JsonResult rowData = new JsonResult();

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        DateTime dt = DateTime.Today;
                        string yearPath = dt.ToString("yyyy");
                        string monthPath = dt.ToString("MMMM");
                        string datePath =  dt.ToString("yyyyMMdd");
                        string pathRepo = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "OTHERS", yearPath, monthPath, datePath);
                        bool exists = System.IO.Directory.Exists(pathRepo);
                        if (!exists)
                            System.IO.Directory.CreateDirectory(pathRepo);
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "OTHERS", yearPath, monthPath, datePath, Path.GetFileName(image.FileName));
                        //string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(image.FileName));
                        image.SaveAs(path);
                        //ViewBag.FileStatus = "File uploaded successfully.";
                        //rowData.Data = "File uploaded successfully." + "-> " + image.FileName;
                        rowData.Data = new { FileStatus = "OK", filePath = image.FileName , ErrorMessage =""};
                    }
                    else
                    {
                        rowData.Data = new { FileStatus = "NOK", filePath = "null" , ErrorMessage = "image is null" };
                    }

                }
                catch (Exception ex)
                {

                    rowData.Data = new { FileStatus = "NOK", filePath = "null", ErrorMessage = ex.Message };

                }
                //return View("Index");

            }
            return rowData;
        }

        [HttpPost]
        public async Task<JsonResult> PostFileR16(HttpPostedFileBase image,string R16NO)
        {
            JsonResult rowData = new JsonResult();

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        DateTime dt = DateTime.Today;
                        string yearPath = dt.ToString("yyyy");
                        string monthPath = dt.ToString("MMMM");
                        string COPR16NO = R16NO;
                        string pathRepo = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "COPR16",yearPath, monthPath, COPR16NO);
                        bool exists = System.IO.Directory.Exists(pathRepo);
                        if (!exists)
                            System.IO.Directory.CreateDirectory(pathRepo);
                        
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "COPR16",yearPath, monthPath, COPR16NO, Path.GetFileName(image.FileName));
                        if (!System.IO.Directory.Exists(path))
                        {
                            image.SaveAs(path);
                        }
                        else
                        {
                            path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "COPR16", yearPath, monthPath, COPR16NO, Path.GetFileName(image.FileName + "_" + dt.ToString("yyyyMMddmmhhss")));
                            image.SaveAs(path);
                        }
                            
                        rowData.Data = new { FileStatus = "OK", filePath = image.FileName, ErrorMessage = "" };
                    }
                    else
                    {
                        rowData.Data = new { FileStatus = "NOK", filePath = "null", ErrorMessage = "image is null" };
                    }

                }
                catch (Exception ex)
                {

                    rowData.Data = new { FileStatus = "NOK", filePath = "null", ErrorMessage = ex.Message };

                }
                //return View("Index");

            }
            return rowData;
        }
        [HttpPost]
        public async Task<JsonResult> PostFileLAT(HttpPostedFileBase image, string LATNO)
        {
            JsonResult rowData = new JsonResult();

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        DateTime dt = DateTime.Today;

                        string yearPath = dt.ToString("yyyy");
                        string monthPath = dt.ToString("MMMM");
                        string COPR16NO = LATNO;
                        string pathRepo = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "COPLAT",yearPath, monthPath, COPR16NO);
                        bool exists = System.IO.Directory.Exists(pathRepo);
                        if (!exists)
                            System.IO.Directory.CreateDirectory(pathRepo);
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), "COPLAT", yearPath, monthPath, COPR16NO, Path.GetFileName(image.FileName));
                        image.SaveAs(path);
                        rowData.Data = new { FileStatus = "OK", filePath = image.FileName, ErrorMessage = "" };
                    }
                    else
                    {
                        rowData.Data = new { FileStatus = "NOK", filePath = "null", ErrorMessage = "image is null" };
                    }

                }
                catch (Exception ex)
                {

                    rowData.Data = new { FileStatus = "NOK", filePath = "null", ErrorMessage = ex.Message };

                }
                //return View("Index");

            }
            return rowData;
        }

        //[Route("api/COPCAMUploadImage/UploadFiles")]
        [HttpPost]
        public async Task<JsonResult> UploadFiles(HttpPostedFileBase FILE_IMAGE)
        {
            JsonResult rowData = new JsonResult();

            if (FILE_IMAGE.ContentLength > 0)
            {
                string rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");

                //string subPath = UID + "/" + hidden_cOPR16_FILE_IMPORT_FSIM_SEQDT;
                bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(rootPath + "/" ));
                if (!exists)
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(rootPath + "/" ));

                var fileName = Path.GetFileName(FILE_IMAGE.FileName);

                var path = Path.Combine(HttpContext.Current.Server.MapPath(rootPath + "/" ), fileName);
                /*
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                */
                FILE_IMAGE.SaveAs(path);

                rowData.Data = "file : " + FILE_IMAGE.ToString();
            }
            /*
            if (ModelState.IsValid)
            {
                try
                {


                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception ex)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                    rowData.Data = "Error while file uploading.";

                }
                //return View("Index");

            }
            */
            return rowData;
        }

 
    }
}
using Academy.Application.Extensions;
using Academy.Application.FilePath;
using Academy.Application.Security;
using Academy.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class UploaderController : Controller
    {
        //[HttpPost("Uploader/UploadImage")]
    
        //public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        //{
        //    if (upload.Length <= 0) return null;
        //    if (!upload.IsImage())
        //    {
        //        var NotImageMessage = "لطفا یک تصویر انتخاب کنید";
        //        dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
        //        return Json(NotImage);
        //    }

        //    var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName);
        //    //add to server
        //    upload.AddImageToServer(fileName, FilePaths.ArticleUploadPath, null, null);


        //    return Json(new
        //    {
        //        uploaded = true,
        //        url = $"{FilePaths.ArticlePath}{fileName}"
        //    });
        //}


        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;
            if (!upload.IsImage())
            {
                var NotImageMessage = "لطفا یک تصویر انتخاب کنید";
                dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
                return Json(NotImage);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName);
            //add to server
            upload.AddImageToServer(fileName, FilePaths.MyImagesUploadPath, null, null);


            var url = $"{FilePaths.MyImagesPath}{fileName}";


            return Json(new { uploaded = true, url });
        }
    }
}
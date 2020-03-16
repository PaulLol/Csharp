using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using testproj.Services;
using testproj.Models;
using testproj.Controllers;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace testproj.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("img")]
    public class ImgController : Controller
    {
        private ImgService _imgService;

        public ImgController(ImgService imgService)
        {
            _imgService = imgService;
        }

        [HttpPost]
        public IActionResult  UploadFile([FromForm]IFormFile file, string id, string collection = "employer")
        {
            if(collection == "topic")
            {
                id = "5cefc7e8050f1b09a02c75e2";
                _imgService.UploadFileTopic(file, id);

            }
            else
            if(collection == "employer")
            {
                id = "5cefd26bdc15f127b42aa5a1";
                _imgService.UploadFileEmployer(file, id);
            };

            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Download(string id = "5cefc7e8050f1b09a02c75e2", string collection = "employer")
        {
            if (collection == "topic")
            {
                return File(_imgService.DownloadFile(id, collection), _imgService.GetTopic(id).IconType);
            }
            else
             if (collection == "employer")
            {
                return File(_imgService.DownloadFile(id, collection), _imgService.GetEmployer(id).IconType);
            };
            return BadRequest();
        }
    }
}

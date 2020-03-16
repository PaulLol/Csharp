using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("music")]
    public class MusicController : Controller
    {
        private MusicService _musicService;

        public MusicController(MusicService fileService)
        {
            _musicService = fileService;
        }

        [HttpPost("file")]
        public void UploadFile([FromForm]IFormFile file)
        {
            _musicService.UploadFileMusic(file);
        }

        [HttpGet("groups")]
        public async Task<List<string>> GetAllGroup()
        {
            return await _musicService.Groups();
        }

        [HttpGet("{group}")]
        public async Task<List<string>> GetSongs(string group)
        {
            return await _musicService.FindSongs(group);
        }

        [HttpGet("{group}/{song}")]
        public async Task<string> GetIdFile(string group, string song)
        {
            return await _musicService.GetIdFile(group, song);
        }

        [HttpGet("file/{idfile}")]
        public IActionResult Download(string idFile)
        {
            return File(_musicService.Downloadfile(idFile), _musicService.GetFileType(idFile));
        }

        [HttpPost("file/user/{id}")]
        public List<string> UploadPrivateFile([FromForm]IFormFile file, string id)
        {
            return _musicService.UploadPrivateFileMusic(file, id);
        }

        [HttpGet("file/{idfile}/application/{fileType}")]
        public IActionResult DownloadPrivate(string idFile, string fileType)
        {
            return File(_musicService.Downloadfile(idFile), "application/"+fileType);
        }
    }
}
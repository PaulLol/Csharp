using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Auth;
using Backend.Models;
using Backend.Other;
using Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("users")]
    public class UsersController : Controller
    {
        private UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [Route("list")]
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _usersService.Get();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            string token = this.Request.Headers["Authorization"];

            if (token == null)
                return BadRequest(new { error = "No authorization header" });

            bool isValid = JWTAuth.ValidateToken(token, id);
            if (!isValid)
            {
                this.Response.StatusCode = 403;
                return new ObjectResult(new { error = "Invalid access token" });
            }


            User user = _usersService.GetForId(id);
            return new ObjectResult(new
            {
                user.Id,
                user.Mail,
                user.Password,
            });
        }

        [HttpPost("{mail}")]
        public IActionResult Login([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User _user = _usersService.GetForMail(user.Mail);

            if (_user == null)
                return new UnauthorizedResult();

            string accessToken = JWTAuth.GenerateToken(_user);
            return new ObjectResult(new
            {
                _user.Id,
                _user.Mail,
                _user.Password,
                access_token = accessToken,
            });
        }


        [HttpGet("personalmusic/{id}")]
        public List<string> GetListPersonalMusic(string id)
        {
            return _usersService.FindListPersonalMusic(id);
        }

        [HttpGet("personalmusicid/{id}")]
        public List<string> GetListPersonalMusicId(string id)
        {
            return _usersService.FindListPersonalMusicId(id);
        }

        [HttpGet("personalmusicfiletype/{id}")]
        public List<string> GetListPersonalMusicFileType(string id)
        {
            return _usersService.FindListPersonalMusicFileType(id);
        }


        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_usersService.GetForMail(user.Mail) != null)
                return BadRequest(new { error = "Mail already in use" });

            _usersService.Create(user);

            string accessToken = JWTAuth.GenerateToken(user);
            return new ObjectResult(new
            {
                user.Id,
                user.Mail,
                access_token = accessToken
            });
        }

        [HttpPost("sendmail")]
        public IActionResult SendMail([FromBody] Temp temp)
        {
            OtherMetods oth = new OtherMetods();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_usersService.GetForMailTemp(temp.Mail) != null)
                return BadRequest(new { error = "Mail already in use" });

            temp.Password = oth.RandomString();
            _usersService.SendMail(temp);
            oth.SendMail(temp);

            string accessToken = JWTAuth.GenerateToken(temp);

            return new ObjectResult(new
            {
                temp.Mail,
                temp.Password,
                access_token = accessToken
            });
        }

        [HttpDelete("temp/{mail}")]
        public IActionResult DeleteTemp(string mail)
        {
            var user = _usersService.GetForMailTemp(mail);

            if (user == null)
            {
                return NotFound();
            }

            _usersService.RemoveTemp(user.Mail);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult ChangePassword([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound();
            }

            _usersService.Update(user.Id, user);

            return Ok();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using testproj.Models;
using testproj.Services;

namespace testproj.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("employer")]
    public class EmployerController : Controller
    {
        private EmployerService _employerService;
        public EmployerController(EmployerService employerService)
        {
            _employerService = employerService;
        }

        [Route("list")]
        [HttpGet]
        public ActionResult<List<Employer>> Get()
        {
            return _employerService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Employer> Get(string id)
        {
            var employer = _employerService.Get(id);
            if (employer == null)
            {
                return NotFound();
            }

            return employer;
        }


        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Employer employerIn)
        {
            var employer = _employerService.Get(id);

            if (employer == null)
            {
                return NotFound();
            }

            _employerService.Update(id, employerIn);

            return Ok();
        }

        [HttpPost]
        public ActionResult<Employer> Create([FromBody]Employer employer)
        {
            _employerService.Create(employer);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var employer = _employerService.Get(id);

            if (employer == null)
            {
                return NotFound();
            }

            _employerService.Remove(employer.Id);

            return NoContent();
        }

    }
}
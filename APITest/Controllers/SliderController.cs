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
    [Route("slider")]
    public class SliderController : Controller
    {
        private SliderService _sliderService;
        public SliderController(SliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [Route("list")]
        [HttpGet]
        public ActionResult<List<Slider>> Get()
        {
            return _sliderService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Slider> Get(string id)
        {
            var slider = _sliderService.Get(id);
            if (slider == null)
            {
                return NotFound();
            }

            return slider;
        }


        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Slider sliderIn)
        {
            var slider = _sliderService.Get(id);

            if (slider == null)
            {
                return NotFound();
            }

            _sliderService.Update(id, sliderIn);

            return Ok();
        }

        [HttpPost]
        public ActionResult<Slider> Create([FromBody]Slider slider)
        {
            _sliderService.Create(slider);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var slider = _sliderService.Get(id);

            if (slider == null)
            {
                return NotFound();
            }

            _sliderService.Remove(slider.Id);

            return NoContent();
        }
    }
}
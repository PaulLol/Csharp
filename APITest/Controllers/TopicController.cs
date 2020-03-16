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
    [Route("topic")]
    public class TopicController : Controller
    {
        private TopicService _topicService;
        public TopicController(TopicService topicService)
        {
            _topicService = topicService;
        }

        [Route("list")]
        [HttpGet]
        public ActionResult<List<Topic>> Get()
        {
            return _topicService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Topic> Get(string id)
        {
            var topic = _topicService.Get(id);
            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }


        [HttpPut("{id}")]
        public IActionResult Update(string id,  [FromBody]Topic topicIn)
        {
            var topic = _topicService.Get(id);

            if (topic == null)
            {
                return NotFound();
            }

            _topicService.Update(id, topicIn);

            return Ok();
        }

        [HttpPost]
        public ActionResult<Topic> Create([FromBody]Topic topic)
        {
            _topicService.Create(topic);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var topic = _topicService.Get(id);

            if (topic == null)
            {
                return NotFound();
            }

            _topicService.Remove(topic.Id);

            return NoContent();
        }

    }

    
}
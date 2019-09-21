using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using TextReader.Managers;
using TextReader.WebApi.Models;

namespace TextReader.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {

        private readonly ITextReaderManager manager;

        public TextController(ITextReaderManager manager)
        {
            this.manager = manager;
        }

        // POST api/text/sort
        [HttpPost]
        [Route("Sort")]
        public IActionResult Sort([FromBody] TextSortItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = manager.Sort(value.Text, (Managers.SortOption)value.SortOption);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST api/text/statistics
        [HttpPost]
        [Route("Statistics")]
        public IActionResult Statistics([FromBody] TextStatisticsItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = manager.GetStatistics(value.Text);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
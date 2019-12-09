using System.Threading.Tasks;
using Ideas.Eventos.Hoteis.Jobs;
using Ideas.Eventos.Hoteis.Core.Model;
using Ideas.Eventos.Hoteis.Crawler;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Ideas.Eventos.Hoteis.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {       
        private IJobsService _jobs;
        private ICrawlerService _crawlerService; 

        public JobsController(IJobsService jobs, ICrawlerService crawlerService)
        {
            _jobs = jobs;
            _crawlerService = crawlerService; 
        }

        [HttpPost("crawler-job")]
        public ActionResult CrawlerJob([FromBody] JobFilterModel jobFilter)
        {
            _jobs.CrawlerJob(jobFilter.Local);
            return Ok(jobFilter); 
        }

        [HttpGet("update-brazil")]
        public ActionResult UpdateBrazil()
        {
            _crawlerService.UpdateBrazilianPlaces(); 
            return Ok();
        }

        [HttpGet("update-db")]
        public ActionResult UpdateDB()
        {
            _crawlerService.UpdateDB("e");
            return Ok();
        }

        [HttpGet("update-by-country/{pais}")]
        public async Task<ActionResult> UpdateByCountry([FromRoute]string pais)
        {
            var upt =  await _crawlerService.UpdateByCountry(pais, 5); 
            return Ok(upt);
        }

        [HttpPost("get-images")]
        public ActionResult GetImages([FromBody] FilterModel filter)
        {
            _jobs.GetHotelImages(filter);
            return Ok();
        }


        [HttpPost("generate-json-places")]
        public ActionResult GenerateJsonPlaces()
        {
            _jobs.GenerateJsonPlaces();
            return Ok();
        }
    }
}
﻿using Ideas.Eventos.Hoteis.Core.Interfaces;
using Ideas.Eventos.Hoteis.Core.Model;
using Ideas.Eventos.Hoteis.Core.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Ideas.Eventos.Hoteis.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private IBasicInfoRepository _repos;
        public HotelController(IBasicInfoRepository repos)
        {
            _repos = repos;
        }

        [HttpPost("list-accor")]
        public ActionResult GetBasicInfoListAccor( [FromBody]FilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.Country) || filter.Limit == 0)
                return BadRequest();

            var list = _repos.GetAccorDocuments(filter);
            return Ok(list);
        }

        [HttpPost("list")]
        public ActionResult GetBasicInfoList([FromBody]FilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.Country) || filter.Limit == 0)
                return BadRequest();

            var list = _repos.GetDocuments(filter);
            return Ok(list);
        }

        [HttpGet("details/{id}/{pais}")]
        public ActionResult<string> GetFullInfo(string id, string pais)
        {
            FullInfoRepository repository = new FullInfoRepository();
            var hotel = repository.GetHotel(id, pais);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

    }
}

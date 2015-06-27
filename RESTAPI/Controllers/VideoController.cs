using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RESTAPI.Data;
using RESTAPI.Data.Entities;


namespace RESTAPI.Controllers
{

    [RoutePrefix("api")]
    public class VideoController : ApiController
    {
        private readonly IVideoRepository _repo;

        public VideoController(IVideoRepository repo)
        {
            _repo = repo;
        }

        // GET api/video
        [HttpGet]
        [Route("video")]
        public IHttpActionResult Get()
        {

            var result = _repo.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/video/2 
        [HttpGet]
        [Route("video/{id}")]
        public IHttpActionResult Get(int id)
        {
            var video = _repo.GetVideo(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }     
    }
}
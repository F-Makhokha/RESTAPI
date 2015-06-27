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
    public class ArtistController : ApiController
    {
        private readonly IArtistRepository _repo;

        public ArtistController(IArtistRepository repo)
        {
            _repo = repo;
        }

        // GET api/artist
        [HttpGet]
        [Route("artist")]
        public IHttpActionResult Get()
        {
            var result = _repo.GetAllArtists();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/artist/5  
        [Route("artist/{id}")]
        public IHttpActionResult Get(int id)
        {
            var artist = _repo.GetArtist(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }


        // POST api/artist
        [HttpPost]
        [Route("artist/")]
        public IHttpActionResult Post(Artist artist)
        {
            _repo.Insert(artist);
            return Ok(artist);
        }

        // PUT api/artist/5
        [HttpPost]
        [Route("artist/id")]
        public IHttpActionResult Put(int id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            Artist oldArtist = _repo.GetArtist(id);
            if (oldArtist == null)
            {
                return NotFound();
            }

            _repo.Update(artist);

            return Ok(artist);
        }


        // DELETE api/artist/5
        public IHttpActionResult Delete(int id)
        {
            Artist artist = _repo.GetArtist(id);
            if (artist == null)
            {
                return NotFound();
            }
            _repo.DeleteArtist(id);
            return Ok(artist);


        }

    }
}

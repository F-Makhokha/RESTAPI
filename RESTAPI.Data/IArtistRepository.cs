using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTAPI.Data.Entities;


namespace RESTAPI.Data
{
    public interface IArtistRepository
    {
        Artist GetArtist(int artistId);
        bool Insert(Entities.Artist artist);
        bool Update(Entities.Artist artist);
        bool DeleteArtist(int id);
        IEnumerable<Artist> GetAllArtists();

    }
}

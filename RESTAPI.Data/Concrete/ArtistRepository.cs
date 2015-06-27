using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RESTAPI.Data.Entities;


namespace RESTAPI.Data.Concrete
{
    public class ArtistRepository : IArtistRepository
    {
        private Data.DatabaseEntities _musicContext { get; set; }

        public ArtistRepository(Data.DatabaseEntities musicContext)
        {
            _musicContext = musicContext;
        }

        public Artist GetArtist(int artistId)
        {
            var param = new SqlParameter("@ArtistId ", SqlDbType.Int) { Value = artistId };
            var result = _musicContext.Database.SqlQuery<Artist>
                ("dbo.GetArtists @ArtistId", param).FirstOrDefault();
            return result;
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            var result = _musicContext.Database.SqlQuery<Artist>
              ("dbo.GetArtists").AsEnumerable();

            return result;
        }


        public bool Insert(Artist artist)
        {
            try
            {
                var idParam = new SqlParameter("@ArtistId ", SqlDbType.Int) { Value = artist.ArtistId };
                var nameParam = new SqlParameter("@Name ", SqlDbType.VarChar) { Value = artist.Name };
                var urlSafeNameParam = new SqlParameter("@UrlSafeName ", SqlDbType.VarChar) { Value = artist.UrlSafeName };

                _musicContext.Database.ExecuteSqlCommand(
                   "dbo.CreateNewArtist @ArtistId, @Name, @UrlSafeName",
                   idParam, nameParam, urlSafeNameParam);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Artist artist)
        {
            try
            {
                var artistIdParam = new SqlParameter("@ArtistId", SqlDbType.Int) { Value = artist.ArtistId };
                var nameParam = new SqlParameter("@Name ", SqlDbType.VarChar) { Value = artist.Name };
                var urlSafeNameParam = new SqlParameter("@UrlSafeName ", SqlDbType.VarChar) { Value = artist.UrlSafeName };

                _musicContext.Database.ExecuteSqlCommand(
                   "dbo.UpdateArtist @ArtistId, @Name, @UrlSafeName",
                   artistIdParam, nameParam, urlSafeNameParam);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteArtist(int id)
        {
            var artistIdParam = new SqlParameter("@ArtistId", SqlDbType.Int) { Value = id };
            _musicContext.Database.ExecuteSqlCommand(
                  "dbo.DeleteArtist @ArtistId",
                  artistIdParam);

            return true;
        }       
    }
}

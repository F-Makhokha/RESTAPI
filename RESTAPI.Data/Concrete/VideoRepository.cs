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
    public class VideoRepository : IVideoRepository
    {
        private Data.DatabaseEntities _musicContext { get; set; }

        public VideoRepository(Data.DatabaseEntities musicContext)
        {
            _musicContext = musicContext;
        }

        public Video GetVideo(int videoId)
        {
            var param = new SqlParameter("@videoId ", SqlDbType.Int) { Value = videoId };
            var result = _musicContext.Database.SqlQuery<Video>
                ("dbo.GetVideos @videoId", param).FirstOrDefault();

            return result;
        }

        public IEnumerable<Video> GetAll()
        {
            var result = _musicContext.Database.SqlQuery<Video>
              ("dbo.GetVideos").AsEnumerable();

            return result;
        }

    }
}

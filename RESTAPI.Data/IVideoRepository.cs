using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTAPI.Data.Entities;


namespace RESTAPI.Data
{
    public interface IVideoRepository
    {
        Video GetVideo(int videoid);
        IEnumerable<Video> GetAll();
    }
}

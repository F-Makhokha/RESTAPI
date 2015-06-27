using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RESTAPI.Data.Entities
{
    [DataContract]
    public class Video
    {
        [DataMember]
        public int VideoId { get; set; }
        [DataMember]
        public string Isrc { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string UrlSafeTitle { get; set; }
        [DataMember]
        public int ArtistId { get; set; }

    }
}

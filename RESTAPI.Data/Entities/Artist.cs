using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace RESTAPI.Data.Entities
{
    [DataContract]
    public class Artist
    {
        [DataMember]
        public int ArtistId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UrlSafeName { get; set; }
        [DataMember]
        public virtual IList<Video> videos { get; set; }
    }
}
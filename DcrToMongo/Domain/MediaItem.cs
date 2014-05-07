using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DcrToMongo.Domain
{
    public class MediaItem
    {
        public ObjectId Id { get; set; }

        public int LegacyId { get; set; }

        public string Title { get; set; }

        public string ShortSynopsis { get; set; }

        public string Rating { get; set; }

        public int ReleaseYear { get; set; }

        public int UkBoxOffice { get; set; }

        public int UsBoxOffice { get; set; }
    }
}

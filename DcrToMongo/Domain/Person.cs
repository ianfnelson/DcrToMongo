using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DcrToMongo.Domain
{
    public class Person
    {
        public ObjectId Id { get; set; }

        public int LegacyId { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string LinkingName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }
    }
}

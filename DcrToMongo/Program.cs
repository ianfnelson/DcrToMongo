using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DcrToMongo.Domain;
using MongoDB.Driver;

namespace DcrToMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient(@"mongodb://localhost");

            var server = client.GetServer();

            var database = server.GetDatabase("test");

            //var collection = database.GetCollection<MediaItem>("mediaitems");

            //foreach (var mediaItem in GetMediaItemsFromDcr())
            //{
            //    collection.Insert(mediaItem);
            //}

            var collection = database.GetCollection<Person>("persons");

            collection.InsertBatch(GetPersonsFromDcr());
        }

        private static IEnumerable<MediaItem> GetMediaItemsFromDcr()
        {
            using (
                var conn =
                    new SqlConnection("Data Source=localhost;Initial Catalog=FilmFlexDCR;Integrated Security=True"))
            {
                conn.Open();

                using (
                    var cmd =
                        new SqlCommand(
                            "select id, title, shortsynopsis, bbfcrating, releaseyear, ukboxoffice, usboxoffice from mediaitem where id<2070",
                            conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new MediaItem()
                            {
                                LegacyId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                ShortSynopsis = reader.GetString(2),
                                Rating = reader.GetString(3),
                                ReleaseYear = reader.GetInt16(4),
                                UkBoxOffice = reader.GetInt32(5),
                                UsBoxOffice = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
        }

        private static IEnumerable<Person> GetPersonsFromDcr()
        {
            using (
                var conn =
                    new SqlConnection("Data Source=localhost;Initial Catalog=FilmFlexDCR;Integrated Security=True"))
            {
                conn.Open();

                using (
                    var cmd =
                        new SqlCommand(
                            "select id, givenname, familyname, linkingname, dateofbirth, placeofbirth from person",
                            conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Person()
                            {
                                LegacyId = reader.GetInt32(0),
                                GivenName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                FamilyName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                LinkingName = reader.IsDBNull(3) ? null : reader.GetString(3),
                                DateOfBirth = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                                PlaceOfBirth = reader.IsDBNull(5) ? null : reader.GetString(5),
                            };
                        }
                    }
                }
            }
        }
    }
}

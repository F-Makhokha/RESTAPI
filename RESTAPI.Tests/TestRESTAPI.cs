using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Http;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Moq;

using RESTAPI.Data;
using RESTAPI.Data.Entities;
using RESTAPI.Controllers;


namespace RESTAPI.Tests
{
    [TestClass]
    public class TestRESTAPI
    {
        protected string _baseUrl = "http://localhost:58876/api";

        [TestMethod]
        public void GetAnArtist()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("artist/2", Method.GET) { RequestFormat = DataFormat.Json };

            var response = client.Execute<HttpResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var results = response.Data;
                Assert.IsNotNull(results);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }
        }



        [TestMethod]
        public void GetAllArtist()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("artist", Method.GET) { RequestFormat = DataFormat.Json };

            var response = client.Execute<HttpResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var results = response.Content;
                Assert.IsNotNull(results);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void GetVideoById()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("video/4", Method.GET) { RequestFormat = DataFormat.Json };

            var response = client.Execute<HttpResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var results = response.Data;
                Assert.IsNotNull(results);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetAllVideos()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("video", Method.GET) { RequestFormat = DataFormat.Json };

            var response = client.Execute<Data.Entities.Video>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var results = response.Content;
                Assert.IsNotNull(results);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void GetAllArtist_Success()
        {
            //Setup mock data
            var mockDataList = GetMockArtists();

            //Arrange
            var mockMusicRepository = new Mock<IArtistRepository>();
            mockMusicRepository.Setup(x => x.GetAllArtists()).Returns(mockDataList);

            var controller = new ArtistController(mockMusicRepository.Object);

            // Act
            IHttpActionResult result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void CreateNewArtist()
        {
            var saveRequest = new Artist();
            {
                saveRequest.ArtistId = 15;
                saveRequest.Name = "David";
                saveRequest.UrlSafeName = "davidthompson";
            };

            var client = new RestClient(_baseUrl);
            var request = new RestRequest("artist", Method.POST) { RequestFormat = DataFormat.Json };

            var jsonSaveRequest = JsonConvert.SerializeObject(saveRequest);
            request.AddParameter("application/json; charset=utf-8", jsonSaveRequest, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<Artist>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var results = response.Data;
                Assert.IsNotNull(results);
                Assert.IsNotNull(results.Name);
                Assert.AreEqual(results.Name, saveRequest.Name);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void DeleteAnArtist()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("artist/15", Method.DELETE) { RequestFormat = DataFormat.Json };

            var response = client.Execute<HttpResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var results = response.Data;
                Assert.IsNotNull(results);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateArtist()
        {
            var Urequest = new Artist();
            {

                Urequest.ArtistId = 1;
                Urequest.Name = "Diana Smith";
                Urequest.UrlSafeName = "diana-smith";
            };

            var client = new RestClient(_baseUrl);
            var request = new RestRequest("artist/1", Method.PUT) { RequestFormat = DataFormat.Json };

            var jsonSaveRequest = JsonConvert.SerializeObject(Urequest);
            request.AddParameter("application/json; charset=utf-8", jsonSaveRequest, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<Artist>(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var results = response.Data;
                Assert.IsNotNull(results);
                Assert.IsNotNull(results.Name);
                Assert.AreEqual(results.Name, Urequest.Name);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
            }
            else
            {
                Assert.Fail();
            }

        }


        private static List<Artist> GetMockArtists()
        {
            var mockDataList = new List<Artist>
                {
                    new Artist {ArtistId= 1, Name = "Shakira", UrlSafeName = "sharkia"},
                    new Artist {ArtistId= 2, Name = "Katy Perry", UrlSafeName = "katy-perry"},
                };

            return mockDataList;
        }       

    }
}

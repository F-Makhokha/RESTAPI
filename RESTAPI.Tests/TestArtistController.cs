using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Linq;
using Moq;

using RESTAPI.Controllers;
using RESTAPI.Data;
using RESTAPI.Data.Entities;


namespace RESTAPI.Tests
{
    [TestClass]
    public class TestArtistController
    {
        private Mock<IArtistRepository> repo = new Mock<IArtistRepository>();

        [TestMethod]
        public void WhenGetAll_Returns_Null_IfNoData()
        {
            // Arrange   
            IEnumerable<Artist> mockArtists = GetNoArtists();

            // setup
            repo.Setup(x => x.GetAllArtists()).Returns(mockArtists);

            ArtistController controller = new ArtistController(repo.Object);

            // Act
            IHttpActionResult actionResult = controller.Get();

            // Assert
            Assert.IsNotNull(actionResult, "Result is null");
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "It should return null");
        }

        [TestMethod]
        public void WhenGetAll_Returns_AllArtists()
        {
            // Arrange   
            IEnumerable<Artist> mockArtists = GetMockArtists();

            // setup
            repo.Setup(x => x.GetAllArtists()).Returns(mockArtists);

            ArtistController controller = new ArtistController(repo.Object);

            // Act
            IHttpActionResult actionResult = controller.Get();
            var artists = actionResult as OkNegotiatedContentResult<IEnumerable<Artist>>;

            // Assert
            Assert.IsNotNull(actionResult, "Result is null");
            Assert.IsInstanceOfType(artists.Content, typeof(IEnumerable<Artist>));
            Assert.AreEqual(2, artists.Content.Count(), "Total number of artists is 2");
        }


        [TestMethod]
        public void WhenGet_WithCorrectId_Returns_Artist()
        {
            var artistId = 1;
            // Arrange   
            IEnumerable<Artist> mockArtists = GetMockArtists();

            // setup
            repo.Setup(x => x.GetArtist(artistId)).Returns(mockArtists.FirstOrDefault(p => p.ArtistId == artistId));
            ArtistController controller = new ArtistController(repo.Object);

            // Act
            var response = controller.Get(artistId);
            var returnedArtist = response as OkNegotiatedContentResult<Artist>;

            // Assert
            Assert.IsNotNull(returnedArtist);
            Assert.AreEqual(returnedArtist.Content.Name, "Shakira", "Got correct artist");
        }


        [TestMethod]
        public void WhenDelete_WithValidId_Returns_Ok()
        {
            var artistId = 2;

            // Arrange
            IEnumerable<Artist> mockArtists = GetMockArtists();
            var numberOfArtistBeforeDelete = mockArtists.Count();

            // setup
            repo.Setup(x => x.GetArtist(artistId)).Returns(mockArtists.FirstOrDefault(p => p.ArtistId == artistId));
            ArtistController controller = new ArtistController(repo.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(artistId);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Artist>));

        }


        [TestMethod]
        public void WhenInsert_ValidUser_ReturnsNewArtist()
        {
            // Arrange   
            IEnumerable<Artist> mockArtists = GetMockArtists();

            // setup
            repo.Setup(x => x.GetAllArtists()).Returns(mockArtists);

            ArtistController controller = new ArtistController(repo.Object);

            // Act
            var result = controller.Post(new Artist { ArtistId = 5, Name = "FooFighter", UrlSafeName = "foofighter" })
                    as OkNegotiatedContentResult<Artist>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Content.ArtistId);
            Assert.AreEqual("FooFighter", result.Content.Name);
        }


        [TestMethod]
        public void WhenUpdate_ValidUser_ReturnsOk()
        {
            // Arrange          
            IEnumerable<Artist> mockArtists = GetMockArtists();

            // setup
            var artistId = 2;
            repo.Setup(x => x.GetArtist(artistId)).Returns(mockArtists.FirstOrDefault(p => p.ArtistId == artistId));
            ArtistController controller = new ArtistController(repo.Object);

            // Act
            var result = controller.Put(2, new Artist { ArtistId = 2, Name = "Enya", UrlSafeName = "enya" })
                 as OkNegotiatedContentResult<Artist>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.ArtistId);
            Assert.AreEqual("Enya", result.Content.Name);
        }

        [TestMethod]
        public void WhenUpdate_InValidUser_ShouldFail()
        {
            // Arrange
            IEnumerable<Artist> mockArtists = GetMockArtists();
            ArtistController controller = new ArtistController(repo.Object);

            // Act
            IHttpActionResult result = controller.Put(10, new Artist { ArtistId = 10, Name = "Taylor", UrlSafeName = "taylor" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }



        #region PrivateMethods
        private static IEnumerable<Artist> GetMockArtists()
        {
            IEnumerable<Artist> mockArtists = new List<Artist> {
               new Artist {ArtistId= 1, Name = "Shakira", UrlSafeName = "sharkia"},
               new Artist {ArtistId= 2, Name = "Katy Perry", UrlSafeName = "katy-perry"}
             };

            return mockArtists;
        }

        private static IEnumerable<Artist> GetNoArtists()
        {
            return null;
        }
        #endregion

       
    }
}

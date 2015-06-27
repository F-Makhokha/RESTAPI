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
    public class TestVideoController
    {
        private Mock<IVideoRepository> repo;
        private VideoController controller;

        [TestInitialize]
        public void SetUp()
        {
            repo = new Mock<IVideoRepository>();
        }


        [TestMethod]
        public void WhenGetAll_Returns_404_IfNoData()
        {
            // Setup
            IEnumerable<Video> fakeVideos = null;
            repo.Setup(mr => mr.GetAll()).Returns(fakeVideos);
            controller = new VideoController(repo.Object);

            // Act
            IHttpActionResult actionResult = controller.Get();

            // Assert
            Assert.IsNotNull(actionResult, "Result is null");
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "It should return 404");
        }

        [TestMethod]
        public void WhenGetAll_Returns_AllVideos()
        {
            var mockVideos = GetVideos();
            repo.Setup(mr => mr.GetAll()).Returns(mockVideos);
            controller = new VideoController(repo.Object);

            // Act
            IHttpActionResult result = controller.Get();
            var videos = result as OkNegotiatedContentResult<IEnumerable<Video>>;

            Assert.IsNotNull(videos, "Result is null");
            Assert.IsInstanceOfType(videos.Content, typeof(IEnumerable<Video>));
            Assert.AreNotEqual(3, videos.Content.Count(), "Got wrong number of videos");
        }

        [TestMethod]
        public void WhenGet_WithCorrectId_Returns_Video()
        {

            // Arrange   
            var VideoId = 2;
            IEnumerable<Video> fakeVideos = GetVideos();
            // setup
            repo.Setup(x => x.GetVideo(VideoId)).Returns(fakeVideos.FirstOrDefault(p => p.VideoId == VideoId));

            VideoController controller = new VideoController(repo.Object);

            // Act
            var response = controller.Get(VideoId);
            var returnedVideo = response as OkNegotiatedContentResult<Video>;

            // Assert
            Assert.IsNotNull(returnedVideo);
            Assert.AreEqual(returnedVideo.Content.Title, "Hobbit", "Got wrong Video");
        }


        #region privateMethods
        private static IEnumerable<Video> GetVideos()
        {
            IEnumerable<Video> fakeVideos = new List<Video> {
                new Video { VideoId = 1, Isrc = "UXBEIEOE", Title = "Lord of the rings", UrlSafeTitle= "lord-of-the-rings" },
                new Video { VideoId = 2, Isrc = "KDOEIEJX", Title = "Hobbit", UrlSafeTitle= "hobbit" }
             }.AsEnumerable();

            return fakeVideos;
        }
        #endregion
    }
}

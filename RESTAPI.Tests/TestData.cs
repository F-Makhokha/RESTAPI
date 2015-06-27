using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Moq;

using RESTAPI.Data;
using RESTAPI.Data.Entities;


namespace RESTAPI.Tests
{
    [TestClass]
    public class TestData
    {
        Mock<IArtistRepository> artistRepository;

        [TestInitialize]
        public void SetUp()
        {
            artistRepository = new Mock<IArtistRepository>();

            var mockArtists = GetMockArtists();

            // getall
            artistRepository.Setup(mr => mr.GetAllArtists()).Returns(mockArtists);

            //getById
            artistRepository.Setup(mr => mr.GetArtist(
                                                It.IsAny<int>())).Returns((int i) => mockArtists.Where(
                                                x => x.ArtistId == i).Single());
        }


        [TestMethod]
        public void WhenGetAll_Returns_ValidArtistsList()
        {
            var testArtists = artistRepository.Object.GetAllArtists();

            Assert.IsNotNull(testArtists);
            Assert.IsInstanceOfType(testArtists, typeof(IEnumerable<Artist>));
            Assert.AreEqual(2, testArtists.Count());
        }



        private static IEnumerable<Artist> GetMockArtists()
        {
            IEnumerable<Artist> mockArtists = new List<Artist> {
               new Artist {ArtistId= 1, Name = "Shakira", UrlSafeName = "sharkia"},
               new Artist {ArtistId= 2, Name = "Katy Perry", UrlSafeName = "katy-perry"}
             };

            return mockArtists;
        }
    }
}

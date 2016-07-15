using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Data.TestRepositories;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Tests
{
    [TestFixture]
    public class AdminTests
    {
        private AdminManager _adminManager;

        [SetUp]
        public void BeforeEachTest()
        {
            _adminManager = new AdminManager();
        }

        [Test]
        public void GetAllPendingPostsReturnsExpected()
        {
            int result = _adminManager.GetAllPendingPosts().Count;
            Assert.AreEqual(result, 2);
        }

        [Test]
        public void GetAllPostsReturnsExpected()
        {
            int result = _adminManager.GetAllPosts().Count;
            Assert.AreEqual(result, 6);
        }

        [Test]
        public void GetTagsReturnsExpected()
        {
            int result = _adminManager.GetTags().Count;
            Assert.AreEqual(result, 10);
        }

        [Test]
        [TestCase("Hotel", true)]
        [TestCase("Trip in Europe", true)]
        [TestCase("SomethingElse", false)]
        public void CheckCategoryExistsReturnsExpected(string category, bool expected)
        {
            bool result = _adminManager.CategoryExists(category);
            Assert.AreEqual(result, expected);
        }
    }
}

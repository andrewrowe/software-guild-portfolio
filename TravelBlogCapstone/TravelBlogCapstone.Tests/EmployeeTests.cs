using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        private EmployeeManager _employeeManager;

        [SetUp]
        public void BeforeEachTest()
        {
            _employeeManager = new EmployeeManager();
        }

        [Test]
        public void GetAllApprovedPostReturnsExpected()
        {
            var result = _employeeManager.GetAllApprovedPost().Count;
            Assert.AreEqual(result, 3);
        }

        [Test]
        public void GetAllCategoriesReturnsExpected()
        {
            var result = _employeeManager.GetAllCategories().Count;
            Assert.AreEqual(result, 6);
        }

        [Test]
        public void GetAllTagsReturnsExpected()
        {
            var result = _employeeManager.GetAllTags().Count;
            Assert.AreEqual(result, 10);
        }
    }
}

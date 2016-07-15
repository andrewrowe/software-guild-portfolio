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
    public class UserTests
    {
        private UserManager _userManager;

        [SetUp]
        public void BeforeEachTest()
        {
            _userManager = new UserManager();
        }

        [Test]
        [TestCase("2", 2)]
        [TestCase("3", 0)]
        public void GetAllSendMessagesReturnsExpected(string id, int expected)
        {
            var result = _userManager.GetAllSendMessages(id).Count;
            Assert.AreEqual(result, expected);
        }

        [Test]
        [TestCase("1", 2)]
        [TestCase("16", 1)]
        public void GetAllReceiveMessagesReturnsExpected(string id, int expected)
        {
            var result = _userManager.GetAllReceiveMessages(id).Count;
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void CountNoReadMessagesReturnsExpected()
        {
            var result = _userManager.CountNoReadMessages("1");
            Assert.AreEqual(result, 1);
        }

        [Test]
        public void CreateMessageReturnsExpected()
        {
            var testMessage = new Message
            {
                Body = "Test",
                Id = 5,
                IsRead = false,
                SendUserId = "4",
                RecipientUserId = "3",
                SendDate = DateTime.Today,
                Subject = "Testing"
            };
            _userManager.CreateMessage(testMessage);
            var result = _userManager.Get1Message(5);

            Assert.AreEqual(result.Id, 5);
            Assert.AreEqual(result.SendUserId, "4");
        }

        [Test]
        public void Get1MessageReturnsExpected()
        {
            var result = _userManager.Get1Message(2);
            Assert.AreEqual(result.Id, 2);
            Assert.AreEqual(result.SendUserId, "2");
            Assert.AreEqual(result.RecipientUserId, "16");
        }
    }
}

namespace PVT.Q1._2017.Shop.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Shop.BLL.Services;
    using global::Shop.BLL.Services.Infrastructure;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using global::Moq;

    [TestClass]
    public class UserDataServiceTest
    {
        private readonly IUserDataService _userDataService;
        private readonly IRepositoryFactory _factory;

        public UserDataServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _userDataService = new UserDataService(_factory);
        }

        [TestMethod]
        public void AddUserTest()
        {
            using (var repository = _factory.GetUserDataRepository())
            {
                repository.AddOrUpdate(new UserData());
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetUserFeedbacksListsTest()
        {
            AddUserTest();

            var user = new UserData { Id = 1 };
            using (var repository = _factory.GetFeedbackRepository())
            {
                repository.AddOrUpdate(new Feedback
                {
                    Comments = "Some comments",
                    User = user,
                    UserId = user.Id
                });
                repository.SaveChanges();
            }

            Assert.IsTrue(_userDataService.GetUserFeedbacksList(user).Any());

            Mock.Get(_factory.GetFeedbackRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Feedback, bool>>>(),
                                     It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetUserVotesListsTest()
        {
            AddUserTest();

            var user = new UserData { Id = 1 };
            var track = new Track { Id = 2 };
            using (var repository = _factory.GetVoteRepository())
            {
                repository.AddOrUpdate(new Vote
                {
                    Mark = 5,
                    User = user,
                    UserId = user.Id,
                    Track = track,
                    TrackId = track.Id
                });
                repository.SaveChanges();
            }

            Assert.IsTrue(_userDataService.GetUserVotesList(user).Any());

            Mock.Get(_factory.GetVoteRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Vote, bool>>>(),
                                     It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetUserInfoTest()
        {
            AddUserTest();
            Assert.IsNotNull(_userDataService.GetUserData(1));

            Mock.Get(_factory.GetUserDataRepository())
                .Verify(m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<UserData, BaseEntity>>[]>()), Times.Once);
        }
    }
}

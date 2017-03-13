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
    using Mocks;
    using Moq;

    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        private readonly IRepositoryFactory _factory;

        public UserServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _userService = new UserService(_factory);
        }

        [TestMethod]
        public void AddUserTest()
        {
            using (var repository = _factory.GetUserRepository())
            {
                repository.AddOrUpdate(new User());
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetUserFeedbacksListsTest()
        {
            AddUserTest();

            var user = new User { Id = 1 };
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

            Assert.IsTrue(_userService.GetUserFeedbacksList(user).Any());

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

            var user = new User { Id = 1 };
            var track = new Track { Id = 2 };
            using (var repository = _factory.GetVoteRepository())
            {
                repository.AddOrUpdate(new Vote
                {
                    Mark = Mark.FiveStars,
                    User = user,
                    UserId = user.Id,
                    Track = track,
                    TrackId = track.Id
                });
                repository.SaveChanges();
            }

            Assert.IsTrue(_userService.GetUserVotesList(user).Any());

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
            Assert.IsNotNull(_userService.GetUserInfo(1));

            Mock.Get(_factory.GetUserRepository())
                .Verify(m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<User, BaseEntity>>[]>()), Times.Once);
        }
    }
}
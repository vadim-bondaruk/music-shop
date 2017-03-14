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
    public class VoteServiceTest
    {
        private readonly IVoteService _voteService;
        private readonly IRepositoryFactory _factory;

        public VoteServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _voteService = new VoteService(_factory);
        }

        [TestMethod]
        public void AddVoteTest()
        {
            using (var repository = _factory.GetVoteRepository())
            {
                repository.AddOrUpdate(new Vote() { Mark = Mark.FiveStars });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetVoteTest()
        {
            AddVoteTest();
            Assert.IsNotNull(_voteService.GetTrackVote(new Track(), new User()));

            Mock.Get(_factory.GetVoteRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Vote, bool>>>(),
                                     It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void VoteExistsTest()
        {
            AddVoteTest();
            Assert.IsTrue(_voteService.VoteExists(new Track(), new User()));

            Mock.Get(_factory.GetVoteRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Vote, bool>>>(),
                                     It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetVoteInfoTest()
        {
            AddVoteTest();
            Assert.IsNotNull(_voteService.GetVoteInfo(1));

            Mock.Get(_factory.GetVoteRepository())
                .Verify(m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Vote, BaseEntity>>[]>()), Times.Once);
        }
    }
}

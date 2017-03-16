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
    public class FeedbackServiceTest
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IRepositoryFactory _factory;

        public FeedbackServiceTest()
        {
            _factory = new RepositoryFactoryMoq();
            _feedbackService = new FeedbackService(_factory);
        }

        [TestMethod]
        public void AddFeedbackTest()
        {
            using (var repository = _factory.GetFeedbackRepository())
            {
                repository.AddOrUpdate(new Feedback { Comments = "Some comments" });
                repository.SaveChanges();

                Assert.IsTrue(repository.GetAll().Any());
            }
        }

        [TestMethod]
        public void GetFeedbackTest()
        {
            AddFeedbackTest();
            Assert.IsNotNull(_feedbackService.GetTrackFeedback(new Track(), new User()));

            Mock.Get(_factory.GetFeedbackRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Feedback, bool>>>(),
                                     It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void FeedbackExistsTest()
        {
            AddFeedbackTest();
            Assert.IsTrue(_feedbackService.FeedbackExists(new Track(), new User()));

            Mock.Get(_factory.GetFeedbackRepository())
                .Verify(
                        m =>
                            m.GetAll(It.IsAny<Expression<Func<Feedback, bool>>>(),
                                     It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()), Times.Once);
        }

        [TestMethod]
        public void GetFeedbackInfoTest()
        {
            AddFeedbackTest();
            Assert.IsNotNull(_feedbackService.GetFeedbackInfo(1));

            Mock.Get(_factory.GetFeedbackRepository())
                .Verify(m => m.GetById(It.IsAny<int>(), It.IsAny<Expression<Func<Feedback, BaseEntity>>[]>()), Times.Once);
        }
    }
}

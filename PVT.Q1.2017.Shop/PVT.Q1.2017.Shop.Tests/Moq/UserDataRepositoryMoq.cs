﻿namespace PVT.Q1._2017.Shop.Tests.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using global::Moq;
    using global::Shop.Common.Models;
    using global::Shop.DAL.Infrastruture;
    using global::Shop.Infrastructure.Models;

    public class UserDataRepositoryMoq
    {
        private readonly Mock<IUserDataRepository> _mock;
        private readonly List<UserData> _users = new List<UserData>();

        public UserDataRepositoryMoq()
        {
            _mock = new Mock<IUserDataRepository>();

            _mock.Setup(m => m.GetAll()).Returns(_users);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<UserData, BaseEntity>>[]>()))
                .Returns(_users);

            _mock.Setup(m => m.GetAll(It.IsAny<Expression<Func<UserData, bool>>>()))
                .Returns(_users);

            _mock.Setup(
                       m =>
                           m.GetAll(It.IsAny<Expression<Func<UserData, bool>>>(),
                                    It.IsAny<Expression<Func<UserData, BaseEntity>>[]>()))
                .Returns(_users);

            _mock.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<UserData, bool>>>()))
                 .Returns(() => _users.FirstOrDefault());

            _mock.Setup(
                        m =>
                            m.FirstOrDefault(It.IsAny<Expression<Func<UserData, bool>>>(),
                                             It.IsAny<Expression<Func<UserData, BaseEntity>>[]>()))
                 .Returns(() => _users.FirstOrDefault());

            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns(() => _users.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.GetById(It.IsAny<int>(),
                                      It.IsAny<Expression<Func<UserData, BaseEntity>>[]>()))
                .Returns(() => _users.FirstOrDefault(a => a.Id > 0));

            _mock.Setup(m => m.Exist(It.IsAny<Expression<Func<UserData, bool>>>()))
                 .Returns(() => _users.Any());

            _mock.Setup(m => m.Count(It.IsAny<Expression<Func<UserData, bool>>>()))
                 .Returns(() => _users.Count);

            _mock.Setup(m => m.AddOrUpdate(It.IsNotNull<UserData>())).Callback(() => _users.Add(new UserData
            {
                Id = _users.Count + 1
            }));

            _mock.Setup(m => m.Delete(It.IsNotNull<UserData>())).Callback(() =>
            {
                if (_users.Any())
                {
                    _users.RemoveAt(_users.Count - 1);
                }
            });
        }

        public IUserDataRepository Repository
        {
            get { return _mock.Object; }
        }
    }
}
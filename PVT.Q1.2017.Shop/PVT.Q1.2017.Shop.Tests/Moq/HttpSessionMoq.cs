using Shop.Common.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace PVT.Q1._2017.Shop.Tests.Moq
{
    public class HttpSessionMock : HttpSessionStateBase
    {
        Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return new CurrencyViewModel(); }
            set { var testData = value; }
        }
    }
}

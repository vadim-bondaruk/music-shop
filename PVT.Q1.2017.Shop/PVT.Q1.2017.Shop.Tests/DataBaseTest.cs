namespace PVT.Q1._2017.Shop.Tests
{
    #region

    using global::Shop.DAL.Context;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    /// <summary>
    ///     Summary description for <see cref="DataBaseTest" />
    /// </summary>
    [TestClass]
    public class DataBaseTest
    {
        /// <summary>
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        ///     Gets or sets the test context which provides information about and
        ///     functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void CreateDataBaseTest()
        {
            using (var context = new ShopContext())
            {
                if (context.Database.Exists()) context.Database.Delete();

                context.Database.Create();
            }
        }

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class

        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
    }
}
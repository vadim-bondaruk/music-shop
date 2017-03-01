namespace PVT.Q1._2017.Shop.Infrastructure
{
    using PVT.Q1._2017.Shop.DataModels;

    /// <summary>
    /// Base view model
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// user data model
        /// </summary>
        private UserDataModel _user;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="user"></param>
        public BaseViewModel(UserDataModel user)
        {
            this._user = user;
        }

        /// <summary>
        /// C'tor
        /// </summary>
        public BaseViewModel() : this(new UserDataModel())
        {
        }

        /// <summary>
        /// Get user data
        /// </summary>
        public UserDataModel User
        {
            get { return this._user; }
        }
    }
}
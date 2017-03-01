namespace PVT.Q1._2017.Shop.ViewModels
{
    using DataModels;
    using Infrastructure;

    /// <summary>
    /// Main view model
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="user"></param>
        public MainViewModel(UserDataModel user) : base(user)
        {
        }
    }
}
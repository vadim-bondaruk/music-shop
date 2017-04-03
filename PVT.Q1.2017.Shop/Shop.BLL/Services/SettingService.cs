namespace Shop.BLL.Services
{
    using System.Linq;

    using Common.Models;
    using Common.ViewModels;

    using DAL.Infrastruture;

    using Infrastructure;

    /// <summary>
    /// </summary>
    public class SettingService : BaseService, ISettingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public SettingService(IRepositoryFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public SettingViewModel GetSettingViewModel()
        {
            var settingViewModel = new SettingViewModel();

            // fill drop down lists
            CurrencyService currencyService = new CurrencyService(this.Factory);
            settingViewModel.DefaultCurrencyViewModelList = currencyService.GetCurrenciesList();

            // if setting exists in db
            Setting setting;
            using (var repository = this.Factory.GetSettingRepository())
            {
                setting = repository.GetAll().FirstOrDefault();
            }

            if (setting != null)
            {
                // set selected value 
                settingViewModel.DefaultCurrencyId = setting.DefaultCurrencyId;
            }

            return settingViewModel;
        }

        /// <summary>
        /// </summary>
        /// <param name="settingViewModel">
        /// The setting view model.
        /// </param>
        public void SaveSettingViewModel(SettingViewModel settingViewModel)
        {
            var setting = new Setting
            {
                DefaultCurrencyId = settingViewModel.DefaultCurrencyId
            };

            using (var repository = this.Factory.GetSettingRepository())
            {
                var oldSetting = repository.GetAll().FirstOrDefault();
                if (oldSetting != null)
                {
                    setting.Id = oldSetting.Id;   // replace old value by new one
                }

                repository.AddOrUpdate(setting);
                repository.SaveChanges();
            }
        }
    }
}

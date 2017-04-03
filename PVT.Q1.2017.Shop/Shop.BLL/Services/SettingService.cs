namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using DAL.Infrastruture;
    using Infrastructure;
    using Shop.Common.Models;
    using Shop.Common.ViewModels;
    using Helpers;
    using System;

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

            using (var repository = this.Factory.GetPriceLevelRepository())
            {
                // settingViewModel.DefaultPriceLevelViewModelList = repository.GetAll();   // TODO get ICollection<DefaultPriceLevelViewModel>
            }

            // if setting exists in db
            Setting setting = null;
            using (var repository = this.Factory.GetSettingRepository())
            {
                setting = repository.GetAll().FirstOrDefault();
            }

            if (setting != null)
            {
                // set selected value  // TODO best way to show drop down list?
                settingViewModel.DefaultCurrencyId = setting.DefaultCurrencyId;
                settingViewModel.DefaultPriceLevelId = setting.DefaultPriceLevelId;
                settingViewModel.DefaultCurrencyFullName = settingViewModel.DefaultCurrencyViewModelList
                    .FirstOrDefault(x => x.Id == settingViewModel.DefaultCurrencyId).FullName;  // TODO add checking

                settingViewModel.DefaultPriceLevelName = settingViewModel.DefaultPriceLevelViewModelList
                    .FirstOrDefault(x => x.Id == settingViewModel.DefaultPriceLevelId).Name;  // TODO add checking
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
                DefaultCurrencyId = settingViewModel.DefaultCurrencyId,
                DefaultPriceLevelId = settingViewModel.DefaultPriceLevelId
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

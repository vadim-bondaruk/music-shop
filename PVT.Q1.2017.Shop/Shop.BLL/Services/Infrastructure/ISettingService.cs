namespace Shop.BLL.Services.Infrastructure
{
    using Shop.Common.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// The setting service
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SettingViewModel GetSettingViewModel();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingViewModel"></param>
        void SaveSettingViewModel(SettingViewModel settingViewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICollection<SettingViewModel> GetList();
    }
}
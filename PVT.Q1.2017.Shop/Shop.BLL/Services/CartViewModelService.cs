namespace Shop.BLL.Services
{
    using System.Linq;
    using Common.Models;
    using Common.Models.ViewModels;

    /// <summary>
    /// Cart View Model service
    /// </summary>
    public static class CartViewModelService
    {
        /// <summary>
        /// Set TotalPrice for cart
        /// </summary>
        /// <param name="userCurrency">
        /// Валюта, которую выбрал пользователь.
        /// </param>
        /// <returns> Успешно ли прошла операция подсчёта</returns>
        public static bool SetTotalPrice(CartViewModel userCart, Currency userCurrency)
        {
            foreach (Track anyTrack in userCart.Tracks)
            {
                if (anyTrack.TrackPrices == null)
                {
                    return false;
                }

                userCart.TotalPrice += anyTrack.TrackPrices.First(p => p.Currency.Code == userCurrency.Code).Price;
            }

            return true;
        }
    }
}

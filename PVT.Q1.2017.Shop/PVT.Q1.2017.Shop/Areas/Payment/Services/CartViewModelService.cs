namespace PVT.Q1._2017.Shop.Areas.Payment.Services
{
    using System.Linq;
    using global::Shop.Common.Models;
    using ViewModels;

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
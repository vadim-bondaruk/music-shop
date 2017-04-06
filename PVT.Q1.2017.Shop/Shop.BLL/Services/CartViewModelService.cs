namespace Shop.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using Common.ViewModels;

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
        public static bool SetTotalPrice(CartViewModel userCart, CurrencyViewModel userCurrency)
        {
            if (userCart == null || userCurrency == null)
            {
                return false;
            }

            userCart = CheckNull(userCart);

            foreach (TrackDetailsViewModel anyTrack in userCart.Tracks)
            {
                if (anyTrack.Price == null)
                {
                    return false;
                }

                userCart.TotalPrice += anyTrack.Price.Amount;
            }

            foreach (Album anyAlbum in userCart.Albums)
            {
                if (anyAlbum.AlbumPrices == null)
                {
                    return false;
                }

                userCart.TotalPrice += anyAlbum.AlbumPrices.FirstOrDefault(a => a.Currency.Code == userCurrency.Code).Price;
            }

            return true;
        }

        /// <summary>
        /// Проверка на null содержимого корзины.
        /// </summary>
        /// <param name="userCart"></param>
        /// <returns>Корзина текущего пользователя</returns>
        private static CartViewModel CheckNull(CartViewModel userCart)
        {
            if (userCart.Tracks == null)
            {
                userCart.Tracks = new List<TrackDetailsViewModel>();
                userCart.IsEmpty = true;
            }

            if (userCart.Albums == null)
            {
                userCart.Albums = new List<Album>();
                userCart.IsEmpty = true;
            }

            if (userCart.Tracks.Count > 0 || userCart.Albums.Count > 0)
            {
                userCart.IsEmpty = false;
            }
            return userCart;
        }
    }
}

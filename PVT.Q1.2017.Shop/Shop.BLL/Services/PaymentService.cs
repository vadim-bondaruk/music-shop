namespace Shop.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Infrastruture;
    using PayPal.Api;
    using Shop.BLL.Services.Infrastructure;
    using System.Web.Configuration;
    using System.Web;
    using Shop.BLL.Helpers;
    using Common.ViewModels;
    using Common.Models;
    using Exceptions;
    using Shop.Infrastructure.Models;

    /// <summary>
    /// The payment service class
    /// </summary>
    public class PaymentService : BaseService, IPaymentService
    {
        /// <summary>
        /// PayPal payment
        /// </summary>
        private PayPal.Api.Payment payment;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        /// <param name="factory">
        /// The repository factory.
        /// </param>
        public PaymentService(IRepositoryFactory factory) : base(factory)
        {
        }
               
       
        /// <summary>
        /// Create PayPal payment with PayPal account
        /// </summary>
        public string PaymentWithPaypal(HttpRequestBase request, HttpSessionStateBase session, CartViewModel cart)
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();
            
            try
            {
                string payerId = request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId)&&cart.TotalPrice>0)
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = request.Url.Scheme + "://" + request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString(cart.CurrentUserId);

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid, cart);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    session.Add(guid, createdPayment.id);

                    //return Redirect(paypalRedirectUrl);
                    return paypalRedirectUrl;
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return "Failure";
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: LOGGER
                // Logger.log("Error " + ex.Message);
                return "Failure";
            }

            return "Success";
        }

        /// <summary>
        /// Creates PayPal payment 
        /// </summary>
        /// <param name="apiContext">PayPal API context</param>
        /// <param name="redirectUrl">URL for redirecting after cancel of return from PayPal payment page</param>
        /// <returns></returns>
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, CartViewModel cart)
        {
            
            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            foreach (var track in cart.Tracks)
            {
                var item = new Item();
                item.currency = cart.CurrencyShortName;
                item.name = track.Name;
                item.price = track.Price.Amount.ToString().Replace(',','.');
                item.quantity = "1";

                itemList.items.Add(item);
            }

            var payer = new Payer()
            {
                payment_method = "paypal"                
            };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = cart.TotalPrice.ToString().Replace(',', '.')
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = cart.CurrencyShortName,
                total = cart.TotalPrice.ToString().Replace(',','.'), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = (new Random()).Next(1000000).ToString(), // "your invoice number",
                amount = amount,
                item_list = itemList
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return payment.Create(apiContext);
        }

        /// <summary>
        /// Executes payment info by payerId and paymentId
        /// </summary>
        /// <param name="apiContext">PayPal API context</param>
        /// <param name="payerId">Payer id</param>
        /// <param name="paymentId">Payment id</param>
        /// /// <param name="cart">cart with tracks</param>
        /// <returns></returns>
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        /// <summary>
        /// Creates and saves instance of payment transaction in DB
        /// </summary>
        /// <param name="cart">cart with tracks or albums</param>
        public void CreatePaymentTransaction(CartViewModel cart)
        {
            ////
            var userID = cart.CurrentUserId;
            UserData user;
            var currencyId = 0;
            using (var userDataRepository = Factory.GetUserDataRepository())
            {
                user = userDataRepository.FirstOrDefault(u => u.UserId == userID);
                if (user == null)
                {
                    throw new InvalidUserIdException($"Пользователь с ID={userID} не найден");
                }
                else
                {
                    currencyId = user.CurrencyId;
                }
            }

            using (var payTransRepo = Factory.GetPaymentTransactionRepository())
            {
                var transaction = new PaymentTransaction()
                {
                    PurchasedTrack = new List<PurchasedTrack>(),
                    PurchasedAlbum = new List<PurchasedAlbum>(),
                    Date = DateTime.Now,
                    Amount = cart.TotalPrice,
                    CurrencyId = currencyId,
                    UserId = userID
                };

                #region tracks
                using (var purchasedTrackRepository = Factory.GetPurchasedTrackRepository())
                {
                    foreach (var track in cart.Tracks)
                    {
                        var trackID = track.Id;
                        var purchasedTrack = purchasedTrackRepository.FirstOrDefault(p =>
                            p.UserId == userID
                            && p.TrackId == trackID);
                        if (purchasedTrack == null)
                        {
                            purchasedTrack = new PurchasedTrack() {
                                UserId = userID,
                                TrackId = trackID,
                                CurrencyId = currencyId,
                                Price = track.Price.Amount
                            };
                            purchasedTrackRepository.AddOrUpdate(purchasedTrack);
                        }
                        transaction.PurchasedTrack.Add(purchasedTrack);
                    }
                }
                #endregion

                #region albums
                using (var purchasedAlbumRepository = Factory.GetPurchasedAlbumRepository())
                {
                    foreach (var album in cart.Albums)
                    {
                        var albumID = album.Id;
                        var purchasedAlbum = purchasedAlbumRepository.FirstOrDefault(p =>
                            p.UserId == userID
                            && p.AlbumId == albumID);
                        if (purchasedAlbum == null)
                        {
                            purchasedAlbum = new PurchasedAlbum()
                            {
                                UserId = userID,
                                AlbumId = albumID,
                                CurrencyId = currencyId,
                                Price = album.Price.Amount
                            };
                            purchasedAlbumRepository.AddOrUpdate(purchasedAlbum);
                        }
                        transaction.PurchasedAlbum.Add(purchasedAlbum);
                    }
                }
                #endregion

                payTransRepo.AddOrUpdate(transaction);
                payTransRepo.SaveChanges();
            }
        }

        /// <summary>
        /// Gets and returns collection of payment transactions for certain user by its ID
        /// </summary>
        /// <param name="userID">user ID</param>
        /// <returns></returns>
        public IEnumerable<PaymentTransaction> GetTransactionsByUserId(int userID)
        {
            IEnumerable<PaymentTransaction> transactions = null;
            if (userID!=0)
            {
                using (var payRepo = Factory.GetPaymentTransactionRepository())
                {
                    transactions = payRepo.GetAll((c) => c.UserId == userID,
                                                  a => a.Currency, a => a.User.User);
                }
            }
            return transactions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public PagedResult<PaymentTransaction> GetDataPerPage(int? userID, int pageNumber = 1, int count = 10)
        {
            using (var repository = Factory.GetPaymentTransactionRepository())
            {
                return repository.GetAll(pageNumber, count, a => a.UserId == userID,
                                        a => a.Currency, a => a.User.User);
            }
        }
    }
}

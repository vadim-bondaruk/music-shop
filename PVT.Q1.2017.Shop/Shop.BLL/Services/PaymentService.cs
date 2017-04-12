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
        /// Create PayPal payment with payment card info
        /// </summary>
        public string CreatePaymentWithCreditCard()
        {
            var rnd = new Random();
            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object
            Item item = new Item();
            item.name = "Demo Item";
            item.currency = "USD";
            item.price = "5";
            item.quantity = "1";
            item.sku = "sku";

            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> items = new List<Item>();
            items.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = items;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = "NewYork";
            billingAddress.country_code = "US";
            billingAddress.line1 = "23rd street kew gardens";
            billingAddress.postal_code = "43210";
            billingAddress.state = "NY";


            //Now Create an object of credit card and add above details to it
            //Please replace your credit card details over here which you got from paypal
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = "874";  //card cvv2 number
            crdtCard.expire_month = 1; //card expire date
            crdtCard.expire_year = 2020; //card expire year
            crdtCard.first_name = "Aman";
            crdtCard.last_name = "Thakur";
            crdtCard.number = "4111111111111111"; //enter your credit card number here
            crdtCard.type = "visa"; //credit card type here paypal allows 4 types

            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = "5"; 
            details.tax = "1";

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total = shipping tax + subtotal.
            amnt.total = "7";
            amnt.details = details;

            // Now make a transaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Description about the payment amount.";
            tran.item_list = itemList;
            tran.invoice_number = rnd.Next(100000000,999999999).ToString();

            // Now, we have to make a list of transaction and add the transactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from the paypal
                //basically we are sending the clientID and clientSecret key in this function
                //to the get the context from the paypal API to make the payment
                //for which we have created the object above.

                //Basically, apiContext object has a accesstoken which is sent by the paypal
                //to authenticate the payment to facilitator account.
                //An access token could be an alphanumeric string

                APIContext apiContext = Configuration.GetAPIContext();

                //Create is a Payment class function which actually sends the payment details
                //to the paypal API for the payment. The function is passed with the ApiContext
                //which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                var approvalUrl = createdPayment.GetApprovalUrl();

                //if the createdPayment.state is "approved" it means the payment was successful else not
                if (createdPayment.state.ToLower() == "created")
                {
                    return "created";
                }
                if (createdPayment.state.ToLower() != "approved")
                {
                    return "Failure";
                }
            }
            catch (PayPal.PayPalException ex)
            {
                // TODO: LOGGER
                //Logger.Log("Error: " + ex.Message);
                return "Failure";
            }
            return "Success";
        }

        /// <summary>
        /// Create PayPal DEMO payment with PayPal account
        /// </summary>
        public string PaymentWithPaypalDemo(HttpRequestBase Request, HttpSessionStateBase Session)
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPalDemo?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePaymentDemo(apiContext, baseURI + "guid=" + guid);

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
                    Session.Add(guid, createdPayment.id);

                    //return Redirect(paypalRedirectUrl);
                    return paypalRedirectUrl;
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePaymentDemo(apiContext, payerId, Session[guid] as string);

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
        /// Create PayPal payment with PayPal account
        /// </summary>
        public string PaymentWithPaypal(HttpRequestBase Request, HttpSessionStateBase Session, CartViewModel cart)
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();
            
            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString(cart.CurrentUserId);

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, cart);

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
                    Session.Add(guid, createdPayment.id);

                    //return Redirect(paypalRedirectUrl);
                    return paypalRedirectUrl;
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

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
        /// Creates demo payment 
        /// </summary>
        /// <param name="apiContext">PayPal API context</param>
        /// <param name="redirectUrl">URL for redirecting after cancel of return from PayPal payment page</param>
        /// <returns></returns>
        private Payment CreatePaymentDemo(APIContext apiContext, string redirectUrl)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Song Name",
                currency = "USD",
                price = "5",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "5"
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = "7", // Total must be equal to sum of shipping, tax and subtotal.
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

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        /// <summary>
        /// Executes payment info by payerId and paymentId
        /// </summary>
        /// <param name="apiContext">PayPal API context</param>
        /// <param name="payerId">Payer id</param>
        /// <param name="paymentId">Payment id</param>
        /// <returns></returns>
        private Payment ExecutePaymentDemo(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        /// <summary>
        /// Creates PayPal payment 
        /// </summary>
        /// <param name="apiContext">PayPal API context</param>
        /// <param name="redirectUrl">URL for redirecting after cancel of return from PayPal payment page</param>
        /// <returns></returns>
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, CartViewModel cart)
        {
            // TODO: получить валюту корзины в человеческом виде
            
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

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
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
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
    }
}

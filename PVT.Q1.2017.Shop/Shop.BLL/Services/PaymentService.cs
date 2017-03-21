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
    
    /// <summary>
    /// The payment service class
    /// </summary>
    public class PaymentService : BaseService, IPaymentService
    {
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
        public void CreatePaymentWithPaymentCard()
        {
            // Get a reference to the config
            // var config = ConfigManager.Instance.GetProperties();
            // var config = ConfigManager.GetConfigWithDefaults(new Dictionary<string, string>());
            var config = new Dictionary<string, string>()
            {
                { "mode", "sandbox" },
                { "clientId", "Aa9fGBRFw-6pgCyMe2FQQf2SGccpQnILH4lPyjAXndpeyyrSEWPnRdO8tLMYwqzBy2h8GV0VisT-MJrH" },
                { "clientSecret", "EO6ZTSoCXFTXEwFhzwwxtibA-omO7yVNn5z1FSK6qCYXEfLWvAzoCi9mNOoueIGfZBtf4TdmbHNn_Hzg" }
            };

            // Use OAuthTokenCredential to request an access token from PayPal
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(accessToken);

            // var payment = Payment.Get(apiContext, "PAY-0XL713371A312273YKE2GCNI");

            ////////////////////////////
            // A transaction defines the contract of a payment - what is the payment for and who is fulfilling it. 
            var transaction = new Transaction()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = "7",
                    details = new Details()
                    {
                        shipping = "1",
                        subtotal = "5",
                        tax = "1"
                    }
                },
                description = "This is the payment transaction description.",
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Item Name",
                            currency = "USD",
                            price = "1",
                            quantity = "5",
                            sku = "sku"
                        }
                    },
                    shipping_address = new ShippingAddress
                    {
                        city = "Johnstown",
                        country_code = "US",
                        line1 = "52 N Main ST",
                        postal_code = "43210",
                        state = "OH",
                        recipient_name = "Joe Buyer"
                    }
                },
                invoice_number = "111222333"// Common.GetRandomInvoiceNumber()
            };

            // A resource representing a Payer that funds a payment.
            var payer = new Payer()
            {
                payment_method = "credit_card",
                funding_instruments = new List<FundingInstrument>()
                {
                    new FundingInstrument()
                    {
                        credit_card = new CreditCard()
                        {
                            billing_address = new Address()
                            {
                                city = "Johnstown",
                                country_code = "US",
                                line1 = "52 N Main ST",
                                postal_code = "43210",
                                state = "OH"
                            },
                            cvv2 = "874",
                            expire_month = 11,
                            expire_year = 2018,
                            first_name = "Joe",
                            last_name = "Shopper",
                            number = "4877274905927862",
                            type = "visa"
                        }
                    }
                },
                payer_info = new PayerInfo
                {
                    email = "test@email.com"
                }
            };

            // A Payment resource; create one using the above types and intent as `sale` or `authorize`
            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction>() { transaction }
            };

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);
        }
    }
}

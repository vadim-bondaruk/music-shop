﻿namespace Shop.BLL.Services
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
                { "clientId", WebConfigurationManager.AppSettings["clientId"] },
                { "clientSecret", WebConfigurationManager.AppSettings["clientSecret"] }
            };

            // Use OAuthTokenCredential to request an access token from PayPal
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(accessToken);
            apiContext.Config = config;
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

        public void RunSample()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

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
                invoice_number = PayPalCommon.GetRandomInvoiceNumber()
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

            // ^ Ignore workflow code segment
            // #region Track Workflow
            // this.flow.AddNewRequest("Create credit card payment", payment);
            // #endregion

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            // ^ Ignore workflow code segment
            // #region Track Workflow
            // this.flow.RecordResponse(createdPayment);
            // #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }

        public string PaymentWithCreditCard()
        {
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
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

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
            crdtCard.number = "1234567890123456"; //enter your credit card number here
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
            tran.invoice_number = "your invoice number which you are generating";

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

                //if the createdPayment.state is "approved" it means the payment was successful else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    return "FailureView";
                }
            }
            catch (PayPal.PayPalException ex)
            {
                //Logger.Log("Error: " + ex.Message);
                return "FailureView";
            }
            return "SuccessView";
        }
    }
}

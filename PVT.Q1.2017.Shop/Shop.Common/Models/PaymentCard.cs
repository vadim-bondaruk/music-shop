namespace Shop.Common.Models
{
    using Shop.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A payment card that can fund a payment.
    /// <para>
    /// See <a href="https://developer.paypal.com/docs/api/">PayPal Developer documentation</a> for more information.
    /// </para>
    /// </summary>
    public class PaymentCard : BaseEntity
    {
        /// <summary>
        /// The card number.
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// The card type.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// The two-digit expiry month for the card.
        /// </summary>
        public int expire_month { get; set; }

        /// <summary>
        /// The four-digit expiry year for the card.
        /// </summary>
        public int expire_year { get; set; }

        /// <summary>
        /// The two-digit start month for the card. Required for UK Maestro cards.
        /// </summary>
        public string start_month { get; set; }

        /// <summary>
        /// The four-digit start year for the card. Required for UK Maestro cards. 
        /// </summary>
        public string start_year { get; set; }

        /// <summary>
        /// The validation code for the card. Supported for payments but not for saving payment cards for future use.
        /// </summary>
        public string cvv2 { get; set; }

        /// <summary>
        /// The first name of the card holder.
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// The last name of the card holder.
        /// </summary>
        public string last_name { get; set; }

        /// <summary>
        /// The two-letter country code.
        /// </summary>
        public string billing_country { get; set; }

        /// <summary>
        /// The billing address for the card.
        /// </summary>
        public Address billing_address { get; set; }

        /// <summary>
        /// The ID of the customer who owns this card account. The facilitator generates and provides this ID. Required when you create or use a stored funding instrument in the PayPal vault.
        /// </summary>
        public string external_customer_id { get; set; }

        /// <summary>
        /// The state of the funding instrument.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// The product class of the financial instrument issuer.
        /// </summary>
        public string card_product_class { get; set; }

        /// <summary>
        /// The date and time until when this instrument can be used fund a payment.
        /// </summary>
        public string valid_until { get; set; }

        /// <summary>
        /// The one- to two-digit card issue number. Required for UK Maestro cards.
        /// </summary>
        public string issue_number { get; set; }
    }
}

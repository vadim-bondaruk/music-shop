﻿namespace Shop.Common.Models
{
    using Shop.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Base Address object used as billing address in a payment or extended for Shipping Address.
    /// <para>
    /// See <a href="https://developer.paypal.com/docs/api/">PayPal Developer documentation</a> for more information.
    /// </para>
    /// </summary>
    public class BaseAddress : BaseEntity
    {
        /// <summary>
        /// Line 1 of the Address (eg. number, street, etc).
        /// </summary>
        public string line1 { get; set; }

        /// <summary>
        /// Optional line 2 of the Address (eg. suite, apt #, etc.).
        /// </summary>
        public string line2 { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 2 letter country code.
        /// </summary>
        public string country_code { get; set; }

        /// <summary>
        /// Zip code or equivalent is usually required for countries that have them. For list of countries that do not have postal codes please refer to http://en.wikipedia.org/wiki/Postal_code.
        /// </summary>
        public string postal_code { get; set; }

        /// <summary>
        /// 2 letter code for US states, and the equivalent for other countries.
        /// </summary>
        public string state { get; set; }
    }
}

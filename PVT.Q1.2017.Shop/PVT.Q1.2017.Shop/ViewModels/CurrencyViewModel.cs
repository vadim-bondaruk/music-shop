using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVT.Q1._2017.Shop.ViewModels
{
    public class CurrencyViewModel
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public List<CrossCurrencyViewModel> CrossCurce { get; set; }

        public class CrossCurrencyViewModel {
            public string FullName { get; set; }
            public string ShortName { get; set; }
            public decimal 
        }
    }
}
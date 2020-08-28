using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SuperSaiyanSearch.Domain;
using SuperSaiyanSearch.Domain.Interfaces;

namespace SuperSaiyanSearch.Integration
{
    public class WoolworthsStoreSite : IStoreSite
    {
        public IEnumerable<IProduct> Search(string keyword)
        {
            var url = "https://www.woolworths.co.za";
            var resultProducts = new List<Product>();


            return resultProducts;
        }
    }
}
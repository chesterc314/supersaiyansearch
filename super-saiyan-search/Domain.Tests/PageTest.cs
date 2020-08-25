using System;
using System.Collections.Generic;
using SuperSaiyanSearch.Domain;
using Xunit;

namespace Domain.Tests
{
    public class PageTest
    {
        [Fact]
        public void CreatePageSuccessfully()
        {
            var page = new Page<Product, Guid>();
            page.Items = new List<Product>(){
                new Product()
            };
            Assert.NotNull(page);
        }
    }
}

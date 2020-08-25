using System;
using SuperSaiyanSearch.Domain;
using Xunit;

namespace Domain.Tests
{
    public class ProductTest
    {
        [Fact]
        public void CreateProductSuccessfully()
        {
            var product = new Product();
            Assert.NotNull(product);
        }
    }
}

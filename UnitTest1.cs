using System;
using Xunit;

namespace Supermarket
{
    public class UnitTest1
    {
        private void ScanItems(string items, decimal totalPrice)
        {
            Checkout checkout = new Checkout();

            for(int i = 0; i < items.Length; i++)
            {
                checkout.Scan(items[i]);
            }

            Assert.Equal(totalPrice, checkout.TotalPrice);
        }

        [Theory]
        [InlineData("A", 50)]
        [InlineData("B", 30)]
        [InlineData("C", 20)]
        [InlineData("D", 15)]
        public void ScanItem_NoOffer(string items, decimal totalPrice)
        {
            ScanItems(items, totalPrice);
        }

        [Theory]
        [InlineData("AA", 100)]
        [InlineData("AAA", 130)]
        [InlineData("BB", 30)]
        [InlineData("AB", 70)]
        public void ScanItem_WithOffer(string items, decimal totalPrice)
        {
            ScanItems(items, totalPrice);
        }
    }
}

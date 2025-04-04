using stellarCinema.Entities;

namespace StellarCinema.UnitTests
{
    public class PaymentTests
    {
        [Fact]
        public void ShouldCalculateDiscount()
        {
            // Arrange
            var payment = new Payment
            {
                Amount = 100
            };

            // Act
            var discount = payment.CalculateDiscount(0.1m);

            // Assert
            Assert.Equal(10, discount);
        }

        [Fact]
        public void ShouldReturnMinimum10()
        {
            // Arrange
            var payment = new Payment
            {
                Amount = 50
            };

            // Act
            var discount = payment.CalculateDiscount(0.1m);

            // Assert
            Assert.Equal(10, discount);
        }
    }
}
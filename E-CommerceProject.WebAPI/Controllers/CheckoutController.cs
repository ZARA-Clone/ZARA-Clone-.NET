using E_CommerceProject.Business.Products.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ECommerceContext _context;
        public CheckoutController(ILogger<ProductsController> logger, ECommerceContext context)
        {
            _logger = logger;
            _context = context;
        }
        #region checkout 
        //[HttpPost]
        //public IActionResult ProcessPayment(CheckoutRequest request)
        //{
        //    try
        //    {
        //        // Initialize Stripe configuration with your secret API key
        //        StripeConfiguration.ApiKey = "sk_test_51OzU9vP6V1Tz8l55QoEP0oANr5fK979TRvC4gSwHwqyTQeoXjpCA1B5mzE8LljDhBzBsi4VcQ5jzHP6Q8kP1L9Mr00jTYMvhZb";

        //        // Create a charge using the token and other payment details
        //        var options = new ChargeCreateOptions
        //        {
        //            Amount = (long)(request.Amount * 100), // Convert amount to cents
        //            Currency = request.Currency,
        //            Source = request.Token,
        //            Description = "Payment for Order"
        //        };

        //        var service = new ChargeService();
        //        var charge = service.Create(options);

        //        // Payment successful
        //        if (charge.Paid)
        //        {
        //            // Create order entity
        //            var order = new Order
        //            {
        //                UserId = request.UserId,
        //                OrderDate = DateTime.Now
        //            };

        //            // Move items from cart to order details
        //            var cartItems = _context.UserCarts.Where(c => c.UserId == request.UserId).ToList();
        //            foreach (var cartItem in cartItems)
        //            {
        //                var orderDetail = new OrderDetails
        //                {
        //                    ProductId = cartItem.ProductId,
        //                    Quantity = cartItem.Quantity,
        //                    Price = cartItem.Product.Price // Or any other logic to get the price
        //                };
        //                _context.OrderDetails.Add(orderDetail);; // This line is removed
        //            }

        //            // Clear user's cart
        //            _context.UserCarts.RemoveRange(cartItems);

        //            // Add order to database
        //            _context.Orders.Add(order);
        //            _context.SaveChanges();

        //            return Ok(new { message = "Payment successful", order });
        //        }
        //        else
        //        {
        //            // Payment failed
        //            return BadRequest(new { error = "Payment failed" });
        //        }
        //    }
        //    catch (StripeException e)
        //    {
        //        // Payment failed due to Stripe error
        //        return BadRequest(new { error = e.Message });
        //    }
        //}
        #endregion
    }
}

using E_CommerceProject.Business.Checkout.Dtos;
using E_CommerceProject.Business.Checkout.Models;
using E_CommerceProject.Business.Products.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ECommerceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckoutController(ILogger<ProductsController> logger, ECommerceContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        [HttpPost("process-payment")]
        public async Task<IActionResult> ProcessPayment([FromBody] CheckoutRequest paymentRequest)
        {
            try
            {
                // Get the user ID from the authenticated user
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                // Initialize Stripe configuration with your secret API key
                StripeConfiguration.ApiKey = "sk_test_51OzU9vP6V1Tz8l55QoEP0oANr5fK979TRvC4gSwHwqyTQeoXjpCA1B5mzE8LljDhBzBsi4VcQ5jzHP6Q8kP1L9Mr00jTYMvhZb";

                // Create a payment intent using the token and other payment details
                var options = new PaymentIntentCreateOptions
                {
                    Amount = 1000, // Amount in cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                    Description = "Example payment",
                    Confirm = true,
                    PaymentMethod = paymentRequest.Token,
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);


                var order = new Order
                {
                    UserId = userId,
                    OrderDate = paymentRequest.AdditionalData.OrderDate
                };

                var cartItems = _context.UserCarts.Where(c => c.UserId == userId).ToList();
                foreach (var cartItem in cartItems)
                {
                    var orderDetail = new OrderDetails
                    {
                        ProductId = cartItem.ProductId,
                        OrderId = order.Id,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price,
                        Size = cartItem.SelectedSize,
                    };
                    _context.OrderDetails.Add(orderDetail);

                    // Reduce the quantity of the selected size in the ProductSize table
                    var productSize = _context.Sizes.FirstOrDefault(ps => ps.ProductID == cartItem.ProductId && ps.Size == cartItem.SelectedSize);
                    if (productSize != null)
                    {
                        productSize.Quantity -= cartItem.Quantity;
                    }
                }


                _context.UserCarts.RemoveRange(cartItems);

                _context.Orders.Add(order);
                _context.SaveChanges();

                return Ok("Payment successful");
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }


        [Authorize]
        [HttpPost("Pay-On-Delivery")]
        public async Task<IActionResult> PayOnDelivery(OrderDate orderdate)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }


            var order = new Order
            {
                UserId = userId,
                OrderDate = orderdate.Orderdate,
            };


            var cartItems = _context
                .UserCarts
                .Include(c=> c.Product)
                .Where(c => c.UserId == userId).ToList();
            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = cartItem.ProductId,
                    OrderId = order.Id,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price,
                    Size = cartItem.SelectedSize,
                };
                _context.OrderDetails.Add(orderDetail);

                // Reduce the quantity of the selected size
                var productSize = _context.Sizes.FirstOrDefault(ps => ps.ProductID == cartItem.ProductId && ps.Size == cartItem.SelectedSize);
                if (productSize != null)
                {
                    productSize.Quantity -= cartItem.Quantity;
                }
            }

            _context.UserCarts.RemoveRange(cartItems);
            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok("Order Placed Successfully");
        }


        [Authorize]
        [HttpGet("cartcontent")]
        public async Task<IActionResult> GetCartContent()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var cartItems = _context.UserCarts.Where(c => c.UserId == userId).ToList();
            List<CartContentDto> cartContent = new List<CartContentDto>();
            foreach (var cartItem in cartItems)
            {
                CartContentDto cart = new CartContentDto()
                {
                    ImgUrl = cartItem.Product.ProductImages[0].Url,
                };
                cartContent.Add(cart);
            }
            return Ok(cartContent);
        }
    }
 
}
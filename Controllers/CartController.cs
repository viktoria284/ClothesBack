using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClothesBack.Models;
using ClothesBack.Dtos;

namespace ClothesBack.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems(string userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                .Select(c => new CartItemDto
                {
                    CartItemId = c.CartItemId,
                    ProductVariantId = c.ProductVariantId,
                    ProductName = c.ProductVariant.Product.ProductName,
                    Image = c.ProductVariant.Product.Image,
                    Size = c.ProductVariant.Size,
                    Quantity = c.Quantity,
                    Price = c.ProductVariant.Product.Price
                })
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound(new { message = "The user's cart is empty or the user was not found." });
            }

            return Ok(cartItems);
        }

        // GET: api/cart/productVariant/{productVariantId}
        [HttpGet("productVariant/{productVariantId}")]
        public async Task<IActionResult> GetProductVariant(Guid productVariantId)
        {
            var productVariant = await _context.ProductVariants.FindAsync(productVariantId);
            if (productVariant == null)
            {
                return NotFound("Product variant not found");
            }

            return Ok(new { stockQuantity = productVariant.StockQuantity });
        }

        // PUT: api/cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(Guid cartItemId, CartItem cartItem)
        {
            if (cartItemId != cartItem.CartItemId)
            {
                return BadRequest(new { message = "The cart item ID does not match." });
            }

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(cartItemId))
                {
                    return NotFound(new { message = "Cart item not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /*[HttpPut("quantity/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItemQuantity(Guid cartItemId, [FromBody] int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }

            var productVariant = await _context.ProductVariants.FindAsync(cartItem.ProductVariantId);
            if (productVariant == null)
            {
                return NotFound("Product variant not found");
            }

            if (quantity < 1)
            {
                return BadRequest("Quantity must be at least 1");
            }

            if (productVariant.StockQuantity < quantity)
            {
                return BadRequest("Not enough stock available");
            }

            // Update stock quantity only if it is an increase in quantity
            if (quantity > cartItem.Quantity)
            {
                int difference = quantity - cartItem.Quantity;
                productVariant.StockQuantity -= difference;
            }
            else if (quantity < cartItem.Quantity)
            {
                int difference = cartItem.Quantity - quantity;
                productVariant.StockQuantity += difference;
            }

            cartItem.Quantity = quantity;

            _context.ProductVariants.Update(productVariant);
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }*/

        // POST: api/cart/toCart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("toCart")]
        public async Task<ActionResult<CartItem>> PostCartItem(AddToCartDto addToCartDto)
        {
            if (addToCartDto == null || addToCartDto.Quantity <= 0 || string.IsNullOrEmpty(addToCartDto.UserId))
            {
                return BadRequest(new { message = "Invalid cart item data." });
            }

            var productVariant = await _context.ProductVariants.FindAsync(addToCartDto.ProductVariantId);
            if (productVariant == null)
            {
                return NotFound(new { message = "Product variant not found." });
            }

            if (productVariant.StockQuantity < addToCartDto.Quantity)
            {
                return BadRequest(new { message = "Not enough stock available." });
            }

            var cartItem = new CartItem
            {
                CartItemId = Guid.NewGuid(),
                UserId = addToCartDto.UserId,
                ProductVariantId = addToCartDto.ProductVariantId,
                Quantity = addToCartDto.Quantity
            };

            //productVariant.StockQuantity -= addToCartDto.Quantity;

            _context.CartItems.Add(cartItem);
            //_context.ProductVariants.Update(productVariant);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }

        // DELETE: api/cart/5
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found." });
            }

            var productVariant = await _context.ProductVariants.FindAsync(cartItem.ProductVariantId);
            if (productVariant == null)
            {
                return NotFound(new { message = "Product variant not found." });
            }

            //productVariant.StockQuantity += cartItem.Quantity;

            _context.CartItems.Remove(cartItem);

            //_context.ProductVariants.Update(productVariant);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cart item successfully deleted." });
        }

        private bool CartItemExists(Guid id)
        {
            return _context.CartItems.Any(с => с.CartItemId == id);
        }
    }
}

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

        // GET: api/Cart/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems(string userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.ProductVariant)
                .Select(c => new CartItemDto
                {
                    CartItemId = c.CartItemId,
                    ProductVariantId = c.ProductVariantId,
                    Size = c.ProductVariant.Size,
                    Quantity = c.Quantity,
                    Price = c.Price
                })
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound(new { message = "The user's cart is empty or the user was not found." });
            }

            return Ok(cartItems);
        }

        /*
        // GET: api/Cart/5
        [HttpGet("{cartItemId}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return cartItem;
        }
        */

        // PUT: api/Cart/5
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

        // POST: api/Cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartItem>> PostCartItem(CartItem cartItem)
        {
            cartItem.CartItemId = Guid.NewGuid();

            // Add validation logic here

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartItems), new { userId = cartItem.UserId }, cartItem);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound(new { message = "Cart item not found." });
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cart item successfully deleted." });
        }

        private bool CartItemExists(Guid id)
        {
            return _context.CartItems.Any(с => с.CartItemId == id);
        }
    }
}

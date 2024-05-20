using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClothesBack.Models;
using Microsoft.AspNetCore.Authorization;
using ClothesBack.Dtos;

namespace ClothesBack.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductVariantDto>> GetProduct(Guid productId)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return NotFound();

            var variantDto = new ProductVariantDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Color = product.Color,
                Price = product.Price,
                Images = product.Images.Select(i => i.Data).ToList(),
                Variants = product.ProductVariants.Select(v => new ProductVariantInfo
                {
                    ProductVariantId = v.ProductVariantId,
                    Size = v.Size,
                    StockQuantity = v.StockQuantity
                }).ToList()
            };

            return Ok(variantDto);
        }


        // GET: api/Products/wImage
        [HttpGet("wImage")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsWithImage()
        {
            /*var productsWithImage = await _context.Products
                .Include(p => p.Images)
                .OrderBy(p => p.ProductName == "Phys Ed Graphic T-Shirt" ? 1 :
                   p.ProductName == "Phys Ed Hoodie" ? 2 :
                   p.ProductName == "Training Oversized Fleece Sweatshirt" ? 3 :
                   p.ProductName == "Balance V3 Seamless Zip Jacket" ? 4 :
                   p.ProductName == "Balance V3 Seamless Crop Top" ? 5 :
                   p.ProductName == "Balance V3 Seamless Shorts" ? 6 :
                   p.ProductName == "Balance V3 Seamless Leggings" ? 7 :
                   p.ProductName == "GFX Crew Socks 7PK" ? 8 :
                   9)
                .Select(product => new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Color = product.Color,
                    Price = product.Price,
                    //Images = product.Images.Select(i => i.Data).ToList(),
                    //Image = GetMainImageData(product)
                    Image = product.Images.FirstOrDefault(img => img.IsMain)?.Data
                })
                .ToListAsync();*/

            var productsWithImage = await _context.Products
               .Join(
                   _context.Images.Where(image => image.IsMain),
                   product => product.ProductId,
                   image => image.ProductId,
                   (product, image) => new ProductDto
                   {
                       ProductId = product.ProductId,
                       ProductName = product.ProductName,
                       Description = product.Description,
                       Color = product.Color,
                       Price = product.Price,
                       Image = image.Data
                   }
               )
               .OrderBy(p => p.ProductName == "Phys Ed Graphic T-Shirt" ? 1 :
                   p.ProductName == "Phys Ed Hoodie" ? 2 :
                   p.ProductName == "Training Oversized Fleece Sweatshirt" ? 3 :
                   p.ProductName == "Balance V3 Seamless Zip Jacket" ? 4 :
                   p.ProductName == "Balance V3 Seamless Crop Top" ? 5 :
                   p.ProductName == "Balance V3 Seamless Shorts" ? 6 :
                   p.ProductName == "Balance V3 Seamless Leggings" ? 7 :
                   p.ProductName == "GFX Crew Socks 7PK" ? 8 :
                   9)
                .ToListAsync();


            return Ok(productsWithImage);
        }

        private byte[] GetMainImageData(Product product)
        {
            var mainImage = product.Images.FirstOrDefault(img => img != null && img.IsMain);
            return mainImage != null ? mainImage.Data : null;
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(Guid productId, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No image provided");
            }

            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            var image = new Image
            {
                ImageId = Guid.NewGuid(),
                ProductId = productId,
                Data = imageData
            };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok("Image uploaded successfully");

            /*var image = new Image(imageId, productId, imageData);
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;*/
        }


        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }


        // GET: api/Products/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }*/

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.ProductId = Guid.NewGuid();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}

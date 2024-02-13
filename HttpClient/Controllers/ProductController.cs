using HttpClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private MySaleDBContext _context = new MySaleDBContext();

        // [HttpGet]
        // public IActionResult GetAllProducts1()
        // {
        //     // return Ok(_context.Products.ToList());
        //     List<Product> products = _context.Products.Include("Category").ToList();
        //     List<ProductResponseDTO> productsDTO = new List<ProductResponseDTO>();
        //     foreach (Product element in products)
        //     {
        //         ProductResponseDTO productResponseDTO = new ProductResponseDTO()
        //         {
        //             ProductId = element.ProductId,
        //             ProductName = element.ProductName,
        //             UnitPrice = element.UnitPrice,
        //             UnitsInStock = element.UnitsInStock,
        //             Image = element.Image,
        //             CategoryName = element.Category.CategoryName
        //         };
        //         productsDTO.Add(productResponseDTO);
        //     }
        //
        //     return Ok(productsDTO);
        // }

        //
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            // return Ok(_context.Products.Select(x => new
            // {
            //     x.ProductId,
            //     Name = x.ProductName,
            //     x.UnitPrice,
            //     x.UnitsInStock,
            //     x.Image,
            //     x.Category.CategoryName
            // }));

            return Ok(_context.Products.Include(x => x.Category).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int? id)
        {
            return Ok(_context.Products.Include(x => x.Category)
                .FirstOrDefault(c => c.ProductId == id));
        }

        [HttpPost]
        public IActionResult AddProduct(Product productDto)
        {
            if (_context.Products.Any(p => p.ProductName.Equals(productDto.ProductName)))
            {
                return BadRequest();
            }

            // Product product = new Product()
            // {
            //     ProductName = productDto.ProductName,
            //     UnitPrice = productDto.UnitPrice,
            //     UnitsInStock = productDto.UnitsInStock,
            //     Image = productDto.Image,
            //     Category = _context.Categories.FirstOrDefault(c => c.CategoryId == productDto.CategoryId),
            // };

            Product p = _context.Add(productDto).Entity;
            _context.SaveChanges();

            return Ok(p);
        }

        // [HttpPut]
        // public IActionResult EditProduct(Product product)
        // {
        //     Product p = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
        //     if (p == null)
        //     {
        //         return BadRequest();
        //     }
        //
        //     p.ProductName = product.ProductName;
        //     p.UnitPrice = product.UnitPrice;
        //     p.UnitsInStock = product.UnitsInStock;
        //     p.Image = product.Image;
        //     p.CategoryId = product.CategoryId;
        //
        //     // _context.U
        //     _context.SaveChanges();
        //     return Ok(_context.Products.ToList());
        // }

        [HttpPut]
        public IActionResult EditProduct2(Product product)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (p == null)
            {
                return BadRequest();
            }

            _context.Entry(p).CurrentValues.SetValues(product);
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (p == null)
            {
                return BadRequest();
            }

            _context.Remove(p);
            _context.SaveChanges();
            return Ok($"Delete successfully product id {id}");
        }
    }
}
using HttpClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private MySaleDBContext _context = new MySaleDBContext();

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_context.Categories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int? id)
        {
            return Ok(_context.Categories.Where(c => c.CategoryId == id).ToList());
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] string categoryName)
        {
            Category c = new Category()
            {
                CategoryName = categoryName
            };

            Category d = _context.Add(c).Entity;
            _context.SaveChanges();

            return Ok(d);
        }

        [HttpPut]
        public IActionResult EditCategory(Category category)
        {
            Category c = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (c == null)
            {
                return BadRequest();
            }

            c.CategoryName = category.CategoryName;
            _context.SaveChanges();
            return Ok(c);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category c = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (c == null)
            {
                return BadRequest();
            }

            _context.Remove(c);
            _context.SaveChanges();
            return Ok(c);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Extensions.Logging;

namespace AspNetApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> Products = new()
        {
            new Product { Id = 1, Name = "Product1", Price = 10.0 },
            new Product { Id = 2, Name = "Product2", Price = 20.0 }
        };

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        // GET: products
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            _logger.LogInformation("Fetching all products");
            return Products.ToArray();
        }

        // GET: products/{id}
        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            _logger.LogInformation($"Fetching product with id {id}");
            var product = FindProductById(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return NotFound();
            }
            return product;
        }

        // POST: products
        [HttpPost(Name = "CreateProduct")]
        public ActionResult<Product> CreateProduct(Product product)
        {
            _logger.LogInformation("Creating a new product");
            product.Id = Products.Count + 1;
            Products.Add(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        // PUT: products/{id}
        [HttpPut("{id}", Name = "UpdateProduct")]
        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {
            _logger.LogInformation($"Updating product with id {id}");
            var product = FindProductById(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        // DELETE: products/{id}
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            _logger.LogInformation($"Deleting product with id {id}");
            var product = FindProductById(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return NotFound();
            }

            Products.Remove(product);
            return NoContent();
        }

        private Product FindProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}

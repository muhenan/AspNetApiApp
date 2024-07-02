using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Extensions.Logging;

namespace AspNetApiApp.Controllers
{
    // add comments to describe the purpose of the class
    // This class serves as the controller for product-related operations in a web application.
    // It handles HTTP requests related to products, such as fetching, creating, updating, and deleting products.
    // The controller interacts with the model to process business logic and returns the appropriate views or data responses.
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

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            _logger.LogInformation("Fetching all products");
            return Products.ToArray();
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>An <see cref="ActionResult"/> representing the result of the operation.</returns>
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

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>An <see cref="ActionResult"/> representing the result of the operation.</returns>
        [HttpPost(Name = "CreateProduct")]
        public ActionResult<Product> CreateProduct(Product product)
        {
            _logger.LogInformation("Creating a new product");
            product.Id = Products.Count + 1;
            Products.Add(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Creates a new random product.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> representing the result of the operation.</returns>
        [HttpPost(Name = "CreateProductRandom")]
        [Route("CreateProductRandom")]
        public ActionResult<Product> CreateProductRandom()
        {
            _logger.LogInformation("Creating a new product");
            var product = new Product { Id = Products.Count + 1, Name = "Product" + (Products.Count + 1), Price = 10.0 };
            Products.Add(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates a product with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The updated product data.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the update operation.</returns>
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

        /// <summary>
        /// Deletes a product with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the delete operation.</returns>
        /// [HttpDelete("{id}", Name = "DeleteProduct")]
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

        private Product? FindProductById(int id)
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

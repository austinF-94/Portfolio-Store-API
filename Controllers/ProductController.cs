using Store_API.Models;
using Store_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Store_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase 
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductsRepository _productsRepository;

    public ProductsController(ILogger<ProductsController> logger, IProductsRepository repository)
    {
        _logger = logger;
        _productsRepository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProduct() 
    {
        return Ok(_productsRepository.GetAllProducts());
    }

    [HttpGet]
    [Route("{productId:int}")]
    public ActionResult<Product> GetProductById(int productId) 
    {
    var product = _productsRepository.GetProductById(productId);
    if (product == null) {
        return NotFound();
    }
    return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product) 
    {
    if (!ModelState.IsValid || product == null) {
        return BadRequest();
    }
    var newProduct = _productsRepository.CreateProduct(product);
    return Created(nameof(GetProductById), newProduct);
    }

    [HttpPut]
    [Route("{productId:int}")]
    public ActionResult<Product> UpdateProduct(Product product) 
    {
    if (!ModelState.IsValid || product == null) {
        return BadRequest();
    }
    return Ok(_productsRepository.UpdateProduct(product));
    }

    [HttpDelete]
    [Route("{productId:int}")]
    public ActionResult DeleteProduct(int productId) 
    {
    _productsRepository.DeleteProductById(productId); 
    return NoContent();
    }
}
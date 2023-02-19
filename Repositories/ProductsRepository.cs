using Store_API.Migrations;
using Store_API.Models;

namespace Store_API.Repositories;

public class ProductsRepository : IProductsRepository 
{
    private readonly ProductDbContext _context;

    public ProductsRepository(ProductDbContext context)
    {
        _context = context;
    }

    public Product CreateProduct(Product newProduct)
    {
        _context.Product.Add(newProduct);
        _context.SaveChanges();
        return newProduct;
    }

    public void DeleteProductById(int productId)
    {
        var product = _context.Product.Find(productId);
        if (product != null) 
        {
            _context.Product.Remove(product); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Product.ToList();
    }

    public Product? GetProductById(int productId)
    {
        return _context.Product.SingleOrDefault(c => c.ProductId == productId);
    }

    public Product? UpdateProduct(Product newProduct)
    {
        var originalProduct = _context.Product.Find(newProduct.ProductId);
        if (originalProduct != null) 
        {
            originalProduct.Title = newProduct.Title;
            originalProduct.Price = newProduct.Price;
            originalProduct.Description = newProduct.Description;
            originalProduct.Picture = newProduct.Picture;
            _context.SaveChanges();
        }
        return originalProduct;
    }
}
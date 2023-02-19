using Store_API.Models;

namespace Store_API.Repositories;

public interface IProductsRepository
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int productId);
    Product CreateProduct(Product newProduct);
    Product? UpdateProduct(Product newProduct);
    void DeleteProductById(int productId);
}
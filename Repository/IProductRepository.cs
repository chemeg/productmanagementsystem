using ProductManagementSystem.Models;

namespace ProductManagementSystem.Repository
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);

        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<List<Product>> GetProductsByNameAsync(string name);

        Task<int> GetTotalProductCountAsync();

        Task<List<Product>> GetProductsByCategoryAsync(string category);

        IQueryable<Product> GetProducts();

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(Product product);

        Task DeleteAllProductsAsync();
    }
}

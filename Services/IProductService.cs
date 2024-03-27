using ProductManagementSystem.Models;

namespace ProductManagementSystem.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task<List<Product>> GetProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<List<Product>> GetProductsByNameAsync(string name);

        Task<int> GetTotalProductCountAsync();

        Task<List<Product>> GetProductsByCategoryAsync(string category);

        Task<List<Product>> GetSortedProductsAsync(string sortBy, string sortOrder);

        Task UpdateProductAsync(int id, Product product);

        Task DeleteProductAsync(int id);

        Task DeleteAllProductsAsync();
    }
}

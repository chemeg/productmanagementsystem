using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;
using ProductManagementSystem.Repository;

namespace ProductManagementSystem.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await _productRepository.GetProductsByNameAsync(name);
        }

        public async Task<int> GetTotalProductCountAsync()
        {
            return await _productRepository.GetTotalProductCountAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _productRepository.GetProductsByCategoryAsync(category);
        }

        public async Task<List<Product>> GetSortedProductsAsync(string sortBy, string sortOrder)
        {
            IQueryable<Product> sortedProducts = sortBy switch
            {
                "name" => _productRepository.GetProducts().OrderBy(p => p.Name),
                "category" => _productRepository.GetProducts().OrderBy(p => p.Category),
                "price" => _productRepository.GetProducts().OrderBy(p => p.Price),
                _ => throw new ArgumentException("Invalid sorting parameter")
            };

            if (sortOrder.ToLower() == "desc")
            {
                sortedProducts = sortedProducts.Reverse();
            }

            return await sortedProducts.ToListAsync();
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                throw new ArgumentException("Product ID mismatch");
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with ID {id} not found");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;

            await _productRepository.UpdateProductAsync(existingProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with ID {id} not found");
            }

            await _productRepository.DeleteProductAsync(existingProduct);
        }

        public async Task DeleteAllProductsAsync()
        {
            await _productRepository.DeleteAllProductsAsync();
        }
    }
}

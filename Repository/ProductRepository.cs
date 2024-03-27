using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<int> GetTotalProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _context.Products.Where(p => p.Category == category).ToListAsync();
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products.AsQueryable();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
        }
    }
}

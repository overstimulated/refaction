using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using refactor_me.Models;
using System.Threading.Tasks;

namespace refactor_me.Services
{
    public class ProductsService : IProductService
    {
        private readonly MainContext _context;

        public ProductsService(MainContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Product;
        }

        public async Task<Product> GetByName(string name)
        {
            return await _context.Product.SingleOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task Create(Product product)
        {
            _context.Product.Add(product);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Product productUpdate)
        {
            var existing = await _context.Product.FindAsync(id);

            if (existing != null)
            {
                existing.Name = productUpdate.Name;
                existing.Description = productUpdate.Description;
                existing.Price = productUpdate.Price;
                existing.DeliveryPrice = productUpdate.DeliveryPrice;

                _context.Product.AddOrUpdate(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid productId)
        {
            var existing = await _context.Product.FindAsync(productId);

            if (existing != null)
            {
                _context.Product.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }

    /// <summary>
    /// TODO refactor
    /// </summary>
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        Task<Product> GetByName(string name);
        Task<Product> GetById(Guid id);
        Task Create(Product product);
        Task Update(Guid id, Product product);
        Task Delete(Guid productId);
    }
}
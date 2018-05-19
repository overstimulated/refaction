using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using refactor_me.Models;

namespace refactor_me.Services
{
    public class ProductOptionsService: IProductOptionsService
    {
        private readonly MainContext _context;

        public ProductOptionsService(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// finds all productoptions for a specified product
        /// </summary>
        public IQueryable<ProductOption> GetAll(Guid productId)
        {
            return FilteredProductOptions(productId);
        }

        /// <summary>
        /// Get productoptions by productId
        /// </summary>
        public async Task<ProductOption> GetBy(Guid productId, Guid productOptionId)
        {
            return await FilteredProductOptions(productId).SingleOrDefaultAsync(p => p.Id == productOptionId);
        }

        public async Task Create(Guid productId, ProductOption option)
        {
            // find the product
            // make sure that product exists
            var existingProduct = await _context.Product.FindAsync(productId);

            if (existingProduct != null)
            {
                option.Product = existingProduct;
                _context.ProductOption.Add(option);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Guid productId, Guid productOptionId, ProductOption product)
        {
            var existingProdOp =
                await FilteredProductOptions(productId).SingleOrDefaultAsync(p => p.Id == productOptionId);

            if (existingProdOp != null)
            {
                existingProdOp.Description = product.Description;
                existingProdOp.Name = product.Name;
                _context.ProductOption.AddOrUpdate(existingProdOp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid productId, Guid productOptionId)
        {
            var existingProdOp =
                await FilteredProductOptions(productId).SingleOrDefaultAsync(p => p.Id == productOptionId);

            if (existingProdOp != null)
            {
                _context.ProductOption.Remove(existingProdOp);
                await _context.SaveChangesAsync();
            }
        }


        private IQueryable<ProductOption> FilteredProductOptions(Guid productId)
        {
            return _context.ProductOption.Include(p => p.Product).Where(p => p.Product.Id == productId);
        }
    }

    /// <summary>
    /// TODO refactor
    /// </summary>
    public interface IProductOptionsService
    {
        IQueryable<ProductOption> GetAll(Guid productId);
        Task<ProductOption> GetBy(Guid productId, Guid productOptionid);
        Task Create(Guid productId, ProductOption productOption);
        Task Update(Guid productId, Guid productOptionId, ProductOption productOption);
        Task Delete(Guid productId, Guid productOptionid);
    }
}
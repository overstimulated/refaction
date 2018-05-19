using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Models;

namespace UnitTestProject1.TestData
{
    public static class TestData
    {
        public static IQueryable<Product> Products()
        {
            return new List<Product>
            {
                new Product{
                    Description = "Prod 1",
                    Id = Guid.NewGuid(),
                    Name = "Prod1",
                    DeliveryPrice = 12,
                    Price = 3
                },
                new Product() {
                    Description = "Prod 2",
                    Id = Guid.NewGuid(),
                    Name = "Prod2",
                    DeliveryPrice = 12,
                    Price = 3
                },
                new Product() {
                    Description = "Prod 3",
                    Id = Guid.NewGuid(),
                    Name = "Prod3",
                    DeliveryPrice = 12,
                    Price = 3
                }
            }.AsQueryable();
        }

        public static IQueryable<ProductOption> ProductOptions()
        {
            return new List<ProductOption>
            {
                new ProductOption
                {
                    Description = "description 1",
                    Id = Guid.NewGuid(),
                    Name = "Desc1"
                },
                new ProductOption()
                {
                    Description = "description 2",
                    Id = Guid.NewGuid(),
                    Name = "Desc2"
                },
                new ProductOption()
                {
                    Description = "description 3",
                    Id = Guid.NewGuid(),
                    Name = "Desc3"
                }
            }.AsQueryable();
        }
    }
}

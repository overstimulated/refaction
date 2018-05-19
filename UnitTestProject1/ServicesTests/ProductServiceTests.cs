using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using refactor_me.Models;
using refactor_me.Services;
using UnitTestProject1.TestData;

namespace UnitTestProject1.ServicesTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly Mock<MainContext> _mockMainContext;
        private readonly ProductsService _service;
        private readonly Mock<DbSet<Product>> _mockSet;

        public ProductServiceTests()
        {
            _mockSet = new Mock<DbSet<Product>>();
            _mockSet.As<IDbAsyncEnumerable<Product>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Product>(TestData.TestData.Products().GetEnumerator()));

            _mockSet.As<IQueryable<Product>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Product>(TestData.TestData.Products().Provider));

            _mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(TestData.TestData.Products().Expression);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(TestData.TestData.Products().ElementType);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(TestData.TestData.Products().GetEnumerator());

            _mockMainContext = new Mock<MainContext>();
            _mockMainContext.Setup(c => c.Product).Returns(_mockSet.Object);
            _service = new ProductsService(_mockMainContext.Object);
        }

        [TestMethod]
        public void GetAllProducts_ReturnsListOfProducts()
        {          
            var products = _service.GetAll();

            Assert.AreEqual(3, products.Count());
        }

        [TestMethod]
        public async Task GetProductByAsync_ReturnsItem()
        {
            var firstProd = TestData.TestData.Products().First();

            _mockSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult(firstProd)).Verifiable(); 
                        
            var products = await _service.GetById(firstProd.Id);

            _mockSet.Verify(m => m.FindAsync(It.IsAny<Guid>()), Times.Once);

            Assert.AreEqual(firstProd.Id, products.Id);
            Assert.AreEqual(firstProd.Name, products.Name);
            Assert.AreEqual(firstProd.Description, products.Description);
        }

        [TestMethod]
        public async Task CreateNewProduct_Creates()
        {
            _mockMainContext.Setup(c => c.Product.Add(It.IsAny<Product>()));

            var prod = new Product();
            await _service.Create(prod);

            _mockMainContext.Verify(c => c.Product.Add(It.IsAny<Product>()), Times.Once);
            _mockMainContext.Verify(c => c.SaveChangesAsync(), Times.Once);
        }


        public void Update_UpdatesExistingProduct()
        {
            //ToDo
        }
    }
}
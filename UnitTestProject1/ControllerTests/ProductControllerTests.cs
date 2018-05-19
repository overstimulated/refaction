using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using refactor_me.Controllers;
using refactor_me.DTO;
using refactor_me.Services;
using System.Web.Http.Results;
using UnitTestProject1.TestData;

namespace UnitTestProject1.ControllerTests
{
    [TestClass]
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProdService;
        private ProductsController _controller;

        public ProductControllerTests()
        {
            _mockProdService = new Mock<IProductService>();
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts_Ok()
        {
            _mockProdService.Setup(p => p.GetAll()).Returns(TestData.TestData.Products()).Verifiable();

            _controller = new ProductsController(_mockProdService.Object, TestMapperConfig.Mapper());
            var result = _controller.Get();
            var contentResult = result as OkNegotiatedContentResult<ItemLists<ProductDto>>;

            _mockProdService.Verify(p => p.GetAll(), Times.Once);

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.Items.Count());
        }

        [TestMethod]
        public async Task GetProductByName_ShouldReturnProduct_Ok()
        {
            _mockProdService.Setup(p => p.GetById(It.IsAny<Guid>())).ReturnsAsync(TestData.TestData.Products().First()).Verifiable();
            _controller = new ProductsController(_mockProdService.Object, TestMapperConfig.Mapper());
            var result = await _controller.Get(Guid.NewGuid());
            var contentResult = (OkNegotiatedContentResult<ProductDto>) result;

            _mockProdService.Verify(p => p.GetById(It.IsAny<Guid>()), Times.Once);

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Prod1", contentResult.Content.Name);
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsOk()
        {
            _mockProdService.Setup(p => p.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(true)).Verifiable();
            _controller = new ProductsController(_mockProdService.Object, TestMapperConfig.Mapper());
            var result = await _controller.Delete(Guid.NewGuid());
            _mockProdService.Verify(p => p.Delete(It.IsAny<Guid>()), Times.Once);

        }

        public void CreateNewProduct_ShouldReturn_Ok()
        {
            //some more tests here
        }

        public void EditProduct_ShouldReturn_Ok()
        {
            // running out of time, will test service instead
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using refactor_me.Controllers;
using refactor_me.Services;

namespace UnitTestProject1.ControllerTests
{
    [TestClass]
    public class ProductOptionControllerTests
    {
        private readonly Mock<IProductOptionsService> _mockService;
        private ProductsController _controller;

        public ProductOptionControllerTests()
        {
            _mockService = new Mock<IProductOptionsService>();
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts_Ok()
        {

        }
    }
}

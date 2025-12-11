using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OnlinePharmacy.Api.Controllers;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlinePharmacy.Tests
{
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoriesController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsList()
        {
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<Category> { new Category(), new Category() });

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsType<List<Category>>(okResult.Value);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task Create_ReturnsOk_WithCategory()
        {
            var newCat = new Category { Id = 1, Name = "Vitamins" };
            _mockService.Setup(s => s.CreateAsync(It.IsAny<CategoryDto>()))
                        .ReturnsAsync(newCat);

            var result = await _controller.Create(new CategoryDto { Name = "Vitamins" });

            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("Vitamins", item.Name);
        }
    }
}

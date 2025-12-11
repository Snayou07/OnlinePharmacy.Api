using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OnlinePharmacy.Api.Controllers;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace OnlinePharmacy.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockService;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockService = new Mock<IOrderService>();
            
            _controller = new OrdersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<Order>());

            var result = await _controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenOrderProcessed()
        {
            // Arrange
            var orderResult = new Order { Id = 55, TotalAmount = 1000 };
            _mockService.Setup(s => s.CreateOrderAsync(It.IsAny<CreateOrderDto>()))
                        .ReturnsAsync(orderResult);

            var dto = new CreateOrderDto { CustomerName = "Client" };

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // 200 OK
            var order = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(55, order.Id);
            Assert.Equal(1000, order.TotalAmount);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenBusinessLogicFails()
        {
            // Arrange: Сервіс каже "Немає товару на складі"
            _mockService.Setup(s => s.CreateOrderAsync(It.IsAny<CreateOrderDto>()))
                        .ThrowsAsync(new ArgumentException("Недостатньо товару"));

            // Act
            var result = await _controller.Create(new CreateOrderDto());

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result); // 400 Bad Request

           
            Assert.NotNull(badRequest.Value);
        }
    }
}

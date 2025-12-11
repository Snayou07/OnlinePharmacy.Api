using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OnlinePharmacy.Api.Controllers;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OnlinePharmacy.Tests
{
    public class MedicinesControllerTests
    {
        private readonly Mock<IMedicineService> _mockService;
        private readonly MedicinesController _controller;

        public MedicinesControllerTests()
        {
            // Ініціалізація перед кожним тестом
            _mockService = new Mock<IMedicineService>();
            _controller = new MedicinesController(_mockService.Object);
        }

        // --- ТЕСТИ НА ОТРИМАННЯ (GET) ---

        [Fact]
        public async Task GetAll_ReturnsOk_WithListOfMedicines()
        {
            // Arrange (Підготовка)
            _mockService.Setup(s => s.GetAllAsync())
                        .ReturnsAsync(new List<Medicine> { new Medicine(), new Medicine() });

            // Act (Дія)
            var result = await _controller.GetAll();

            // Assert (Перевірка)
            var okResult = Assert.IsType<OkObjectResult>(result); // Має бути 200 OK
            var list = Assert.IsType<List<Medicine>>(okResult.Value); // Має бути список
            Assert.Equal(2, list.Count); // У списку 2 елементи
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenMedicineExists()
        {
            // Arrange
            long testId = 1;
            _mockService.Setup(s => s.GetByIdAsync(testId))
                        .ReturnsAsync(new Medicine { Id = testId, Name = "Aspirin" });

            // Act
            var result = await _controller.GetById(testId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<Medicine>(okResult.Value);
            Assert.Equal(testId, item.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMedicineDoesNotExist()
        {
            // Arrange: Сервіс повертає null
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((Medicine?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Має бути 404
        }

        // --- ТЕСТИ НА СТВОРЕННЯ (POST) ---

        [Fact]
        public async Task Create_ReturnsCreated_WhenDtoIsValid()
        {
            // Arrange
            var dto = new MedicineDto { Name = "New Med" };
            var createdMed = new Medicine { Id = 10, Name = "New Med" };

            _mockService.Setup(s => s.CreateAsync(It.IsAny<MedicineDto>()))
                        .ReturnsAsync(createdMed);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result); // 201 Created
            Assert.Equal(10, ((Medicine)createdResult.Value).Id);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange: Додаємо помилку валідації вручну
            _controller.ModelState.AddModelError("Price", "Required");

            // Act
            var result = await _controller.Create(new MedicineDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result); // 400 Bad Request
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenServiceThrowsException()
        {
            // Arrange: Імітуємо ситуацію, коли сервіс каже "Такий препарат вже є"
            _mockService.Setup(s => s.CreateAsync(It.IsAny<MedicineDto>()))
                        .ThrowsAsync(new ArgumentException("Дублікат!"));

            // Act
            var result = await _controller.Create(new MedicineDto());

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            // Перевіряємо, що ми не впали, а віддали JSON з помилкою
            Assert.NotNull(badRequest.Value);
        }

        // --- ТЕСТИ НА ОНОВЛЕННЯ (PUT) ---

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<MedicineDto>()))
                        .ReturnsAsync(true); // Успіх

            // Act
            var result = await _controller.Update(1, new MedicineDto());

            // Assert
            Assert.IsType<NoContentResult>(result); // 204 No Content
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenIdIsWrong()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<MedicineDto>()))
                        .ReturnsAsync(false); // Не знайдено

            // Act
            var result = await _controller.Update(1, new MedicineDto());

            // Assert
            Assert.IsType<NotFoundResult>(result); // 404
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenValidationFails()
        {
            _controller.ModelState.AddModelError("Error", "Error");
            var result = await _controller.Update(1, new MedicineDto());
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // --- ТЕСТИ НА ВИДАЛЕННЯ (DELETE) ---

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(false);
            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
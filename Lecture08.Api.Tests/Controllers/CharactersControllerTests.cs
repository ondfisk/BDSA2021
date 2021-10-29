using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lecture08.Api.Controllers;
using Lecture08.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static Lecture08.Core.Status;

namespace Lecture08.Api.Tests.Controllers
{
    public class CharactersControllerTests
    {
        [Fact]
        public async Task Create_creates_Character()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var toCreate = new CharacterCreateDTO();
            var created = new CharacterDetailsDTO(1, "Clark", "Kent", "Superman", "Metropolis", DateTime.Parse("1938-04-18"), "Reporter", new HashSet<string>());
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var result = await controller.Post(toCreate) as CreatedAtRouteResult;

            // Assert
            Assert.Equal(created, result.Value);
            Assert.Equal("Get", result.RouteName);
            Assert.Equal(KeyValuePair.Create("Id", 1 as object), result.RouteValues.Single());
        }

        [Fact]
        public async Task Get_returns_Characters_from_repo()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var expected = new CharacterDTO[0];
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.ReadAsync()).ReturnsAsync(expected);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var actual = await controller.Get();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Get_given_non_existing_returns_NotFound()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(CharacterDetailsDTO));
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var response = await controller.Get(42);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Get_given_existing_returns_character()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var repository = new Mock<ICharacterRepository>();
            var character = new CharacterDetailsDTO(1, "Clark", "Kent", "Superman", "Metropolis", DateTime.Parse("1931-01-01"), "Reporter", new HashSet<string>());
            repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(character);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var response = await controller.Get(1) as OkObjectResult;

            // Assert
            Assert.Equal(character, response.Value);
        }

        [Fact]
        public async Task Put_updates_Character()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var character = new CharacterUpdateDTO();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.UpdateAsync(character)).ReturnsAsync(Updated);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var response = await controller.Put(1, character);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task Put_given_unknown_id_returns_NotFound()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var character = new CharacterUpdateDTO();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.UpdateAsync(character)).ReturnsAsync(NotFound);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var response = await controller.Put(1, character);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Delete_given_non_existing_returns_NotFound()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.DeleteAsync(42)).ReturnsAsync(Status.NotFound);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var response = await controller.Delete(42);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Delete_given_existing_returns_NoContent()
        {
            // Arrange
            var logger = new Mock<ILogger<CharactersController>>();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.DeleteAsync(1)).ReturnsAsync(Status.Deleted);
            var controller = new CharactersController(logger.Object, repository.Object);

            // Act
            var response = await controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }
    }
}

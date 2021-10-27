using System;
using System.Threading.Tasks;
using Lecture08.Api.Controllers;
using Lecture08.Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static Lecture08.Core.Response;

namespace Lecture08.Api.Tests.Controllers
{
    public class CharactersControllerTests
    {
        [Fact]
        public async Task Get_returns_Characters_from_repo()
        {
            // Arrange
            var expected = new CharacterDTO[0];
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.ReadAsync()).ReturnsAsync(expected);
            var controller = new CharactersController(repository.Object);

            // Act
            var actual = await controller.Get();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Put_updates_Character()
        {
            // Arrange
            var character = new CharacterUpdateDTO();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.UpdateAsync(character)).ReturnsAsync(Updated);
            var controller = new CharactersController(repository.Object);

            // Act
            var result = await controller.Put(1, character);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

                [Fact]
        public async Task Put_given_unknown_id_returns_NotFound()
        {
            // Arrange
            var character = new CharacterUpdateDTO();
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(m => m.UpdateAsync(character)).ReturnsAsync(NotFound);
            var controller = new CharactersController(repository.Object);

            // Act
            var result = await controller.Put(1, character);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

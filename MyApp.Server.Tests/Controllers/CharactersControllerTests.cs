namespace MyApp.Server.Tests.Controllers;

public class CharactersControllerTests
{
    [Fact]
    public async Task Create_creates_Character()
    {
        // Arrange
        var logger = new Mock<ILogger<CharactersController>>();
        var toCreate = new CharacterCreateDto();
        var created = new CharacterDetailsDto(1, "Superman", "Clark", "Kent", "Metropolis", Male, 1938, "Reporter", "https://images.com/superman.png", new HashSet<string>());
        var repository = new Mock<ICharacterRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new CharactersController(logger.Object, repository.Object);

        // Act
        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        // Assert
        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }

    [Fact]
    public async Task Get_returns_Characters_from_repo()
    {
        // Arrange
        var logger = new Mock<ILogger<CharactersController>>();
        var expected = Array.Empty<CharacterDto>();
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
        repository.Setup(m => m.ReadAsync(42)).ReturnsAsync(default(CharacterDetailsDto));
        var controller = new CharactersController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(42);

        // Assert
        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Get_given_existing_returns_character()
    {
        // Arrange
        var logger = new Mock<ILogger<CharactersController>>();
        var repository = new Mock<ICharacterRepository>();
        var character = new CharacterDetailsDto(1, "Superman", "Clark", "Kent", "Metropolis", Male, 1938, "Reporter", "https://images.com/superman.png", new HashSet<string>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(character);
        var controller = new CharactersController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(1);

        // Assert
        Assert.Equal(character, response.Value);
    }

    [Fact]
    public async Task Put_updates_Character()
    {
        // Arrange
        var logger = new Mock<ILogger<CharactersController>>();
        var character = new CharacterUpdateDto();
        var repository = new Mock<ICharacterRepository>();
        repository.Setup(m => m.UpdateAsync(1, character)).ReturnsAsync(Updated);
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
        var character = new CharacterUpdateDto();
        var repository = new Mock<ICharacterRepository>();
        repository.Setup(m => m.UpdateAsync(1, character)).ReturnsAsync(NotFound);
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

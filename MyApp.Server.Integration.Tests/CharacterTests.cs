namespace MyApp.Server.Integration.Tests;

public class CharacterTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public CharacterTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_returns_characters()
    {
        var characters = await _client.GetFromJsonAsync<CharacterDto[]>("/api/characters");

        Assert.NotNull(characters);
        Assert.True(characters.Length >= 4);
        Assert.Contains(characters, c => c.AlterEgo == "Batman");
    }

    [Fact]
    public async Task Post_returns_Created_with_location()
    {
        var character = new CharacterCreateDto
        {
            GivenName = "Harleen",
            Surname = "Quinzel",
            AlterEgo = "Harley Quinn",
            FirstAppearance = 1992,
            Occupation = "Former psychiatrist",
            City = "Gotham City",
            Gender = Female,
            ImageUrl = "https://localhost/images/harley-quinn.png",
            Powers = new HashSet<string> { "complete unpredictability", "superhuman agility", "skilled fighter", "intelligence", "emotional manipulation", "immunity to toxins" }
        };

        var response = await _client.PostAsJsonAsync("/api/characters", character);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        Assert.Equal(new Uri("http://localhost/api/Characters/5"), response.Headers.Location);

        var created = await response.Content.ReadFromJsonAsync<CharacterDetailsDto>();

        Assert.NotNull(created);
        Assert.Equal(5, created.Id);
        Assert.Equal("Harleen", created.GivenName);
        Assert.Equal("Quinzel", created.Surname);
        Assert.Equal("Harley Quinn", created.AlterEgo);
        Assert.Equal("Gotham City", created.City);
        Assert.Equal("Former psychiatrist", created.Occupation);
        Assert.Equal(1992, created.FirstAppearance);
        Assert.Equal(Female, created.Gender);
        Assert.Equal("https://localhost/images/harley-quinn.png", created.ImageUrl);
        Assert.True(created.Powers.SetEquals(new[] { "complete unpredictability", "superhuman agility", "skilled fighter", "intelligence", "emotional manipulation", "immunity to toxins" }));
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddKeyPerFile("/run/secrets", optional: true);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(c =>
{
    c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    c.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApp.Api", Version = "v1" });
    c.UseInlineDefinitionsForEnums();
}); 
builder.Services.AddDbContext<ComicsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Comics")));
builder.Services.AddScoped<IComicsContext, ComicsContext>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Seed();

app.Run();

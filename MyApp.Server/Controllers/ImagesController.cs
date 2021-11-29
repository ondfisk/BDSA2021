namespace MyApp.Server.Controllers;

public class ImagesController : Controller
{
    private readonly IImageRepository _repository;

    private readonly IReadOnlyCollection<string> _allowedContentTypes = new[]
    {
            "image/gif",
            "image/jpeg",
            "image/png"
        };

    public ImagesController(IImageRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Guid name, [FromForm] IFormFile file)
    {
        if (!_allowedContentTypes.Contains(file.ContentType))
        {
            return BadRequest("Content type not allowed");
        }

        var (status, uri) = await _repository.CreateImageAsync(name.ToString(), file.ContentType, file.OpenReadStream());

        return status == Status.Created
            ? new CreatedResult(uri, null)
            : status.ToActionResult();
    }
}

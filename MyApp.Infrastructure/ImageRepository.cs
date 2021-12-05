namespace MyApp.Infrastructure;

public class ImageRepository : IImageRepository
{
    private readonly BlobContainerClient _client;

    public ImageRepository(BlobContainerClient client)
    {
        _client = client;
    }

    public async Task<(Status status, Uri uri)> CreateImageAsync(string name, string contentType, Stream stream)
    {
        var client = _client.GetBlockBlobClient(name);

        await client.UploadAsync(stream);

        await client.SetHttpHeadersAsync(new Azure.Storage.Blobs.Models.BlobHttpHeaders {  ContentType = contentType });

        return (Created, client.Uri);
    }
}

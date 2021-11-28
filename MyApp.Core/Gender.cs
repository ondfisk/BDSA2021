namespace MyApp.Core
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Female,
        Male,
        Other
    }
}

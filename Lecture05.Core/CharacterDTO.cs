namespace Lecture05.Core
{
    public record CharacterDTO
    {
        public int? Id { get; init; }
        public string Name { get; init; }
        public string AlterEgo { get; init; }
    }
}

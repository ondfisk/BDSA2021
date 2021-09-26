namespace Lecture04
{
    public record FuturamaCharacterDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Species { get; init; }
        public string Planet { get; init; }
        public int? ActorId { get; init; }
        public string ActorName { get; init; }
    }
}
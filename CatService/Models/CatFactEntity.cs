namespace CatService.Models
{
    public record CatFactEntity
    {
        public List<CatFactDetails> Data { get; set; }
    }

    public record CatFactDetails
    {
        public string? Fact { get; set; }
        public int Length { get; set; }
    }
}
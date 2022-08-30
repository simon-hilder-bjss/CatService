namespace CatService.Models
{
    public class CatFactResponse
    {
        public List<CatFactDetails> Facts { get; set; }
        public string SubstitutedName { get; set; }

        public CatFactResponse(CatFactEntity facts, string substitutedName)
        {
            Facts = facts.Data;
            SubstitutedName = substitutedName;
        }
    }
}

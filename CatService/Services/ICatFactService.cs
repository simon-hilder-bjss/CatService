using CatService.Models;

namespace CatService.Services
{
    public interface ICatFactService
    {
        public Task<CatFactEntity> GetCatFactsWithNameSubstitution(string name, int? maxLength, int? factLimit);
    }
}
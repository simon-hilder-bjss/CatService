using TmUnitTesting.Models;

namespace TmUnitTesting.Services
{
    public interface ICatFactService
    {
        public Task<CatFactEntity> GetCatFactsWithNameSubstitution(string name, int? maxLength, int? factLimit);
    }
}
using TmUnitTesting.Models;

namespace TmUnitTesting.Services
{
    public interface ICatFactService
    {
        public Task<CatFactEntity> GetCatFactsWithName(string name, int? maxLength, int? factLimit);
    }
}
using TmUnitTesting.Models;

namespace TmUnitTesting.Repositories
{
    public interface ICatFactRepository
    {
        public Task<CatFactEntity> GetCatFacts(int? maxLength, int? limit);
    }
}

using CatService.Models;

namespace CatService.Repositories
{
    public interface ICatFactRepository
    {
        public Task<CatFactEntity> GetCatFacts(int? maxLength, int? limit);
    }
}

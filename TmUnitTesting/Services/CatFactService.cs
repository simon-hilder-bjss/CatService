using TmUnitTesting.Models;
using TmUnitTesting.Repositories;

namespace TmUnitTesting.Services
{
    public class CatFactService : ICatFactService
    {
        private ICatFactRepository _catFactRepository;

        public CatFactService(ICatFactRepository catFactRepository)
        {
            _catFactRepository = catFactRepository;
        }

        public async Task<CatFactEntity> GetCatFactsWithName(string name, int? maxLength, int? factLimit)
        {
            var catFacts = await _catFactRepository.GetCatFacts(maxLength, factLimit);
            foreach (var catFact in catFacts.Data)
            {
                catFact.Fact = catFact.Fact?.Replace("cat", name);
            }
            return catFacts;
        }
    }
}

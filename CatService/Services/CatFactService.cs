/* https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs */

using System.Text.RegularExpressions;
using CatService.Models;
using CatService.Repositories;

namespace CatService.Services
{
    public class CatFactService : ICatFactService
    {
        private readonly ICatFactRepository _catFactRepository;

        public CatFactService(ICatFactRepository catFactRepository)
        {
            _catFactRepository = catFactRepository;
        }

        public async Task<CatFactEntity> GetCatFactsWithNameSubstitution(string name, int? maxLength, int? factLimit)
        {
            var catFacts = await _catFactRepository.GetCatFacts(maxLength, factLimit);
            foreach (var catFact in catFacts.Data)
            {
                catFact.Fact = Regex.Replace(catFact.Fact, "[Cc]at", name);
            }
            return catFacts;
        }
    }
}

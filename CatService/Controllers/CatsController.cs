using Microsoft.AspNetCore.Mvc;
using CatService.Models;
using CatService.Services;

namespace CatService.Controllers
{
    public class CatsController : Controller
    {
        private readonly ILogger<CatsController> _logger;
        private readonly ICatFactService _catFactService;

        public CatsController(ILogger<CatsController> logger, ICatFactService catFactService)
        {
            _logger = logger;
            _catFactService = catFactService;
        }

        [HttpGet]
        public async Task<CatFactResponse> CatFacts(string name, int? maxLength, int? factLimit)
        {
            _logger.LogInformation("Get CatFacts triggered with the following name: {name}", name);
            var factsWithSubstitions = await _catFactService.GetCatFactsWithNameSubstitution(name, maxLength, factLimit);
            return new CatFactResponse(factsWithSubstitions, name);
        }
    }
}
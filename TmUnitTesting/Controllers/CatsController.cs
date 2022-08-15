using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TmUnitTesting.Models;
using TmUnitTesting.Services;

namespace TmUnitTesting.Controllers
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
        public async Task<CatFactResponse> GetCatFacts(string name, int? maxLength, int? factLimit)
        {
            _logger.LogInformation("{0} called by user", System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            var factsWithSubstitions = await _catFactService.GetCatFactsWithNameSubstitution(name, maxLength, factLimit);
            return new CatFactResponse(factsWithSubstitions, name);
        }
    }
}
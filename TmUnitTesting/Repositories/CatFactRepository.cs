using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using TmUnitTesting.Models;

namespace TmUnitTesting.Repositories
{
    public class CatFactRepository : ICatFactRepository
    {
        private static readonly string _baseUrl = "https://catfact.ninja/facts";
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public CatFactRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<CatFactEntity> GetCatFacts(int? maxLength, int? factLimit)
        {
            var queryParams = ConstructQueryString(maxLength, factLimit);
            var response = await _httpClient.GetAsync($"{_baseUrl}{queryParams}");
            return await JsonSerializer.DeserializeAsync<CatFactEntity>(response.Content.ReadAsStream(), serializeOptions);
        }

        private static NameValueCollection ConstructQueryString(int? maxLength, int? factLimit)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (maxLength.HasValue)
            {
                query["?max_length"] = maxLength.ToString();
            }
            if (factLimit.HasValue)
            {
                if (maxLength.HasValue)
                {
                    query["&limit"] = factLimit.ToString();
                }
                else
                {
                    query["?limit"] = factLimit.ToString();
                }
            }
            return query;
        }
    }
}

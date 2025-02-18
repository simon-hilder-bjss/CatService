﻿using System.Text.Json;
using CatService.Models;

namespace CatService.Repositories
{
    public class CatFactRepository : ICatFactRepository
    {
        private static readonly string _baseUrl = "https://catfact.ninja/facts";
        private static readonly string _maxLengthParamKey = "max_length";
        private static readonly string _factLimitParamKey = "limit";
        private readonly HttpClient _httpClient;

        public CatFactRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<CatFactEntity> GetCatFacts(int? maxLength, int? factLimit)
        {
            var paramList = new List<KeyValuePair<string, string>>();
            if (maxLength.HasValue)
            {
                paramList.Add(new KeyValuePair<string, string>(_maxLengthParamKey, maxLength.ToString()));
            }
            if (factLimit.HasValue)
            {
                paramList.Add(new KeyValuePair<string, string>(_factLimitParamKey, factLimit.ToString()));
            }
            var queryParams = Helpers.ConstructQueryString(paramList);
            var response = await _httpClient.GetAsync($"{_baseUrl}{queryParams}");
            return await JsonSerializer.DeserializeAsync<CatFactEntity>(response.Content.ReadAsStream(), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}

using System.Collections.Specialized;
using System.Web;

namespace TmUnitTesting
{
    public static class Helpers
    {
        public static string ConstructQueryString(List<KeyValuePair<string, string>> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var parameter in parameters)
            {
                query.Add(parameter.Key, parameter.Value);
            }
            return string.IsNullOrEmpty(query.ToString()) ? "" : $"?{query}";
        }
    }
}

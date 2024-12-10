using Newtonsoft.Json;

namespace Aura.PersonalSafety.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}

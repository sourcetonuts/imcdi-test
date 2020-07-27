using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TaxService.Interfaces;

namespace TaxCalculator.Taxjar
{
    public class CalculatorJSON
        : CalculatorBaseTaxjar
        , ITaxService
    {
        static JsonSerializerSettings SettingsSerializer;
        static AuthenticationHeaderValue AuthKey;

        static CalculatorJSON()
        {
            SettingsSerializer = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            AuthKey = new AuthenticationHeaderValue("Bearer", API_KEY);
        }

        public async Task<decimal> GetTaxRate(string zipcode)
        {
            // GET https://api.taxjar.com/v2/rates/:zip
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = AuthKey;

                var response = await httpClient.GetStringAsync($"https://api.taxjar.com/v2/rates/{zipcode}");
                //response.Wait();
                var jsonString = response;
                var data = JsonConvert.DeserializeObject<TaxData>(jsonString, SettingsSerializer);
                return data.Rate.CombinedRate;
            }
        }

        public class TaxData
        {
            [JsonProperty("rate")]
            public TaxRateData Rate { get; set; }
        }

        public class TaxRateData
        {
            [JsonProperty("combined_rate")]
            public decimal CombinedRate { get; set; }
        }
    }
}

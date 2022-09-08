using GenerateTableFipeVehicles.Entities;
using Newtonsoft.Json;

namespace GenerateTableFipeVehicles.Infra.Api
{
    internal class ExternalApi
    {
        public static async Task<List<Vehicles>> GetBrand(string type)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://parallelum.com.br/fipe/api/v1/{type}/marcas");
            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<List<Vehicles>>(jsonString);
            return jsonObject;
        }

        public static async Task<AllVehicles> GetModels(string type, string code)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://parallelum.com.br/fipe/api/v1/{type}/marcas/{code}/modelos");
            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<AllVehicles>(jsonString);
            return jsonObject;
        }

        public static async Task<List<Vehicles>> GetYears(string type, string codeBrand, string codeModel)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://parallelum.com.br/fipe/api/v1/{type}/marcas/{codeBrand}/modelos/{codeModel}/anos");
            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<List<Vehicles>>(jsonString);

            jsonObject.ForEach(x => x.CodModel = codeModel);

            return jsonObject;
        }

        public static async Task<Vehicles> GetVehicle(string type, string codeBrand, string codeModel, string codeYear)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://parallelum.com.br/fipe/api/v1/{type}/marcas/{codeBrand}/modelos/{codeModel}/anos/{codeYear}");
            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<Vehicles>(jsonString);

            return jsonObject;
        }
    }
}

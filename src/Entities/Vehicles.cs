using Newtonsoft.Json;


namespace GenerateTableFipeVehicles.Entities
{
    internal class Vehicles
    {
        [JsonProperty(PropertyName = "nome")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "codigo")]
        public string Code { get; set; }
        public string CodModel { get; set; }

        //
        [JsonProperty(PropertyName = "Valor")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "Marca")]
        public string Brand { get; set; }

        [JsonProperty(PropertyName = "Modelo")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "AnoModelo")]
        public string YearModel { get; set; }

        [JsonProperty(PropertyName = "Combustivel")]
        public string Fuel { get; set; }

        [JsonProperty(PropertyName = "CodigoFipe")]
        public string FipeCode { get; set; }

        [JsonProperty(PropertyName = "MesReferencia")]
        public string ReferenceMonth { get; set; }

        [JsonProperty(PropertyName = "TipoVeiculo")]
        public string VehicleType { get; set; }

        [JsonProperty(PropertyName = "SiglaCombustivel")]
        public string AcronymFuel { get; set; }

        public Vehicles()
        {
        }

        public Vehicles(string name, string code, string codModel, string value, string brand, string model, string yearModel, string fuel, string fipeCode, string referenceMonth, string vehicleType, string acronymFuel)
        {
            Name = name;
            Code = code;
            CodModel = codModel;
            Value = value;
            Brand = brand;
            Model = model;
            YearModel = yearModel;
            Fuel = fuel;
            FipeCode = fipeCode;
            ReferenceMonth = referenceMonth;
            VehicleType = vehicleType;
            AcronymFuel = acronymFuel;
        }
    }
}

using GenerateTableFipeVehicles.Entities;
using GenerateTableFipeVehicles.Exceptions;
using GenerateTableFipeVehicles.Infra.Api;
using System.Globalization;

namespace GenerateTableFipeVehicles.Services
{
    internal class UserInteraction
    {
        public static async Task Interaction()
        {
            int vehicleType = 4;
            string vehicleSearch = null;
            do
            {
                try
                {
                    Console.WriteLine("Gerador de Tabelas Fipes");
                    Console.WriteLine("1 = Carro");
                    Console.WriteLine("2 = Moto");
                    Console.WriteLine("3 = Caminhao");
                    Console.WriteLine("4 = Sair");
                    Console.WriteLine();
                    Console.Write("Opcao: ");
                    vehicleType = int.Parse(Console.ReadLine());

                    if (vehicleType > 4 || vehicleType < 1)
                        throw new ExceptionsValueOutOfTheList("Valor invalido, tente novamente!");

                    if (vehicleType == 1)
                    {
                        vehicleSearch = "carros";
                    }
                    else if (vehicleType == 2)
                    {
                        vehicleSearch = "motos";
                    }
                    else if (vehicleType == 3)
                    {
                        vehicleSearch = "caminhoes";
                    }
                    else if (vehicleType == 4)
                    {
                        Console.WriteLine("Encerrando...");
                        break;
                    }

                    Console.Write("Informe a marca: ");
                    string vehicleBrand = Console.ReadLine();

                    List<Vehicles> list = await ExternalApi.GetBrand(vehicleSearch);

                    var codeBrand = list.Where(x => x.Name.Contains(vehicleBrand.ToUpper())).Select(x => x.Code);

                    if (codeBrand.Count() == 0)
                    {
                        codeBrand = list.Where(x => x.Name.Contains(String.Format(vehicleBrand[0].ToString()).ToUpper() + vehicleBrand.Substring(1))).Select(x => x.Code);
                    }
                    else if (codeBrand.Count() == 0)
                    {
                        TextInfo vehicleBrandFormat = new CultureInfo("en-US", true).TextInfo;
                        codeBrand = list.Where(x => x.Name.Contains(vehicleBrandFormat.ToTitleCase(vehicleBrand))).Select(x => x.Code);
                    }
                    if (codeBrand.Count() > 0)
                    {
                        Console.Write("Informe o modelo: ");
                        string modelVehicle = Console.ReadLine();

                        var models = await ExternalApi.GetModels(vehicleSearch, codeBrand.First());

                        var vehiclesFound = models.Vehicles.Where(x => x.Name.Contains(modelVehicle.ToUpper())).Select(x => x.Code);

                        if (vehiclesFound.Count() == 0)
                        {
                            vehiclesFound = models.Vehicles.Where(x => x.Name.Contains(String.Format(modelVehicle[0].ToString()).ToUpper() + modelVehicle.Substring(1))).Select(x => x.Code);
                        }
                        else if (vehiclesFound.Count() == 0)
                        {
                            TextInfo modelVehicleFormat = new CultureInfo("en-US", true).TextInfo;

                            vehiclesFound = models.Vehicles.Where(x => x.Name.Contains(modelVehicleFormat.ToTitleCase(modelVehicle))).Select(x => x.Code);
                        }

                        if (vehiclesFound.Count() > 0)
                        {

                            Console.Write("Informe o ano: ");
                            string yearVehicle = Console.ReadLine();

                            List<Vehicles> listAuxYearVehicle = new List<Vehicles>();

                            Console.WriteLine();
                            Console.WriteLine("Filtrando o ano do veículo...");

                            foreach (var codModel in vehiclesFound)
                            {
                                List<Vehicles> listYearVehicle = await ExternalApi.GetYears(vehicleSearch, codeBrand.First(), codModel);

                                foreach (var item in listYearVehicle)
                                {
                                    listAuxYearVehicle.Add(item);
                                }
                            }

                            listAuxYearVehicle = listAuxYearVehicle.Where(x => x.Name.Contains(yearVehicle)).ToList();

                            if (listAuxYearVehicle.Count > 0)
                            {
                                Console.WriteLine("Gerando relatório, aguarde um instante...");
                                List<Vehicles> listAuxVehicle = new List<Vehicles>();

                                foreach (var item in listAuxYearVehicle)
                                {
                                    Vehicles vehicle = await ExternalApi.GetVehicle(vehicleSearch, codeBrand.First(), item.CodModel, item.Code);

                                    listAuxVehicle.Add(vehicle);
                                }
                                await ReportGenerator.ExcelGeneratorVehicles(listAuxVehicle);

                                Console.WriteLine("Relatório Gerado.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Ano não encontrado");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Modelo não Encontrado, Tente novamente.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Marca não encontrada, tente novamente");
                        Console.WriteLine();
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor insira um numero valido");
                    vehicleType = 0;
                }
                catch (IOException e)
                {
                    Console.WriteLine("Arquivo excel em uso, feche e tente novamente");
                    vehicleType = 0;
                }
                catch (ExceptionsValueOutOfTheList e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Algo deu errado");
                    Console.WriteLine(e.Message);
                }
            } while (vehicleType != 4);
        }
    }
}

using ClosedXML.Excel;
using GenerateTableFipeVehicles.Entities;
using System.Diagnostics;

namespace GenerateTableFipeVehicles.Services
{
    internal class ReportGenerator
    {
        public static async Task ExcelGeneratorVehicles(List<Vehicles> listVehicles)
        {

            using (var workbook = new XLWorkbook())
            {
                int i = 2;

                var worksheet = workbook.Worksheets.Add("Planilha1");

                worksheet.Cell("A1").Value = "Modelo";
                worksheet.Cell("B1").Value = "Ano Modelo";
                worksheet.Cell("C1").Value = "Valor";
                worksheet.Cell("D1").Value = "Combustivel";
                worksheet.Cell("E1").Value = "Codigo Fipe";
                worksheet.Cell("F1").Value = "Mes Referencia";
                worksheet.Cell("G1").Value = "Tipo Veiculo";
                worksheet.Cell("H1").Value = "Sigla Combustivel";

                foreach (var item in listVehicles)
                {
                    worksheet.Cell("A" + i).Value = item.Model;
                    worksheet.Cell("B" + i).SetValue<string>(item.YearModel.ToString());
                    worksheet.Cell("C" + i).SetValue<string>(item.Value);
                    worksheet.Cell("D" + i).Value = item.Fuel;
                    worksheet.Cell("E" + i).SetValue<string>(item.FipeCode);
                    worksheet.Cell("F" + i).Value = item.ReferenceMonth;
                    worksheet.Cell("G" + i).Value = item.VehicleType;
                    worksheet.Cell("H" + i).Value = item.AcronymFuel;

                    i++;
                }
                workbook.SaveAs(@"c:\\temp\VehiclesFipe.xlsx");
            }
            Process.Start(new ProcessStartInfo(@"c:\\temp\VehiclesFipe.xlsx") { UseShellExecute = true });
        }
    }
}

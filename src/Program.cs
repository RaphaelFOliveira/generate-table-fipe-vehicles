using GenerateTableFipeVehicles.Services;

namespace GenerateTableFipeVehicles
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await UserInteraction.Interaction();
        }
    }
}
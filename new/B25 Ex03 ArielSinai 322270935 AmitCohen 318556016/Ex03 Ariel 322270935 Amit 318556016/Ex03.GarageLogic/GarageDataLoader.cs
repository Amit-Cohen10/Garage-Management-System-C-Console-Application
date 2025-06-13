using System;
using System.IO;

namespace Ex03.GarageLogic
{
    public static class GarageDataLoader
    {
        public static void LoadVehiclesFromFile(Garage i_Garage, string i_FilePath)
        {
            if (!File.Exists(i_FilePath))
            {
                throw new FileNotFoundException(string.Format("File {0} not found", i_FilePath));
            }

            string[] lines = File.ReadAllLines(i_FilePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) ||
                    line.StartsWith("*") ||
                    line.StartsWith("THE FORMAT", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("VehicleType", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                parseAndAddVehicle(i_Garage, line);
            }
        }

        private static void parseAndAddVehicle(Garage i_Garage, string i_Line)
        {
            string[] parts = i_Line.Split(',');

            string vehicleType = parts[0].Trim();
            string licensePlate = parts[1].Trim();
            string modelName = parts[2].Trim();
            float energyPercentage = float.Parse(parts[3].Trim());
            string tiresManufacturer = parts[4].Trim();
            float currentAirPressure = float.Parse(parts[5].Trim());
            string ownerName = parts[6].Trim();
            string ownerPhone = parts[7].Trim();

            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);

            float energyAmount = (energyPercentage / 100f) * vehicle.Engine.MaxEnergyAmount;
            vehicle.Engine.EnergyLeft = energyAmount;
            vehicle.UpdatePercentOfEnergyLeft();

            foreach (Wheel wheel in vehicle.Wheels)
            {
                wheel.ManufacturerName = tiresManufacturer;
                wheel.CurrentAirPressure = currentAirPressure;
            }

            vehicle.LoadExtraData(parts, 8);

            EntryForm entryForm = new EntryForm(ownerName, ownerPhone, vehicle);
            i_Garage.AddVehicle(licensePlate, entryForm);
        }
    }
}
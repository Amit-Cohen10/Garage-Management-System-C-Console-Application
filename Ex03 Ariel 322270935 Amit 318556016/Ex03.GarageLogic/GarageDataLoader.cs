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
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*"))
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

            setVehicleSpecificProperties(vehicle, parts, 8);

            EntryForm entryForm = new EntryForm(ownerName, ownerPhone, vehicle);
            i_Garage.AddVehicle(licensePlate, entryForm);
        }

        private static void setVehicleSpecificProperties(Vehicle i_Vehicle, string[] i_Parts, int i_StartIndex)
        {
            if (i_Vehicle is Car car)
            {
                if (i_Parts.Length > i_StartIndex)
                {
                    car.CarColor = parseEnum<Car.eCarColor>(i_Parts[i_StartIndex].Trim());
                }
                if (i_Parts.Length > i_StartIndex + 1)
                {
                    car.DoorsAmount = (Car.eDoorsAmount)int.Parse(i_Parts[i_StartIndex + 1].Trim());
                }
            }
            else if (i_Vehicle is MotorCycle motorCycle)
            {
                if (i_Parts.Length > i_StartIndex)
                {
                    motorCycle.LicenseType = parseEnum<MotorCycle.eLicenseType>(i_Parts[i_StartIndex].Trim());
                }
                if (i_Parts.Length > i_StartIndex + 1)
                {
                    motorCycle.EngineVolumeCC = int.Parse(i_Parts[i_StartIndex + 1].Trim());
                }
            }
            else if (i_Vehicle is Truck truck)
            {
                if (i_Parts.Length > i_StartIndex)
                {
                    truck.IsCarryingDangerousMaterials = bool.Parse(i_Parts[i_StartIndex].Trim());
                }
                if (i_Parts.Length > i_StartIndex + 1)
                {
                    truck.CargoVolume = float.Parse(i_Parts[i_StartIndex + 1].Trim());
                }
            }
        }

        private static T parseEnum<T>(string i_Value) where T : struct, Enum
        {
            if (Enum.TryParse<T>(i_Value, true, out T result))
            {
                return result;
            }

            throw new ArgumentException(string.Format("Invalid value '{0}' for enum {1}", i_Value, typeof(T).Name));
        }
    }
}
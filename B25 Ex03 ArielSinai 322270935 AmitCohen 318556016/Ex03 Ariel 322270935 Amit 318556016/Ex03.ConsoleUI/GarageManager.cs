using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.EntryForm;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        internal Garage m_Garage;

        public GarageManager()
        {
            m_Garage = new Garage();
        }

        internal void LoadVehiclesFromFile()
        {
            Console.Write("Enter file path (or press Enter for 'Vehicles.db'): ");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = "Vehicles.db";
            }

            try
            {
                GarageDataLoader.LoadVehiclesFromFile(m_Garage, filePath);
                Console.WriteLine("Vehicles loaded successfully from file!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to load vehicles: {0}", ex.Message));
            }
        }

        private string getLicensePlate()
        {
            string licensePlate;

            Console.WriteLine("Please insert license plate number");
            while (true)
            {
                licensePlate = Console.ReadLine();
                if (m_Garage.GarageVehicles.Keys.Contains(licensePlate))
                {
                    break;
                }

                Console.WriteLine("License plate does not exist in our system. Please try again");
            }

            return licensePlate;
        }

        private void getEntryFormAndAddToGarage(string i_LicensePlate, Vehicle i_NewVehicle)
        {
            string ownerName, phoneNumber = "";
            EntryForm entryForm;

            Console.WriteLine("Please enter vehicle owner's name");
            ownerName = Console.ReadLine();
            Console.WriteLine("Please enter owner's phone number");
            phoneNumber = Console.ReadLine();
            if (!long.TryParse(phoneNumber, out _))
            {
                throw new Exception("Phone number should be numeric.");
            }

            entryForm = new EntryForm(ownerName, phoneNumber, i_NewVehicle);
            m_Garage.AddVehicle(i_LicensePlate, entryForm);
        }

        private void printEnumOptionsForUser(Type enumType)
        {
            int typeIndex = 1;

            foreach (Enum value in Enum.GetValues(enumType))
            {
                Console.WriteLine(string.Format("{0} - for {1}", typeIndex, value));
                typeIndex++;
            }
        }

        private int getEnumInputAndCheckValidity(Type enumType)
        {
            string userChoice;
            int userChoiceNumber, minEnumValue, maxEnumValue;
            Array enumValues;

            while (true)
            {
                userChoice = Console.ReadLine();
                try
                {
                    userChoiceNumber = int.Parse(userChoice);
                    enumValues = Enum.GetValues(enumType);
                    minEnumValue = (int)enumValues.GetValue(0);
                    maxEnumValue = (int)enumValues.GetValue(enumValues.Length - 1);
                    if (userChoiceNumber >= minEnumValue && userChoiceNumber <= maxEnumValue)
                    {
                        break;
                    }

                    Console.WriteLine("Invalid input. Try again");
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Input is not numeric. Try again");
                }
            }

            return userChoiceNumber;
        }

        private string getUserVehicleChoice()
        {
            Console.WriteLine("Please choose a vehicle type:");
            List<string> supportedTypes = VehicleCreator.SupportedTypes;

            for (int i = 0; i < supportedTypes.Count; i++)
            {
                Console.WriteLine(string.Format("{0} - {1}", i + 1, supportedTypes[i]));
            }

            while (true)
            {
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice >= 1 && choice <= supportedTypes.Count)
                    {
                        return supportedTypes[choice - 1];
                    }
                    Console.WriteLine("Invalid choice. Try again.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number.");
                }
            }
        }

        internal void AddCarToGarage()
        {
            string licensePlate;
            string vehicleType;
            Vehicle newVehicle;

            Console.WriteLine("Please insert license plate number");
            licensePlate = Console.ReadLine();

            if (m_Garage.GarageVehicles.ContainsKey(licensePlate))
            {
                Console.WriteLine("This vehicle is already in our garage. Vehicle state updated to 'Under Repair'");
                m_Garage.UpdateState(licensePlate, 1);
            }
            else
            {
                Console.WriteLine("Please insert model name");
                string modelName = Console.ReadLine();

                vehicleType = getUserVehicleChoice();
                newVehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);

                try
                {
                    getEntryFormAndAddToGarage(licensePlate, newVehicle);
                    setVehicleProperties(newVehicle);
                    Console.WriteLine("Vehicle added successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("An error occurred: {0}", e.Message));
                }
            }
        }

        private void setVehicleProperties(Vehicle i_Vehicle)
        {
            try
            {
                Console.Write(string.Format("Enter current energy amount (0-{0:F1}): ", i_Vehicle.Engine.MaxEnergyAmount));
                float energyAmount = float.Parse(Console.ReadLine());
                i_Vehicle.Engine.EnergyLeft = energyAmount;
                i_Vehicle.UpdatePercentOfEnergyLeft();

                Console.Write("Enter wheel manufacturer: ");
                string manufacturer = Console.ReadLine();
                Console.Write(string.Format("Enter current air pressure (0-{0:F1}): ", i_Vehicle.Wheels[0].MaxAirPressure));
                float airPressure = float.Parse(Console.ReadLine());

                foreach (Wheel wheel in i_Vehicle.Wheels)
                {
                    wheel.ManufacturerName = manufacturer;
                    wheel.CurrentAirPressure = airPressure;
                }

                if (i_Vehicle is Car car)
                {
                    setCarProperties(car);
                }
                else if (i_Vehicle is MotorCycle motorCycle)
                {
                    setMotorCycleProperties(motorCycle);
                }
                else if (i_Vehicle is Truck truck)
                {
                    setTruckProperties(truck);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error setting vehicle properties: {0}", ex.Message));
            }
        }

        private void setCarProperties(Car i_Car)
        {
            Console.WriteLine("Select car color:");
            printEnumOptionsForUser(typeof(Car.eCarColor));
            int colorChoice = getEnumInputAndCheckValidity(typeof(Car.eCarColor));
            i_Car.CarColor = (Car.eCarColor)colorChoice;

            Console.WriteLine("Select number of doors:");
            printEnumOptionsForUser(typeof(Car.eDoorsAmount));
            int doorsChoice = getEnumInputAndCheckValidity(typeof(Car.eDoorsAmount));
            i_Car.DoorsAmount = (Car.eDoorsAmount)doorsChoice;
        }

        private void setMotorCycleProperties(MotorCycle i_MotorCycle)
        {
            Console.WriteLine("Select license type:");
            printEnumOptionsForUser(typeof(MotorCycle.eLicenseType));
            int licenseChoice = getEnumInputAndCheckValidity(typeof(MotorCycle.eLicenseType));
            i_MotorCycle.LicenseType = (MotorCycle.eLicenseType)licenseChoice;

            Console.Write("Enter engine volume (CC): ");
            int engineVolume = int.Parse(Console.ReadLine());
            i_MotorCycle.EngineVolumeCC = engineVolume;
        }

        private void setTruckProperties(Truck i_Truck)
        {
            Console.Write("Is carrying dangerous materials? (true/false): ");
            bool isDangerous = bool.Parse(Console.ReadLine());
            i_Truck.IsCarryingDangerousMaterials = isDangerous;

            Console.Write("Enter cargo volume: ");
            float cargoVolume = float.Parse(Console.ReadLine());
            i_Truck.CargoVolume = cargoVolume;
        }

        internal void PrintLicensePlateList()
        {
            Console.WriteLine(string.Format(@"Select filter options (comma separated):
0- Show all
1- {0}
2- {1}
3- {2}
Example: For both 'Under Repair' and 'Fixed', type 1,2", eVehicleState.UnderRepair, eVehicleState.Fixed, eVehicleState.Paid));

            string userFilterChoice = Console.ReadLine();
            string[] userFilterChoicesArray = userFilterChoice.Split(',');
            bool showAll = false;
            List<eVehicleState> chosenStates = new List<eVehicleState>();

            foreach (var choice in userFilterChoicesArray)
            {
                if (choice.Trim() == "0")
                {
                    showAll = true;
                    break;
                }

                if (Enum.TryParse(choice.Trim(), out eVehicleState state))
                {
                    chosenStates.Add(state);
                }
            }

            foreach (KeyValuePair<string, EntryForm> vehicle in m_Garage.GarageVehicles)
            {
                if (showAll || chosenStates.Contains(vehicle.Value.VehicleState))
                {
                    Console.WriteLine(vehicle.Key);
                }
            }
        }

        internal void ChangeVehicleState()
        {
            string licensePlate = getLicensePlate();

            Console.WriteLine("Please choose a vehicle state:");
            printEnumOptionsForUser(typeof(eVehicleState));
            int userStateChoiceNumber = getEnumInputAndCheckValidity(typeof(eVehicleState));

            m_Garage.GarageVehicles[licensePlate].VehicleState = (eVehicleState)userStateChoiceNumber;
            Console.WriteLine("State changed successfully.");
        }

        internal void InflateWheelsPressureToMax()
        {
            string licensePlate = getLicensePlate();
            m_Garage.InflateWheelsToMax(licensePlate);
            Console.WriteLine("Wheels inflated successfully.");
        }

        internal void FuelVehicle()
        {
            string licensePlate = getLicensePlate();

            try
            {
                Vehicle vehicle = m_Garage.GarageVehicles[licensePlate].Vehicle;
                FuelEngine vehicleEngine = vehicle.Engine as FuelEngine;

                if (vehicleEngine == null)
                {
                    Console.WriteLine("This is not a fuel type vehicle!");
                    return;
                }

                Console.WriteLine(string.Format(@"Your fuel type is: {0}
Amount of fuel left in tank is {1:F1}", vehicleEngine.FuelType, vehicleEngine.EnergyLeft));

                Console.WriteLine("Please choose a fuel type:");
                printEnumOptionsForUser(typeof(eFuelType));
                eFuelType fuelType = (eFuelType)getEnumInputAndCheckValidity(typeof(eFuelType));

                Console.WriteLine("Please insert amount of fuel to fill:");
                float fuelAmount = float.Parse(Console.ReadLine());

                m_Garage.RefuelVehicle(licensePlate, fuelType, fuelAmount);
                Console.WriteLine("Vehicle fueled successfully.");
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(string.Format("Can only add between {0:F1} - {1:F1}", valueOutOfRangeException.MinValue, valueOutOfRangeException.MaxValue));
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Input is not valid: " + e.Message);
            }
        }

        internal void ChargeElectricVehicle()
        {
            string licensePlate = getLicensePlate();

            try
            {
                Vehicle vehicle = m_Garage.GarageVehicles[licensePlate].Vehicle;
                ElectricEngine vehicleEngine = vehicle.Engine as ElectricEngine;

                if (vehicleEngine == null)
                {
                    Console.WriteLine("This is not an electric type vehicle!");
                    return;
                }

                Console.WriteLine(string.Format(@"Amount of battery left is {0:F1} hours", vehicleEngine.EnergyLeft));
                Console.WriteLine("Please insert amount of minutes to charge:");
                float minutesToCharge = float.Parse(Console.ReadLine());

                m_Garage.ChargeVehicle(licensePlate, minutesToCharge);
                Console.WriteLine("Vehicle charged successfully.");
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                Console.WriteLine(string.Format("Can only add between {0:F1} - {1:F1} minutes", valueOutOfRangeException.MinValue, valueOutOfRangeException.MaxValue));
            }
            catch (Exception e)
            {
                Console.WriteLine("Input is not valid: " + e.Message);
            }
        }

        internal void ShowAllVehicleDataByLicensePlate()
        {
            string licensePlate = getLicensePlate();
            Console.WriteLine(m_Garage.GarageVehicles[licensePlate].ToString());
        }
    }
}
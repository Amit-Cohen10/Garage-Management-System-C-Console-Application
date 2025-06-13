using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class GarageMenuApp
    {
        internal static void RunGarage()
        {
            GarageManager garageManager = new GarageManager();
            eMainMenuAction userActionChoice = default;

            while (!(userActionChoice == eMainMenuAction.Exit))
            {
                Console.Clear();
                userActionChoice = getUserActionChoice();
                Console.Clear();
                switch (userActionChoice)
                {
                    case eMainMenuAction.LoadFromFile:
                        garageManager.LoadVehiclesFromFile();
                        break;

                    case eMainMenuAction.AddCar:
                        garageManager.AddCarToGarage();
                        break;

                    case eMainMenuAction.ShowLicensePlates:
                        garageManager.PrintLicensePlateList();
                        break;

                    case eMainMenuAction.ChangeVehicleState:
                        garageManager.ChangeVehicleState();
                        break;

                    case eMainMenuAction.InflateWheelsToMax:
                        garageManager.InflateWheelsPressureToMax();
                        break;

                    case eMainMenuAction.FuelVehicle:
                        garageManager.FuelVehicle();
                        break;

                    case eMainMenuAction.ChargeVehicle:
                        garageManager.ChargeElectricVehicle();
                        break;

                    case eMainMenuAction.ShowVehicleInfo:
                        garageManager.ShowAllVehicleDataByLicensePlate();
                        break;

                    case eMainMenuAction.Exit:
                        Console.WriteLine("Goodbye!");
                        return;
                }

                Console.WriteLine("Press any key to get back to the Main Menu.");
                Console.ReadLine(); 
            }
        }

        private static eMainMenuAction getUserActionChoice()
        {
            string userInput;
            bool isChoiceValid = false;
            eMainMenuAction choice = default;

            Console.WriteLine(@"Main Menu:

1. Load vehicles from file
2. Add vehicle to garage
3. Show license plates
4. Change vehicle state
5. Inflate wheels to max
6. Fuel vehicle
7. Charge vehicle
8. Show vehicle info
9. Exit
");
            while (!isChoiceValid)
            {
                Console.Write("Please enter your choice: ");
                userInput = Console.ReadLine();
                try
                {
                    if (int.TryParse(userInput, out int choiceNum) && choiceNum >= 1 && choiceNum <= 9)
                    {
                        choice = (eMainMenuAction)choiceNum;
                        isChoiceValid = true;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid choice");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            }

            return choice;
        }
        
        public enum eMainMenuAction
        {
            LoadFromFile = 1,
            AddCar = 2,
            ShowLicensePlates = 3,
            ChangeVehicleState = 4,
            InflateWheelsToMax = 5,
            FuelVehicle = 6,
            ChargeVehicle = 7,
            ShowVehicleInfo = 8,
            Exit = 9
        }
    }
}
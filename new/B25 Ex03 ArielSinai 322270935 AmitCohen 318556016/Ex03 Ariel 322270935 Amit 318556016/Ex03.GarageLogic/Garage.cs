using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, EntryForm> r_GarageVehicles;

        public Garage()
        {
            r_GarageVehicles = new Dictionary<string, EntryForm>();
        }

        public Dictionary<string, EntryForm> GarageVehicles
        {
            get { return r_GarageVehicles; }
        }

        public bool IsVehicleInGarage(string i_LicensePlateNumber)
        {
            return r_GarageVehicles.ContainsKey(i_LicensePlateNumber);
        }

        public void AddVehicle(string i_LicensePlateNumber, EntryForm i_Form)
        {
            r_GarageVehicles[i_LicensePlateNumber] = i_Form;
        }

        public void UpdateState(string i_LicensePlateNumber, int i_State)
        {
            r_GarageVehicles[i_LicensePlateNumber].VehicleState = (EntryForm.eVehicleState)i_State;
        }

        public List<string> GetLicensePlates(EntryForm.eVehicleState? i_StatusFilter = null)
        {
            List<string> licensePlates = new List<string>();

            foreach (var entry in r_GarageVehicles)
            {
                if (i_StatusFilter == null || entry.Value.VehicleState == i_StatusFilter)
                {
                    licensePlates.Add(entry.Key);
                }
            }

            return licensePlates;
        }

        public EntryForm GetVehicle(string i_LicensePlateNumber)
        {
            if (!r_GarageVehicles.ContainsKey(i_LicensePlateNumber))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            return r_GarageVehicles[i_LicensePlateNumber];
        }

        public void InflateWheelsToMax(string i_LicensePlateNumber)
        {
            Vehicle vehicle = GetVehicle(i_LicensePlateNumber).Vehicle;
            foreach (Wheel wheel in vehicle.Wheels)
            {
                wheel.CurrentAirPressure = wheel.MaxAirPressure;
            }
        }

        public void RefuelVehicle(string i_LicensePlateNumber, FuelEngine.eFuelType i_FuelType, float i_LitersToAdd)
        {
            Vehicle vehicle = GetVehicle(i_LicensePlateNumber).Vehicle;
            FuelEngine fuelEngine = vehicle.Engine as FuelEngine;

            if (fuelEngine == null)
            {
                throw new ArgumentException("Vehicle is not fuel-powered");
            }

            fuelEngine.Refuel(i_LitersToAdd, i_FuelType);
            vehicle.UpdatePercentOfEnergyLeft();
        }

        public void ChargeVehicle(string i_LicensePlateNumber, float i_MinutesToCharge)
        {
            Vehicle vehicle = GetVehicle(i_LicensePlateNumber).Vehicle;
            ElectricEngine electricEngine = vehicle.Engine as ElectricEngine;

            if (electricEngine == null)
            {
                throw new ArgumentException("Vehicle is not electric-powered");
            }

            electricEngine.ChargeBattery(i_MinutesToCharge);
            vehicle.UpdatePercentOfEnergyLeft();
        }
    }
}
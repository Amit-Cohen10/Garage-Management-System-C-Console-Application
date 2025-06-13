using System;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;
        
        public FuelEngine(eFuelType i_FuelType, float i_MaxEnergyAmount) : base(i_MaxEnergyAmount)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public void Refuel(float i_FuelLittersToAdd, eFuelType i_FuelType)
        {
            
            if (this.r_FuelType != i_FuelType)
            {
                throw new ArgumentException("The fuel type you used is different than your vehicle's.");
            }

            if (this.m_EnergyAmountLeft + i_FuelLittersToAdd > this.r_MaxEnergyAmount)
            {    
                throw new ValueOutOfRangeException(0, this.r_MaxEnergyAmount - this.m_EnergyAmountLeft);
            }

            this.m_EnergyAmountLeft += i_FuelLittersToAdd;
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Fuel Type: {1}", base.ToString(), r_FuelType);
        }

        public enum eFuelType
        {
            Soler = 1,
            Octan95 = 2,
            Octan96 = 3,
            Octan98 = 4
        }
    }
}
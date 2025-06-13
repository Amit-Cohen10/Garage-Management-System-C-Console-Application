namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergyAmount) : base(i_MaxEnergyAmount)
        {
        }

        public void ChargeBattery(float i_MinutesAmountToAdd)
        {
            float hoursToAdd = i_MinutesAmountToAdd / 60;

            if (this.m_EnergyAmountLeft + hoursToAdd <= this.r_MaxEnergyAmount)
            {
                this.m_EnergyAmountLeft += hoursToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, this.r_MaxEnergyAmount * 60 - this.m_EnergyAmountLeft * 60);
            }
        }

        public override string ToString()
        {
            return string.Format(@"{0}
Engine Type: Electric", base.ToString());
        }
    }
}

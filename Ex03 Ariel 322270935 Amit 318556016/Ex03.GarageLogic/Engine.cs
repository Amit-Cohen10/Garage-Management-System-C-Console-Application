namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected readonly float r_MaxEnergyAmount;
        protected float m_EnergyAmountLeft;

        public Engine(float i_MaxEnergyAmount)
        {
            r_MaxEnergyAmount = i_MaxEnergyAmount;
            m_EnergyAmountLeft = 0;
        }

        public float MaxEnergyAmount
        {
            get { return r_MaxEnergyAmount; }
        }

        public float EnergyLeft
        {
            get { return m_EnergyAmountLeft; }
            set
            {
                if (value > r_MaxEnergyAmount)
                {
                    throw new ValueOutOfRangeException(0, r_MaxEnergyAmount, "Energy Amount Left");
                }

                m_EnergyAmountLeft = value;
            }
        }

        public override string ToString()
        {
            return string.Format(@"Max Energy Amount: {0:F1}
Energy Amount Left: {1:F1}", r_MaxEnergyAmount, m_EnergyAmountLeft);
        }
    }
}
namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            set { m_ManufacturerName = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            set
            {
                if (value > r_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure, "Air pressure");
                }

                m_CurrentAirPressure = value;
            }
            get { return m_CurrentAirPressure; }
        }

        public override string ToString()
        {
            return string.Format(@"Manufacturer: {0}
Current Air Pressure: {1:F1}
Max Air Pressure: {2:F1}", m_ManufacturerName, m_CurrentAirPressure, r_MaxAirPressure);
        }
    }
}
using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private float  m_PercentOfEnergyLeft;
        private string m_LicensePlate;
        private Wheel[] m_Wheels;
        private Engine  m_Engine;

        protected Vehicle(string i_LicensePlateNumber,
                          Engine i_Engine,
                          Wheel[] i_Wheels)
        {
            m_LicensePlate = i_LicensePlateNumber;
            m_Wheels       = i_Wheels;
            m_Engine       = i_Engine;
        }

        public string LicensePlate => m_LicensePlate;

        public string ModelName
        {
            get => m_ModelName;
            set => m_ModelName = value;
        }

        public Wheel[] Wheels   => m_Wheels;
        public Engine  Engine   => m_Engine;
        public float   PercentOfEnergyLeft => m_PercentOfEnergyLeft;

        public void UpdatePercentOfEnergyLeft()
        {
            m_PercentOfEnergyLeft =
                (m_Engine.EnergyLeft / m_Engine.MaxEnergyAmount) * 100;
        }

        //add new data for your new vehicle here
        public virtual void LoadExtraData(string[] i_parts, int i_startIdx) { }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.AppendLine(string.Format(
                @"License plate number: {0}
Model name: {1}
Percentage of energy left: {2:F1}%",
                m_LicensePlate, m_ModelName, m_PercentOfEnergyLeft));

            vehicleInfo.AppendLine(@"Wheels:");
            for (int i = 0; i < m_Wheels.Length; i++)
            {
                vehicleInfo.AppendLine($"Wheel {i + 1}:");
                vehicleInfo.AppendLine(m_Wheels[i].ToString());
            }

            vehicleInfo.AppendLine(m_Engine.ToString());

            return vehicleInfo.ToString();
        }
    }
}
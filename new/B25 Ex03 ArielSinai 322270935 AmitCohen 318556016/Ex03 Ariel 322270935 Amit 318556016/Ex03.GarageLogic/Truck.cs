using System.Text;
using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarryingDangerousMaterials;
        private float m_CargoVolume;

        public Truck(string i_LicenseID, string i_ModelName)
    : base(i_LicenseID,
          new FuelEngine(FuelEngine.eFuelType.Soler, 135f),
          createWheels())
        {
            ModelName = i_ModelName;
        }

        private static Wheel[] createWheels()
        {
            Wheel[] wheels = new Wheel[12];
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i] = new Wheel(27f);
            }
            return wheels;
        }

        public bool IsCarryingDangerousMaterials
        {
            get { return m_IsCarryingDangerousMaterials; }
            set { m_IsCarryingDangerousMaterials = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public override string ToString()
        {
            StringBuilder truckInfo = new StringBuilder(base.ToString());

            truckInfo.AppendLine(string.Format(@"Is Carrying Dangerous Materials: {0}
Cargo Volume: {1:F1}", m_IsCarryingDangerousMaterials ? "Yes" : "No", m_CargoVolume));

            return truckInfo.ToString();
        }
        
        public override void LoadExtraData(string[] i_parts, int i_startIdx)
        {
            if (i_parts.Length > i_startIdx)
            {
                IsCarryingDangerousMaterials = bool.Parse(i_parts[i_startIdx]);
            }

            if (i_parts.Length > i_startIdx + 1)
            {
                CargoVolume = float.Parse(i_parts[i_startIdx + 1]);
            }
        }
    }
}
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarryingDangerousMaterials;  // ✅ תוקן: שם נכון
        private float m_CargoVolume;

        public Truck(string i_LicenseID, string i_ModelName)
    :     base(i_LicenseID,
          new FuelEngine(FuelEngine.eFuelType.Soler, 135f),  // ✅ ערכים נכונים
          createWheels())
        {
            ModelName = i_ModelName;
        }

        private static Wheel[] createWheels()
        {
            Wheel[] wheels = new Wheel[12];  // ✅ 12 גלגלים
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i] = new Wheel(27f);  // ✅ לחץ 27
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
    }
}
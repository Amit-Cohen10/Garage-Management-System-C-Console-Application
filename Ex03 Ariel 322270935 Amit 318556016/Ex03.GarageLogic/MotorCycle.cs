using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class MotorCycle : Vehicle
    {
        private int m_EngineVolumeCC;
        private eLicenseType m_LicenseType;

        public MotorCycle(string i_LicensePlateNumber, Engine i_Engine, Wheel[] i_Wheels) : base(i_LicensePlateNumber, i_Engine, i_Wheels)
        {
        }

        public int EngineVolumeCC
        {
            get { return m_EngineVolumeCC; }
            set { m_EngineVolumeCC = value; }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public override string ToString()
        {
            StringBuilder motorCycleInfo = new StringBuilder(base.ToString());

            motorCycleInfo.AppendLine(string.Format(@"Engine Volume (CC): {0}
License Type: {1}", m_EngineVolumeCC, m_LicenseType));

            return motorCycleInfo.ToString();
        }

        public enum eLicenseType
        {
            A = 1,
            A2 = 2,
            AB = 3,
            B2 = 4
        }
    }
}
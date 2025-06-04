using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle 
    {
        protected eCarColor m_CarColor;
        protected eDoorsAmount m_DoorsAmount;

        public Car(string i_LicensePlateNumber, Engine i_Engine, Wheel[] i_Wheels) : base(i_LicensePlateNumber, i_Engine, i_Wheels)
        {
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eDoorsAmount DoorsAmount
        {
            get { return m_DoorsAmount; }
            set { m_DoorsAmount = value; }
        }

        public override string ToString()
        {
            StringBuilder carInfo = new StringBuilder(base.ToString());

            carInfo.AppendLine(string.Format(@"Car Color: {0}
Doors amount: {1}", m_CarColor, m_DoorsAmount));

            return carInfo.ToString();
        }

        // ✅ תוקן: צבעים נכונים לפי דרישות
        public enum eCarColor
        {
            Yellow = 1,
            Black = 2,
            White = 3,
            Silver = 4
        }

        public enum eDoorsAmount
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }
    }
}
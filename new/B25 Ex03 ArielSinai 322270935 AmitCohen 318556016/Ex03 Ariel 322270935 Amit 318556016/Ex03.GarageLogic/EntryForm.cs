using System.Text;

namespace Ex03.GarageLogic
{
    public class EntryForm
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerCellNumber;
        private eVehicleState m_VehicleState;
        private readonly Vehicle r_Vehicle;

        public EntryForm(string i_OwnerName, string i_OwnerCellNumber, Vehicle i_Vehicle)
        {
            m_VehicleState = eVehicleState.UnderRepair;
            r_OwnerName = i_OwnerName;
            r_OwnerCellNumber = i_OwnerCellNumber;
            r_Vehicle = i_Vehicle;
        }

        public eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public string OwnerName
        {
            get { return r_OwnerName; }
        }

        public string OwnerPhone
        {
            get { return r_OwnerCellNumber; }
        }

        public override string ToString()
        {
            StringBuilder entryFormInfo = new StringBuilder();

            entryFormInfo.AppendLine(string.Format(@"Owner's name: {0}
Owner's phone number: {1}
Vehicle state: {2}", r_OwnerName, r_OwnerCellNumber, m_VehicleState));
            entryFormInfo.AppendLine(r_Vehicle.ToString());

            return entryFormInfo.ToString();
        }

        public enum eVehicleState
        {
            UnderRepair = 1,
            Fixed = 2,
            Paid = 3
        }
    }
}
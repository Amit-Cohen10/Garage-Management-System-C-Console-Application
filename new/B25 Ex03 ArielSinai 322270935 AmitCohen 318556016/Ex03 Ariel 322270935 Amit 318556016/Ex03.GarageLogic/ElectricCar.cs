namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID,
                  new ElectricEngine(4.8f),  
                  createWheels())
        {
            ModelName = i_ModelName;
        }

        private static Wheel[] createWheels()
        {
            Wheel[] wheels = new Wheel[5];  
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i] = new Wheel(32f);  
            }
            return wheels;
        }
    }
}
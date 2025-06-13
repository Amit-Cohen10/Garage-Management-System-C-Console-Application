namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : MotorCycle
    {
        public FuelMotorcycle(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID,
                  new FuelEngine(FuelEngine.eFuelType.Octan98, 5.8f),  
                  createWheels())
        {
            ModelName = i_ModelName;
        }

        private static Wheel[] createWheels()
        {
            Wheel[] wheels = new Wheel[2];  
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i] = new Wheel(30f);  
            }
            return wheels;
        }
    }
}
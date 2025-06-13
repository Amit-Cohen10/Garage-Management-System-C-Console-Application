namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        public FuelCar(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID,
                  new FuelEngine(FuelEngine.eFuelType.Octan95, 48f),
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
using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleCreation
    {
        public VehicleCreation()
        {
        }

        public enum eVehicleTypes
        {
            Fuel_Motorcycle = 1,
            Electric_Motorcycle = 2,
            Fuel_Car = 3,
            Electric_Car = 4,
            Truck = 5
        }

        public static string GetStringVehicleTypes()
        {
            int index = 1;
            StringBuilder vehicleTypesString = new StringBuilder();
            foreach (eVehicleTypes vehicleType in Enum.GetValues(typeof(eVehicleTypes)))
            {
                string currentVehicleTypeString = vehicleType.ToString();
                vehicleTypesString.Append(index + ". " + currentVehicleTypeString);
                vehicleTypesString.AppendLine();
                index++;
            }

            return vehicleTypesString.ToString();
        }

        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleTypes.Fuel_Motorcycle:
                    newVehicle = CreateNewFuelMotorcycle();
                    break;
                case eVehicleTypes.Electric_Motorcycle:
                    newVehicle = CreateNewElectricMotorcycle();
                    break;
                case eVehicleTypes.Fuel_Car:
                    newVehicle = CreateNewFuelCar();
                    break;
                case eVehicleTypes.Electric_Car:
                    newVehicle = CreateNewElectricCar();
                    break;
                case eVehicleTypes.Truck:
                    newVehicle = CreateNewTruck();
                    break;
            }

            return newVehicle;
        }

        private static Vehicle CreateNewFuelMotorcycle()
        {
            return new MotorCycle(Engine.eEngineType.Fuel);
        }

        private static Vehicle CreateNewElectricMotorcycle()
        {
            return new MotorCycle(Engine.eEngineType.Electric);
        }

        private static Vehicle CreateNewFuelCar()
        {
            return new Car(Engine.eEngineType.Fuel);
        }

        private static Vehicle CreateNewElectricCar()
        {
            return new Car(Engine.eEngineType.Electric);
        }

        private static Vehicle CreateNewTruck()
        {
            return new Truck();
        }
    }
}

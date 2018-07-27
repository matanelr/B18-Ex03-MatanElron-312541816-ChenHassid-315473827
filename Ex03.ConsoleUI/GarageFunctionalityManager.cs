using System.Text;
using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.VehicleCreation;

namespace Ex03.ConsoleUI
{
    internal class GarageFunctionalityManager
    {
        private static IDictionary<string, VehicleInGarage> m_ListVehiclesInGarage;

        public static IDictionary<string, VehicleInGarage> ListVehiclesInGarage
        {
            get
            {
                return m_ListVehiclesInGarage;
            }

            set
            {
                m_ListVehiclesInGarage = value;
            }
        }

        internal static void AddVehicleToTheGarage(eVehicleTypes i_VeicleToTheGarage)
        {
            bool isValid = false;
            string ownerName;
            string ownerPhoneNumber;

            GarageUI.GetVehicleOwnerDetails(out ownerName, out ownerPhoneNumber);
            Vehicle currentVehicleToAdd = VehicleCreation.CreateVehicle(i_VeicleToTheGarage);
            Dictionary<string, string> vehicleDetails = currentVehicleToAdd.CreateVehicleDetailsDict();
            vehicleDetails = currentVehicleToAdd.EngineOfVehicle.AddEngineDetailsToDict(vehicleDetails);

            while (!isValid)
            {
                vehicleDetails = GarageUI.GetVehicleDetailsFromUser(vehicleDetails, currentVehicleToAdd);

                try
                {
                    currentVehicleToAdd.AddVehicleInformation(vehicleDetails);
                    currentVehicleToAdd.EngineOfVehicle.AddEngineInformation(vehicleDetails);
                    isValid = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            VehicleInGarage newVehicleIntheGarage = new VehicleInGarage(currentVehicleToAdd, ownerName, ownerPhoneNumber);
            ListVehiclesInGarage.Add(currentVehicleToAdd.LicenseNumber, newVehicleIntheGarage);
        }

        internal static void DisplayVehicleInformation(string i_LicenseNumber)
        {
            StringBuilder stringBuilder = new StringBuilder();
            VehicleInGarage vehicleToDisplay = ListVehiclesInGarage[i_LicenseNumber];
            List<string> listOfVehicleDetails = vehicleToDisplay.GetAllVehicleDetails();

            stringBuilder.AppendLine(string.Format("The details of the vehicle with the license number: {0}{1}", i_LicenseNumber, Environment.NewLine));

            foreach (string strToDisplay in listOfVehicleDetails)
            {
                stringBuilder.AppendLine(strToDisplay);
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        internal static void inflateTiresToMaximum(string i_LicenseNumber)
        {
            foreach (Wheel currentWheel in ListVehiclesInGarage[i_LicenseNumber].Vehicle.ListOfWheels)
            {
                try
                {
                    currentWheel.AddAirPressure(currentWheel.MaxPressure - currentWheel.CurrentPressure);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        internal static List<string> ShowAllVehiclesInGarage()
        {
            List<string> allVehiclesLicenseNumbers = new List<string>();

            foreach (VehicleInGarage currentVehicle in m_ListVehiclesInGarage.Values)
            {
                allVehiclesLicenseNumbers.Add(currentVehicle.Vehicle.LicenseNumber);
            }

            return allVehiclesLicenseNumbers;
        }

        internal static List<string> ShowVehiclesByStatus(VehicleInGarage.eVehicleStatus i_VehicleStatus)
        {
            List<string> vehiclesLicenseNumbers = new List<string>();

            foreach (VehicleInGarage currentVehicle in m_ListVehiclesInGarage.Values)
            {
                if (i_VehicleStatus == currentVehicle.VehicleStatus)
                {
                    vehiclesLicenseNumbers.Add(currentVehicle.Vehicle.LicenseNumber);
                }
            }

            return vehiclesLicenseNumbers;
        }

        internal static void ChangeCertainVehicleStatus(string i_VehicleLicenseNumber, VehicleInGarage.eVehicleStatus i_VehicleRequestStatus)
        {
            if (IsExistInGarage(i_VehicleLicenseNumber))
            {
                m_ListVehiclesInGarage[i_VehicleLicenseNumber].VehicleStatus = i_VehicleRequestStatus;
            }
            else
            {
                throw new ArgumentException(string.Format("The cehicle which has this license number: {0} is not in the garage.", i_VehicleLicenseNumber));
            }
        }

        internal static bool IsExistInGarage(string i_VehicleLicenseNumber)
        {
            return ListVehiclesInGarage.ContainsKey(i_VehicleLicenseNumber);
        }

        internal static void RefuelFuelVehicle(string i_VehicleLicenseNumber, FuelEngine.eFuelType i_TypeOfFuel, float i_AmountToFill)
        {
            VehicleInGarage currentVehicle = ListVehiclesInGarage[i_VehicleLicenseNumber];
            Vehicle vehicleToAddEnergy = currentVehicle.Vehicle;

            if (vehicleToAddEnergy.EngineOfVehicle.EngineType != Engine.eEngineType.Fuel)
            {
                throw new ArgumentException(string.Format("Invalid input. The type of engine of this vehicle is not Electric{0}", Environment.NewLine));
            }

            FuelEngine fuelEngine = (FuelEngine)vehicleToAddEnergy.EngineOfVehicle;

            try
            {
                fuelEngine.Refuel(i_AmountToFill, i_TypeOfFuel);
                vehicleToAddEnergy.EngineOfVehicle = fuelEngine;
            }
            catch (ArgumentException exception)
            {
                throw exception;
            }
            catch (OutOfRangeException exception)
            {
                throw exception;
            }
        }

        internal static void ChargeElectricVehicle(string i_VehicleLicenseNumber, float i_AmountToFill)
        {
            VehicleInGarage currentVehicle = ListVehiclesInGarage[i_VehicleLicenseNumber];
            Vehicle vehicleToAddEnergy = currentVehicle.Vehicle;

            if (vehicleToAddEnergy.EngineOfVehicle.EngineType != Engine.eEngineType.Electric)
            {
                throw new ArgumentException(string.Format("Invalid input. The type of engine of this vehicle is not Electric{0}", Environment.NewLine));
            }

            ElectricEngine electricEngine = (ElectricEngine)vehicleToAddEnergy.EngineOfVehicle;

            try
            {
                electricEngine.Charge(i_AmountToFill);
                vehicleToAddEnergy.EngineOfVehicle = electricEngine;
            }
            catch (OutOfRangeException exception)
            {
                throw exception;
            }
        }
    }
}

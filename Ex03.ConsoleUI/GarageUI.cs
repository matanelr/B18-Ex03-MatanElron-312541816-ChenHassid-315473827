using System.Collections.Generic;
using System;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.VehicleCreation;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        internal static void StartManageTheGarage()
        {
            short userInputNumOfOption;
            bool isContinueToManage = true;
            GarageFunctionalityManager.ListVehiclesInGarage = new Dictionary<string, VehicleInGarage>();

            while (isContinueToManage)
            {
                userInputNumOfOption = getvalidInputOption();

                try
                {
                    switch (userInputNumOfOption)
                    {
                        case 1:
                            {
                                addVehicleToGarage();
                                break;
                            }

                        case 2:
                            {
                                showLicenseNumbers();
                                break;
                            }

                        case 3:
                            {
                                changeCertainVehicleStatus();
                                break;
                            }

                        case 4:
                            {
                                inflateTiresToMaximum();
                                break;
                            }

                        case 5:
                            {
                                refuelFuelVehicle();
                                break;
                            }

                        case 6:
                            {
                                chargeElectricVehicle();
                                break;
                            }

                        case 7:
                            {
                                displayVehicleInformation();
                                break;
                            }
                    }

                    isContinueToManage = isProceedToManageGarage();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private static bool isProceedToManageGarage()
        {
            bool isContinueToManage = true;
            string userInput = string.Empty;

            Console.WriteLine("Would you like to continue?{0}1: Yes{0}2: No{0}", Environment.NewLine);
            userInput = Console.ReadLine();
            if (userInput.Equals("2"))
            {
                isContinueToManage = false;
                Console.WriteLine("Thank you. GoodBye");
            }

            return isContinueToManage;
        }

        private static void printOptionsForUser()
        {
            string stringOutput = string.Format(
            @"Please choose one of the options below:
1: Add a vehicle to the garage
2: Display a list of license numbers currently in the garage, with a filtering option based on status
3: Change a certain vehicle's status
4: Infalte wheels to maximum
5: Refuel a fuel-based vehicle
6: Charge an electric-based vehicle
7: Display vehicle information");

            Console.WriteLine(stringOutput);
        }

        private static short getvalidInputOption()
        {
            string userInputString;
            short userInputNumberOption = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                printOptionsForUser();
                userInputString = Console.ReadLine();
                isValidInput = short.TryParse(userInputString, out userInputNumberOption);
                if (!isValidInput || userInputNumberOption > 7 || userInputNumberOption < 1)
                {
                    Console.WriteLine("{0}Invalid input! please choose a number between 1 to 7{0}", Environment.NewLine);
                    isValidInput = false;
                }
            }

            return userInputNumberOption;
        }

        private static void addVehicleToGarage()
        {
            eVehicleTypes vehicleType = getVehicleTypeFromUser();

            GarageFunctionalityManager.AddVehicleToTheGarage(vehicleType);
        }

        private static eVehicleTypes getVehicleTypeFromUser()
        {
            string VehicleTypesOptions = getStringOfVehicleTypes();
            string userInput = string.Empty;
            short userInputAsNumber = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Please select a type of vehicle to add to the garage:");
                Console.WriteLine(VehicleTypesOptions);
                userInput = Console.ReadLine();

                try
                {
                    checkValidVehicleType(userInput, out userInputAsNumber);
                    isValidInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return (eVehicleTypes)userInputAsNumber;
        }

        private static void checkValidVehicleType(string i_UserInput, out short o_UserInputOptionNumber)
        {
            if (!short.TryParse(i_UserInput, out o_UserInputOptionNumber))
            {
                throw new FormatException("Invalid input. Please choose a number between 1 to 5");
            }

            if (o_UserInputOptionNumber < 1 || o_UserInputOptionNumber > 5)
            {
                throw new ArgumentException("Invalid input. Please enter a number between 1 to 5");
            }
        }

        private static string getStringOfVehicleTypes()
        {
            string stringOfVehicleTypes = VehicleCreation.GetStringVehicleTypes();

            return stringOfVehicleTypes;
        }

        internal static void GetVehicleOwnerDetails(out string o_VehicleOwnerName, out string o_VehicleOwnerPhone)
        {
            Console.WriteLine("Please enter the name of the vehicle owner");
            o_VehicleOwnerName = Console.ReadLine();
            Console.WriteLine("Please enter the phone of the vehicle owners");
            o_VehicleOwnerPhone = Console.ReadLine();

            if (string.IsNullOrEmpty(o_VehicleOwnerPhone) || string.IsNullOrEmpty(o_VehicleOwnerName))
            {
                Console.WriteLine("You must enter a value. Try again");
                GetVehicleOwnerDetails(out o_VehicleOwnerName, out o_VehicleOwnerPhone);
            }
        }

        internal static Dictionary<string, string> GetVehicleDetailsFromUser(IDictionary<string, string> io_VehicleDetail, Vehicle i_CurrentVehicle)
        {
            Dictionary<string, string> vehicleDetailFromUser = new Dictionary<string, string>();
            string userInput;

            foreach (string currentDetailToAdd in io_VehicleDetail.Keys)
            {
                Console.WriteLine("Please enter {0}", currentDetailToAdd);
                userInput = checkValidDetailsInput(currentDetailToAdd, i_CurrentVehicle);
                vehicleDetailFromUser.Add(currentDetailToAdd, userInput);
            }

            return vehicleDetailFromUser;
        }

        private static string checkValidDetailsInput(string i_CurrentDetailToAdd, Vehicle i_CurrentVehicle)
        {
            bool isValidInput = false;
            string userInput = string.Empty;

            while (!isValidInput)
            {
                userInput = Console.ReadLine();

                switch (i_CurrentDetailToAdd)
                {
                    case "License number":
                        if (GarageFunctionalityManager.ListVehiclesInGarage.Keys.Contains(userInput))
                        {
                            GarageFunctionalityManager.ListVehiclesInGarage[userInput].VehicleStatus = VehicleInGarage.eVehicleStatus.InRepair;
                            throw new ArgumentException("This vehicle is already in the garage, his status has changed to InRepair.");
                        }

                        isValidInput = true;
                        break;

                    case "Wheels current pressure":
                        try
                        {
                            i_CurrentVehicle.CheckValidWheelPressure(userInput);
                            isValidInput = true;
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }

                        break;

                    case "Current energy":
                        try
                        {
                            i_CurrentVehicle.EngineOfVehicle.CheckValidCurrentEnergy(userInput);
                            isValidInput = true;
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }

                        break;

                    default:
                        isValidInput = true;
                        break;
                }
            }

            return userInput;
        }

        private static void showLicenseNumbers()
        {
            try
            {
                checkValidityOfUserOptionChoice();
            }
            catch (UnavailableOptionException exception)
            {
                throw exception;
            }

            bool ValidInputFromUser = false;
            string userInputString = string.Empty;
            short userInputNumberOption = 0;
            List<string> relevantLicenseNumbers = new List<string>();
            VehicleInGarage.eVehicleStatus vehicleStatus;

            string stringOutput = string.Format(
                @"Please choose one of the options below:
1: Show all vehicles
2: Show all vehicles that are in repair
3: Show all vehicles that have been repaired 
4: Show all vehicles that were paid");

            while (!ValidInputFromUser)
            {
                Console.WriteLine("Please select which vehicles would you like to see?");
                Console.WriteLine(stringOutput);
                userInputString = Console.ReadLine();
                ValidInputFromUser = short.TryParse(userInputString, out userInputNumberOption);

                if (!ValidInputFromUser || userInputNumberOption > 4 || userInputNumberOption < 1)
                {
                    Console.WriteLine("Invalid Input! Please choose one of the valid options");
                    ValidInputFromUser = false;
                }
            }

            switch (userInputNumberOption)
            {
                case 1:
                    {
                        relevantLicenseNumbers = GarageFunctionalityManager.ShowAllVehiclesInGarage();
                        break;
                    }

                case 2:
                    {
                        vehicleStatus = VehicleInGarage.eVehicleStatus.InRepair;
                        relevantLicenseNumbers = GarageFunctionalityManager.ShowVehiclesByStatus(vehicleStatus);
                        break;
                    }

                case 3:
                    {
                        vehicleStatus = VehicleInGarage.eVehicleStatus.IsRepaired;
                        relevantLicenseNumbers = GarageFunctionalityManager.ShowVehiclesByStatus(vehicleStatus);
                        break;
                    }

                case 4:
                    {
                        vehicleStatus = VehicleInGarage.eVehicleStatus.IsPaid;
                        relevantLicenseNumbers = GarageFunctionalityManager.ShowVehiclesByStatus(vehicleStatus);
                        break;
                    }
            }

            if (relevantLicenseNumbers.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage with the chosen status");
            }
            else
            {
                Console.WriteLine("The vehicles' license number with the chosen status are:");
                foreach (string currentlicenseNumber in relevantLicenseNumbers)
                {
                    Console.WriteLine(currentlicenseNumber);
                }
            }
        }

        private static void changeCertainVehicleStatus()
        {
            try
            {
                checkValidityOfUserOptionChoice();
            }
            catch (UnavailableOptionException exception)
            {
                throw exception;
            }

            if (GarageFunctionalityManager.ListVehiclesInGarage.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage. Choose another option");
            }
            else
            {
                bool validInputFromUser = false;
                string userInputString = string.Empty;
                short userInputNumberOption = 0;
                VehicleInGarage.eVehicleStatus vehicleRequestStatus = GarageLogic.VehicleInGarage.eVehicleStatus.None;
                string stringOutput = string.Format(
                   @"Please choose one of the options below:
1: In Repair (in progress) 
2: Is Repaired (fixed)
3: Is Paid ");
                string vehicleLicenseNumber = getVehicleLicenseNumber();

                while (!validInputFromUser)
                {
                    Console.WriteLine("Please select the status that you whould like to change to : ");
                    Console.WriteLine(stringOutput);
                    userInputString = Console.ReadLine();
                    validInputFromUser = short.TryParse(userInputString, out userInputNumberOption);

                    if (!validInputFromUser || userInputNumberOption > 3 || userInputNumberOption < 1)
                    {
                        Console.WriteLine("Invalid input! Please enter a valid number from the options below.");
                        validInputFromUser = false;
                    }
                }

                switch (userInputNumberOption)
                {
                    case 1:
                        {
                            vehicleRequestStatus = VehicleInGarage.eVehicleStatus.InRepair;
                            break;
                        }

                    case 2:
                        {
                            vehicleRequestStatus = VehicleInGarage.eVehicleStatus.IsRepaired;
                            break;
                        }

                    case 3:
                        {
                            vehicleRequestStatus = VehicleInGarage.eVehicleStatus.IsPaid;
                            break;
                        }
                }

                try
                {
                    GarageFunctionalityManager.ChangeCertainVehicleStatus(vehicleLicenseNumber, vehicleRequestStatus);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private static string getVehicleLicenseNumber()
        {
            string vehicleLicenseNumber = string.Empty;
            bool isFoundInGarage = false;

            Console.WriteLine("Please enter vehicles' license number");
            vehicleLicenseNumber = Console.ReadLine();
            isFoundInGarage = GarageFunctionalityManager.IsExistInGarage(vehicleLicenseNumber);

            while (!isFoundInGarage)
            {
                Console.WriteLine("This vehicle doesn't exist in the garage. Please try another license number");
                vehicleLicenseNumber = Console.ReadLine();
                isFoundInGarage = GarageFunctionalityManager.IsExistInGarage(vehicleLicenseNumber);
            }

            return vehicleLicenseNumber;
        }

        private static FuelEngine.eFuelType getFuelTypeFromUser()
        {
            bool isValidFuelType = false;
            string fuelTypeStringInput;
            FuelEngine.eFuelType fuelTypeValue = FuelEngine.eFuelType.None;

            while (!isValidFuelType)
            {
                Console.WriteLine("Please enter vehicle's type of fuel");
                fuelTypeStringInput = Console.ReadLine();

                try
                {
                    fuelTypeValue = (FuelEngine.eFuelType)Enum.Parse(typeof(FuelEngine.eFuelType), fuelTypeStringInput, true);
                    isValidFuelType = true;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Throw on type ful");
                    Console.WriteLine(string.Format("{0} is not a type of fuel. Try again", fuelTypeStringInput));
                }
            }

            return fuelTypeValue;
        }

        private static float getAmountOfEnergyToAdd()
        {
            bool isValidValue = false;
            string amountOfEnergyString;
            float amountOfEnergyValue = 0;

            while (!isValidValue)
            {
                Console.WriteLine("Please enter amount of energy to add");
                amountOfEnergyString = Console.ReadLine();

                if (float.TryParse(amountOfEnergyString, out amountOfEnergyValue))
                {
                    isValidValue = true;
                }
                else
                {
                    throw new FormatException(string.Format("{0} is not a valid argument", amountOfEnergyString));
                }
            }

            return amountOfEnergyValue;
        }

        private static void displayVehicleInformation()
        {
            try
            {
                checkValidityOfUserOptionChoice();
            }
            catch (UnavailableOptionException exception)
            {
                throw exception;
            }

            bool isValidLicenseNumber = false;
            string userInputLicenseNumber = string.Empty;

            while (!isValidLicenseNumber)
            {
                Console.WriteLine("Please enter a license number you would like to display it's information");
                userInputLicenseNumber = Console.ReadLine();

                if (!GarageFunctionalityManager.IsExistInGarage(userInputLicenseNumber))
                {
                    Console.WriteLine("The vehicle with the license number you entered is not in the garage.");
                }
                else
                {
                    isValidLicenseNumber = true;
                    GarageFunctionalityManager.DisplayVehicleInformation(userInputLicenseNumber);
                }
            }
        }

        private static void inflateTiresToMaximum()
        {
            try
            {
                checkValidityOfUserOptionChoice();
            }
            catch (UnavailableOptionException exception)
            {
                throw exception;
            }

            string licenseNumber = getVehicleLicenseNumber();

            try
            {
                GarageFunctionalityManager.inflateTiresToMaximum(licenseNumber);
                Console.WriteLine("All the wheels of the vehicle have been filled.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void refuelFuelVehicle()
        {
            try
            {
                checkValidityOfUserOptionChoice();
            }
            catch (UnavailableOptionException exception)
            {
                throw exception;
            }

            try
            {
                string licenseNumber = getVehicleLicenseNumber();
                FuelEngine.eFuelType fuelType = getFuelTypeFromUser();
                float amountFuelToFill = getAmountOfEnergyToAdd();
                GarageFunctionalityManager.RefuelFuelVehicle(licenseNumber, fuelType, amountFuelToFill);
                Console.WriteLine("Refule is done.");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void chargeElectricVehicle()
        {
            try
            {
                checkValidityOfUserOptionChoice();
            }
            catch (UnavailableOptionException exception)
            {
                throw exception;
            }

            try
            {
                string licenseNumber = getVehicleLicenseNumber();
                float amountFuelToFill = getAmountOfEnergyToAdd();
                GarageFunctionalityManager.ChargeElectricVehicle(licenseNumber, amountFuelToFill);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void checkValidityOfUserOptionChoice()
        {
            if (GarageFunctionalityManager.ListVehiclesInGarage.Count == 0)
            {
                throw new UnavailableOptionException(string.Format("The garage is empty.{0}", Environment.NewLine));
            }
        }
    }
}

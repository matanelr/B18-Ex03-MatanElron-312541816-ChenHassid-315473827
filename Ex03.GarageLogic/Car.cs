using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const float k_MaxWheelPressure = 32;
        private const int k_NumberOfWheels = 4;
        private const FuelEngine.eFuelType k_CarFuelType = FuelEngine.eFuelType.Octan98;
        private eColor m_Color;
        private eNumbersOfDoors m_NumberOfDoors;

        [Flags]
        public enum eColor
        {
            Grey = 0,
            Blue = 1,
            White = 2,
            Black = 4,
        }

        public enum eNumbersOfDoors
        {
            Two,
            Three,
            Four,
            Five,
        }

        public Car(Engine.eEngineType i_EngineType)
          : base()
        {
            switch (i_EngineType)
            {
                case Engine.eEngineType.Fuel:
                    this.EngineOfVehicle = new FuelEngine(k_CarFuelType);
                    this.EngineOfVehicle.MaxEnergy = 45;
                    break;

                case Engine.eEngineType.Electric:
                    this.EngineOfVehicle = new ElectricEngine();
                    this.EngineOfVehicle.MaxEnergy = 3.2f;
                    break;
            }

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                this.ListOfWheels.Add(new Wheel(k_MaxWheelPressure));
            }
        }

        public eColor Color
        {
            get
            {
                return this.m_Color;
            }

            set
            {
                this.m_Color = value;
            }
        }

        public eNumbersOfDoors NumberOfDoors
        {
            get
            {
                return this.m_NumberOfDoors;
            }

            set
            {
                this.m_NumberOfDoors = value;
            }
        }

        public FuelEngine.eFuelType GetFuelType
        {
            get { return k_CarFuelType; }
        }

        public override Dictionary<string, string> CreateVehicleDetailsDict()
        {
            Dictionary<string, string> vehicleDedatilsDic = new Dictionary<string, string>();

            vehicleDedatilsDic = base.CreateVehicleDetailsDict();
            vehicleDedatilsDic.Add("Color of the car (Grey/Blue/White/Black)", null);
            vehicleDedatilsDic.Add("Number of doors (2/3/4/5)", null);

            return vehicleDedatilsDic;
        }

        public override void AddVehicleInformation(Dictionary<string, string> i_VehicleDetailsDic)
        {
            string colorString;
            eColor colorValue;
            string numOfDoorsString;

            base.AddVehicleInformation(i_VehicleDetailsDic);

            if (!i_VehicleDetailsDic.TryGetValue("Color of the car (Grey/Blue/White/Black)", out colorString))
            {
                throw new KeyNotFoundException("Color");
            }

            if (!i_VehicleDetailsDic.TryGetValue("Number of doors (2/3/4/5)", out numOfDoorsString))
            {
                throw new KeyNotFoundException("Number of doors");
            }

            try
            {
                colorValue = (eColor)Enum.Parse(typeof(eColor), colorString, true);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(string.Format("'{0}' is not a valid color.", colorString));
            }

            if (!Enum.IsDefined(typeof(eColor), colorString))
            {
                throw new ArgumentException(string.Format("'{0}' is not a valid color.", colorString));
            }

            switch (numOfDoorsString)
            {
                case "2":
                    NumberOfDoors = eNumbersOfDoors.Two;
                    break;

                case "3":
                    NumberOfDoors = eNumbersOfDoors.Three;
                    break;

                case "4":
                    NumberOfDoors = eNumbersOfDoors.Four;
                    break;

                case "5":
                    NumberOfDoors = eNumbersOfDoors.Five;
                    break;

                default:
                    throw new ArgumentException(string.Format("'{0}' is not a possible choice for the number of doors", numOfDoorsString));
            }

            Color = colorValue;
        }

        public override List<string> GetVehicleDetails(List<string> i_VehicleDetalisDic)
        {
            i_VehicleDetalisDic = base.GetVehicleDetails(i_VehicleDetalisDic);
            i_VehicleDetalisDic.Add(string.Format("The color of the car: {0}", m_Color.ToString()));
            i_VehicleDetalisDic.Add(string.Format("The number of doors: {0}", m_NumberOfDoors.ToString()));

            return i_VehicleDetalisDic;
        }
    }
}

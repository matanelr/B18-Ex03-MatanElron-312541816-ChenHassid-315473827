using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class MotorCycle : Vehicle
    {
        private const float k_MaxWheelPressure = 30;
        private const int k_NumberOfWheels = 2;
        private const FuelEngine.eFuelType k_MotorcycleFuelType = FuelEngine.eFuelType.Octan96;
        private eTypeOfLicense m_TypeOfLicense;

        public enum eTypeOfLicense
        {
            A,
            A1,
            B1,
            B2,
        }

        public MotorCycle(Engine.eEngineType i_EngineType)
            : base()
        {
            switch (i_EngineType)
            {
                case Engine.eEngineType.Fuel:
                    this.EngineOfVehicle = new FuelEngine(FuelEngine.eFuelType.Octan96);
                    this.EngineOfVehicle.MaxEnergy = 6;
                    break;

                case Engine.eEngineType.Electric:
                    this.EngineOfVehicle = new ElectricEngine();
                    this.EngineOfVehicle.MaxEnergy = 1.8f;
                    break;
            }

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                this.ListOfWheels.Add(new Wheel(k_MaxWheelPressure));
            }
        }

        public eTypeOfLicense TypeOfLicense
        {
            get
            {
                return m_TypeOfLicense;
            }

            set
            {
                m_TypeOfLicense = value;
            }
        }

        public override void AddVehicleInformation(Dictionary<string, string> i_VehicleDetailsDic)
        {
            string typeOfLicenseString;
            eTypeOfLicense typeOfLicenseValue;

            base.AddVehicleInformation(i_VehicleDetailsDic);

            if (!i_VehicleDetailsDic.TryGetValue("The type of the license (A/A1/B1/B2)", out typeOfLicenseString))
            {
                throw new KeyNotFoundException("Please enter the type of license");
            }

            try
            {
                typeOfLicenseValue = (eTypeOfLicense)Enum.Parse(typeof(eTypeOfLicense), typeOfLicenseString, true);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(string.Format("'{0}' is not a member of the type of licenses. please choose one of the options: A/A1/B1/B2 ", typeOfLicenseString));
            }

            if (!Enum.IsDefined(typeof(eTypeOfLicense), typeOfLicenseString))
            {
                throw new ArgumentException(string.Format("'{0}' is not a member of the type of licenses. please choose one of the options: A/A1/B1/B2 ", typeOfLicenseString));
            }

            TypeOfLicense = typeOfLicenseValue;
        }

        public override Dictionary<string, string> CreateVehicleDetailsDict()
        {
            Dictionary<string, string> vehicleDedatilsDic = new Dictionary<string, string>();

            vehicleDedatilsDic = base.CreateVehicleDetailsDict();

            vehicleDedatilsDic.Add("The type of the license (A/A1/B1/B2)", null);

            return vehicleDedatilsDic;
        }

        public override List<string> GetVehicleDetails(List<string> i_VehicleDetalisDic)
        {
            i_VehicleDetalisDic = base.GetVehicleDetails(i_VehicleDetalisDic);
            i_VehicleDetalisDic.Add(string.Format("Motorcycle license type: {0}", m_TypeOfLicense.ToString()));

            return i_VehicleDetalisDic;
        }
    }
}

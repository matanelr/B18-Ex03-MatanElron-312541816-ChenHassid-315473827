using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_PercentOfEnergyLeft;
        private List<Wheel> m_Wheels;
        private Engine m_Engine;

        internal Vehicle()
        {
            m_ModelName = string.Empty;
            m_LicenseNumber = string.Empty;
            m_Engine = null;
            m_Wheels = new List<Wheel>();
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public List<Wheel> ListOfWheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }

        public Engine EngineOfVehicle
        {
            get
            {
                return m_Engine;
            }

            set
            {
                m_Engine = value;
            }
        }

        public float GetPrecentOfEnergyLeft
        {
            get
            {
                return m_PercentOfEnergyLeft = (float)(EngineOfVehicle.CurrentEnergy / EngineOfVehicle.MaxEnergy) * 100;
            }
        }

        public virtual Dictionary<string, string> CreateVehicleDetailsDict()
        {
            Dictionary<string, string> vehicleDetails = new Dictionary<string, string>();
            vehicleDetails.Add("License number", null);
            vehicleDetails.Add("Model name", null);
            vehicleDetails.Add("Wheels manufacturer name", null);
            vehicleDetails.Add("Wheels current pressure", null);

            return vehicleDetails;
        }

        public virtual void AddVehicleInformation(Dictionary<string, string> i_VehicleDetailsDic)
        {
            string modelName;
            string licenseNumber;
            string wheelManufactureName;
            string currentPressureString;
            float currentPressure;

            if (!i_VehicleDetailsDic.TryGetValue("Model name", out modelName) || string.IsNullOrEmpty(modelName))
            {
                throw new KeyNotFoundException("Invalid Model name. Please try again");
            }

            if (!i_VehicleDetailsDic.TryGetValue("License number", out licenseNumber) || string.IsNullOrEmpty(licenseNumber))
            {
                throw new KeyNotFoundException("Invalid license number. Please try again");
            }

            if (!i_VehicleDetailsDic.TryGetValue("Wheels manufacturer name", out wheelManufactureName) || string.IsNullOrEmpty(wheelManufactureName))
            {
                throw new KeyNotFoundException("Invalid wheel's manufacture name. Please try again");
            }

            if (!i_VehicleDetailsDic.TryGetValue("Wheels current pressure", out currentPressureString) || string.IsNullOrEmpty(currentPressureString))
            {
                throw new KeyNotFoundException("Invalid wheel's current pressure. Please try again");
            }

            if (!float.TryParse(currentPressureString, out currentPressure))
            {
                throw new KeyNotFoundException("Invalid wheel's current pressure. Please try again");
            }

            ModelName = modelName;
            LicenseNumber = licenseNumber;

            foreach (Wheel currentWheel in ListOfWheels)
            {
                currentWheel.CurrentPressure = currentPressure;
                currentWheel.ManufacturerName = wheelManufactureName;
            }
        }

        public virtual List<string> GetVehicleDetails(List<string> i_VehicleDetalisDic)
        {
            i_VehicleDetalisDic.Add(string.Format("Model: {0}", ModelName));
            i_VehicleDetalisDic.Add(string.Format("License Number: {0}", LicenseNumber));
            i_VehicleDetalisDic.Add(string.Format("Remaining precent of energy: {0}%", GetPrecentOfEnergyLeft));
            i_VehicleDetalisDic.Add(string.Format("Wheel Manufacturer: {0}", ListOfWheels[0].ManufacturerName));
            i_VehicleDetalisDic.Add(string.Format("Wheel Current Pressure: {0}", ListOfWheels[0].CurrentPressure));
            i_VehicleDetalisDic.Add(string.Format("Wheel Max Pressure: {0}", ListOfWheels[0].MaxPressure));

            return i_VehicleDetalisDic;
        }

        public virtual void CheckValidWheelPressure(string i_WheelPressureString)
        {
            float WheelPressureValue;

            if (!float.TryParse(i_WheelPressureString, out WheelPressureValue))
            {
                throw new FormatException(string.Format("{0} is invalid input format. Please enter a float number", i_WheelPressureString));
            }

            if (WheelPressureValue > ListOfWheels[0].MaxPressure)
            {
                throw new ArgumentException(string.Format("Invalid wheels pressure. please enter a number between 0 to {0}", ListOfWheels[0].MaxPressure));
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_MaxWheelPressure = 28;
        private const int k_NumberOfWheels = 4;
        private const FuelEngine.eFuelType k_TruckFuelType = FuelEngine.eFuelType.Octan96;
        private bool v_IsCarryingHazardousMaterials;
        private float m_MaxCarryingWeight;

        public Truck()
            : base()
        {
            IsCarryingHazardousMaterials = false;
            m_MaxCarryingWeight = 0;
            this.EngineOfVehicle = new FuelEngine(k_TruckFuelType);
            this.EngineOfVehicle.MaxEnergy = 115;

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                this.ListOfWheels.Add(new Wheel(k_MaxWheelPressure));
            }
        }

        public bool IsCarryingHazardousMaterials
        {
            get
            {
                return this.v_IsCarryingHazardousMaterials;
            }

            set
            {
                v_IsCarryingHazardousMaterials = value;
            }
        }

        public float MaxCarryingWeight
        {
            get
            {
                return this.m_MaxCarryingWeight;
            }

            set
            {
                m_MaxCarryingWeight = value;
            }
        }

        public override void AddVehicleInformation(Dictionary<string, string> i_VehicleDetailsDic)
        {
            string isCarryingMaterialsString;
            bool isCarryingMaterialValue;
            string maxCarryWeightString;
            float maxCarryWeightValue;

            base.AddVehicleInformation(i_VehicleDetailsDic);

            if (!i_VehicleDetailsDic.TryGetValue("The track is carrying hazardous materials (True/False)", out isCarryingMaterialsString) || string.IsNullOrEmpty(isCarryingMaterialsString))
            {
                throw new KeyNotFoundException("Please enter of the track is carrying hazardous materials");
            }

            if (!bool.TryParse(isCarryingMaterialsString, out isCarryingMaterialValue))
            {
                throw new FormatException(string.Format("{0} is invelid input. Please enter True/False is the track is carrying hazardous materials", isCarryingMaterialsString));
            }

            if (!i_VehicleDetailsDic.TryGetValue("The maximium carrying weight", out maxCarryWeightString) || string.IsNullOrEmpty(maxCarryWeightString))
            {
                throw new KeyNotFoundException("Please enter the maximum carrying weight of the truck");
            }

            if (!float.TryParse(maxCarryWeightString, out maxCarryWeightValue))
            {
                throw new FormatException(string.Format("{0} is invelid input. Please enter a float number representing the maximum carrying weight of the truck.", maxCarryWeightString));
            }

            IsCarryingHazardousMaterials = isCarryingMaterialValue;
            MaxCarryingWeight = maxCarryWeightValue;
        }

        public override Dictionary<string, string> CreateVehicleDetailsDict()
        {
            Dictionary<string, string> vehicleDedatilsDic = new Dictionary<string, string>();

            vehicleDedatilsDic = base.CreateVehicleDetailsDict();
            vehicleDedatilsDic.Add("The track is carrying hazardous materials (True/False)", null);
            vehicleDedatilsDic.Add("The maximium carrying weight", null);
            ////vehicleDedatilsDic.Add("Fuel Type (Soler/Octan95/Octan96,Octan98)", k_TruckFuelType.ToString());

            return vehicleDedatilsDic;
        }

        public override List<string> GetVehicleDetails(List<string> i_VehicleDetalisDic)
        {
            i_VehicleDetalisDic = base.GetVehicleDetails(i_VehicleDetalisDic);
            i_VehicleDetalisDic.Add(string.Format("Is carrying hazardous materials: {0}", v_IsCarryingHazardousMaterials.ToString()));
            i_VehicleDetalisDic.Add(string.Format("The max weight capacity is: {0}", m_MaxCarryingWeight));

            return i_VehicleDetalisDic;
        }
    }
}

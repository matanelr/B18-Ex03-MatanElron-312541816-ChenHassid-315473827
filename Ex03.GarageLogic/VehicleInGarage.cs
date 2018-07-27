using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private Vehicle m_Vehicle;
        private string m_NameOfOwner;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public enum eVehicleStatus
        {
            InRepair,
            IsRepaired,
            IsPaid,
            None
        }

        public Vehicle Vehicle
        {
            get
            {
                return this.m_Vehicle;
            }

            set
            {
                this.m_Vehicle = value;
            }
        }

        public string NameOfOwner
        {
            get
            {
                return this.m_NameOfOwner;
            }

            set
            {
                this.m_NameOfOwner = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return this.m_OwnerPhoneNumber;
            }

            set
            {
                this.m_OwnerPhoneNumber = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return this.m_VehicleStatus;
            }

            set
            {
                this.m_VehicleStatus = value;
            }
        }

        public VehicleInGarage(Vehicle i_Vehicle, string i_NameOfOwner, string i_OwnerPhoneNumber)
        {
            this.m_Vehicle = i_Vehicle;
            this.m_NameOfOwner = i_NameOfOwner;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public List<string> GetAllVehicleDetails()
        {
            List<string> vehicleInGarageDetails = new List<string>();

            vehicleInGarageDetails.Add(string.Format("The owner of the vehicle: {0}", NameOfOwner));
            vehicleInGarageDetails.Add(string.Format("The status of the vehicle: {0}", VehicleStatus.ToString()));
            vehicleInGarageDetails = Vehicle.GetVehicleDetails(vehicleInGarageDetails);
            vehicleInGarageDetails = Vehicle.EngineOfVehicle.GetEngineDetails(vehicleInGarageDetails);

            return vehicleInGarageDetails;
        }
    }
}

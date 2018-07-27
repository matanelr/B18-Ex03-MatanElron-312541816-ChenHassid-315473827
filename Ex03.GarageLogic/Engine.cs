using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private eEngineType m_EngineType;
        private float m_CurrentEnergy;
        private float m_MaxEnergy;

        public enum eEngineType
        {
            Fuel,
            Electric
        }

        internal float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
            }
        }

        internal float MaxEnergy
        {
            get
            {
                return m_MaxEnergy;
            }

            set
            {
                m_MaxEnergy = value;
            }
        }

        public eEngineType EngineType
        {
            get
            {
                return m_EngineType;
            }

            set
            {
                m_EngineType = value;
            }
        }

        internal Engine(eEngineType i_EngineType)
        {
            EngineType = i_EngineType;
            MaxEnergy = 0;
        }

        public virtual void AddEnergy(float i_AmountOfEnergyToAdd)
        {
            float currentNewEnergy = m_CurrentEnergy + i_AmountOfEnergyToAdd;

            if (currentNewEnergy > m_MaxEnergy)
            {
                throw new OutOfRangeException(0, m_MaxEnergy - m_CurrentEnergy);
            }
            else
            {
                m_CurrentEnergy = currentNewEnergy;
            }
        }

        public virtual Dictionary<string, string> AddEngineDetailsToDict(Dictionary<string, string> i_VehicleDetailsDictionary)
        {
            i_VehicleDetailsDictionary.Add("Current energy", null);

            return i_VehicleDetailsDictionary;
        }

        public virtual void AddEngineInformation(Dictionary<string, string> i_VehicleDetailsDictionary)
        {
            string currentEnergyString;
            float currentEnergy;

            if (!i_VehicleDetailsDictionary.TryGetValue("Current energy", out currentEnergyString) || string.IsNullOrEmpty(currentEnergyString))
            {
                throw new KeyNotFoundException("Current energy");
            }

            if (!float.TryParse(currentEnergyString, out currentEnergy))
            {
                throw new ArgumentException("Invalid 'Current energy' input, please enter a number");
            }

            CurrentEnergy = currentEnergy;
        }

        public virtual List<string> GetEngineDetails(List<string> i_DetailsDictionary)
        {
            i_DetailsDictionary.Add(string.Format("Engine type: {0}", this.EngineType.ToString()));

            return i_DetailsDictionary;
        }

        public void CheckValidCurrentEnergy(string i_CurrentEnergy)
        {
            float currentEnergy;

            if (!float.TryParse(i_CurrentEnergy, out currentEnergy))
            {
                throw new ArgumentException("Invalid 'Current energy' input, please enter a number");
            }

            if (currentEnergy > MaxEnergy)
            {
                throw new ArgumentException(string.Format("Invalid 'Current energy' input. {0} is bigger than the max energy:{1}", i_CurrentEnergy, MaxEnergy));
            }
        }
    }
}
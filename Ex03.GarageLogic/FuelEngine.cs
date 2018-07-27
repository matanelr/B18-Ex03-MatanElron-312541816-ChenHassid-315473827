using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98,
            None
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public float CurrentFuel
        {
            get
            {
                return this.CurrentEnergy;
            }

            set
            {
                this.CurrentEnergy = value;
            }
        }

        public float MaxFuel
        {
            get
            {
                return this.MaxEnergy;
            }

            set
            {
                this.MaxEnergy = value;
            }
        }

        internal FuelEngine(eFuelType i_FuelType)
            : base(eEngineType.Fuel)
        {
            m_FuelType = i_FuelType;
        }

        public override List<string> GetEngineDetails(List<string> i_DetailsDictionary)
        {
            i_DetailsDictionary = base.GetEngineDetails(i_DetailsDictionary);
            i_DetailsDictionary.Add(string.Format("Current remaining fuel: {0}", CurrentFuel));
            i_DetailsDictionary.Add(string.Format("Liter fuel tank: {0}", MaxEnergy));
            i_DetailsDictionary.Add(string.Format("Fuel type: {0}", FuelType.ToString()));

            return i_DetailsDictionary;
        }

        public void Refuel(float i_AmountToRefuel, eFuelType i_FuelType)
        {
            if (FuelType == i_FuelType)
            {
                try
                {
                    this.AddEnergy(i_AmountToRefuel);
                }
                catch (OutOfRangeException exception)
                {
                    throw exception;
                }
            }
            else
            {
                throw new ArgumentException(string.Format("{0} fuel type doesn't fit. This vehicle fuel type is :{1}{2}", i_FuelType, FuelType, Environment.NewLine));
            }
        }
    }
}
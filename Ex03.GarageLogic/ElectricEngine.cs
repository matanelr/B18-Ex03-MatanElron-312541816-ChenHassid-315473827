using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        internal ElectricEngine() : base(eEngineType.Electric)
        {
        }

        public float BatteryTimeLeft
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

        public float MaxBatteryTime
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

        public void Charge(float i_MinutesToCharge)
        {
            try
            {
                this.AddEnergy(i_MinutesToCharge);
            }
            catch (OutOfRangeException exception)
            {
                throw exception;
            }
        }

        public override List<string> GetEngineDetails(List<string> i_DetailsDictionary)
        {
            i_DetailsDictionary = base.GetEngineDetails(i_DetailsDictionary);
            i_DetailsDictionary.Add(string.Format("Current remaining charge time: {0}", CurrentEnergy));
            i_DetailsDictionary.Add(string.Format("Maximum battery life: {0}", MaxEnergy));

            return i_DetailsDictionary;
        }
    }
}

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentPressure;
        private float m_MaxPressure;

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentPressure
        {
            get
            {
                return m_CurrentPressure;
            }

            set
            {
                m_CurrentPressure = value;
            }
        }

        public float MaxPressure
        {
            get
            {
                return m_MaxPressure;
            }

            set
            {
                m_MaxPressure = value;
            }
        }

        internal Wheel(float i_MaxPressure)
        {
            m_ManufacturerName = string.Empty;
            m_CurrentPressure = 0;
            m_MaxPressure = i_MaxPressure;
        }

        public void AddAirPressure(float i_AmountPressureToAdd)
        {
            float currentNewPressure = m_CurrentPressure + i_AmountPressureToAdd;

            if (currentNewPressure > m_MaxPressure)
            {
                throw new OutOfRangeException(0, MaxPressure - CurrentPressure);
            }
            else
            {
                m_CurrentPressure = currentNewPressure;
            }
        }
    }
}
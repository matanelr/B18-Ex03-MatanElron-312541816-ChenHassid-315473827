using System;

namespace Ex03.GarageLogic
{
    public class OutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public OutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("An error occured. Please choose a value in the specific range: {0} to {1}", i_MinValue, i_MaxValue))
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
        }
    }
}

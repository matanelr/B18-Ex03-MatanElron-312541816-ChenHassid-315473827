using System;

namespace Ex03.GarageLogic
{
    public class UnavailableOptionException : Exception
    {
        internal string m_Message;

        public UnavailableOptionException(string i_Message)
            : base(string.Format("An error occured. The option is unavailable. {0}", i_Message))
        {
            this.m_Message = i_Message;
        }
    }
}

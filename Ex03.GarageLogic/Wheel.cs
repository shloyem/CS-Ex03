using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public struct Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(
            string i_ManufacturerName, 
            float i_MaxAirPressure, 
            float i_WheelsCurrentAirPreassure)
        {
            m_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = 0;
            Pump(i_WheelsCurrentAirPreassure);
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public void Pump(float i_AirToBeAdded)
        {
            bool isInRange = i_AirToBeAdded >= 0 && CurrentAirPressure + i_AirToBeAdded <= r_MaxAirPressure;

            if (isInRange)
            {
                m_CurrentAirPressure += i_AirToBeAdded;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            return string.Format(
                "({0}, {1})",
                m_ManufacturerName,
                m_CurrentAirPressure);
        }
    }
}

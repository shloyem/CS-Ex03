using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly float r_WheelsMaxAirPreassure;
        private string m_ModelName;
        private string m_LicenseNumber;
        private Wheel[] m_Wheels;
        private Engine m_EngineType;

        // $G$ DSN-008 (-5) A constructor should not be interactive. It should be used for initialization only.
        public Vehicle(
            string i_ModelName, 
            string i_LicenseNumber, 
            int i_AmountOfWheels, 
            string i_WheelsManufacturerName, 
            float i_WheelsMaxAirPressure, 
            float i_WheelsCurrentAirPreassure,
            Engine i_EngineType)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_Wheels = new Wheel[i_AmountOfWheels];
            setUpWheels(i_WheelsManufacturerName, i_WheelsMaxAirPressure, i_WheelsCurrentAirPreassure);
            r_WheelsMaxAirPreassure = i_WheelsMaxAirPressure;
            m_EngineType = i_EngineType;
        }

        public string LicenseID
        {
            get { return m_LicenseNumber; }
        }

        public float WheelsMaxAirPreassure
        {
            get { return r_WheelsMaxAirPreassure; }
        }

        public Engine EngineType
        {
            get { return m_EngineType; }
        }

        public float EnergyPercentLeft
        {
            get { return m_EngineType.CurrentAmountOfEnergy; }
        }

        private void setUpWheels(
            string i_WheelsManufacturerName, 
            float i_WheelsMaxAirPressure, 
            float i_WheelsCurrentAirPreassure)
        {
            for(int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i] = new Wheel(i_WheelsManufacturerName, i_WheelsMaxAirPressure, i_WheelsCurrentAirPreassure);
            }
        }

        public void PumpWheels(float i_DesiredPreassure)
        {
            bool isInRange;

            // Using standard for loop instead of foreach because:
            // changing a valuetype in a foreach loop will change a copy of the object and not the object itself.
            for (int i = 0; i < m_Wheels.Length; i++) 
            {
                isInRange = i_DesiredPreassure >= m_Wheels[i].CurrentAirPressure && i_DesiredPreassure <= m_Wheels[i].MaxAirPressure;

                if (isInRange)
                {
                    float neededAirForDesiredPreassure = i_DesiredPreassure - m_Wheels[i].CurrentAirPressure;

                    m_Wheels[i].Pump(neededAirForDesiredPreassure);
                }
                else
                {
                    throw new ValueOutOfRangeException(m_Wheels[i].CurrentAirPressure, m_Wheels[i].MaxAirPressure);
                }
            }
        }

        private string[] wheelsInfo()
        {
            List<string> wheelsInfo = new List<string>();

            foreach(Wheel wheel in m_Wheels)
            {
                wheelsInfo.Add(wheel.ToString());
            }

            return wheelsInfo.ToArray();
        }

        public override string ToString()
        {
            return string.Format(
@"Model: {0}
License: {1}
Wheels info (Manufacture name, Current air preassure):
    {2}
Engine Info:
    {3}",
                m_ModelName,
                m_LicenseNumber,
                string.Join(", ", wheelsInfo()),
                EngineType.ToString());
        }
    }
}

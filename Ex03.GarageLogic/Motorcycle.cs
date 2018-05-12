using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    // $G$ DSN-011 (-5) No use of internal constructors.
    public class Motorcycle : Vehicle
    {
        private const int k_MotorcycleWheelsAmount = 2;
        private const float k_WheelsMaxAirPressure = 33;
        private int m_EngineSize;
        private eLicenseLevel m_LicenseLevel;
        private Engine m_EngineType;

        public Motorcycle(
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacturerName,
            float i_WheelsCurrentAirPreassure,
            Engine i_EngineType,
            int i_EngineSize, 
            eLicenseLevel i_LicenseLevel)
            : base(
                i_ModelName, 
                i_LicenseNumber, 
                k_MotorcycleWheelsAmount,
                i_WheelsManufacturerName,
                k_WheelsMaxAirPressure,
                i_WheelsCurrentAirPreassure,
                i_EngineType)
        {
            m_EngineSize = i_EngineSize;
            m_LicenseLevel = i_LicenseLevel;
            m_EngineType = i_EngineType;
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type: Motorcycle
Engine size: {0}
License level: {1}
{2}",
                m_EngineSize, 
                m_LicenseLevel,
                base.ToString());
        }

        public enum eLicenseLevel
        {
            A1 = 1,
            AB,
            A2,
            B1
        }
    }
}

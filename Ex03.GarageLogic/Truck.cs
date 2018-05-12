using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public const int k_TruckWheelsAmount = 12;
        private const float k_WheelsMaxAirPressure = 32;
        private bool m_IsDangerous;
        private float m_MaximumAllowedCarriageWeight;

        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacturerName,
            float i_WheelsCurrentAirPreassure,
            Engine i_EngineType,
            bool i_IsDangerous,
            float i_MaximumAllowedCarriageWeight)
            : base(
                i_ModelName,
                i_LicenseNumber,
                k_TruckWheelsAmount,
                i_WheelsManufacturerName,
                k_WheelsMaxAirPressure,
                i_WheelsCurrentAirPreassure,
                i_EngineType)
        {
            m_IsDangerous = i_IsDangerous;
            m_MaximumAllowedCarriageWeight = i_MaximumAllowedCarriageWeight;
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type: Truck
Maximum allowed carriage weight: {0}
Carring dangerous matters: {1} 
{2}",
                m_MaximumAllowedCarriageWeight, 
                m_IsDangerous,
                base.ToString()); 
        }
    }
}

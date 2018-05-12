using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eDoorsAmount
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public enum eCarColor
        {
            Yellow = 1,
            White,
            Black,
            Blue
        }

        private const int k_CarWheelsAmount = 4;
        private const float k_WheelsMaxAirPressure = 30;
        private eCarColor m_CarColor;
        private eDoorsAmount m_DoorsAmount;

        public Car(
            string i_ModelName, 
            string i_LicenseNumber, 
            string i_WheelsManufacturerName,
            float i_WheelsCurrentAirPreassure,
            Engine i_EngineType,
            eCarColor i_CarColor, 
            eDoorsAmount i_DoorsAmount)
            : base(
                  i_ModelName, 
                  i_LicenseNumber, 
                  k_CarWheelsAmount,
                  i_WheelsManufacturerName,
                  k_WheelsMaxAirPressure,
                  i_WheelsCurrentAirPreassure,
                  i_EngineType)
        {
            m_CarColor = i_CarColor;
            m_DoorsAmount = i_DoorsAmount;
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type: Car
Color: {0}
Doors amount: {1} 
{2}",
                m_CarColor,
                m_DoorsAmount,
                base.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasolineEngine : Engine
    {
        private eGasolineType m_GasolineType;

        public GasolineEngine(
            float i_MaximumAmountOfGasoline, 
            eGasolineType i_GasolineType, 
            float i_CurrentAmountOfGasoline)
            : base(i_MaximumAmountOfGasoline, i_CurrentAmountOfGasoline)
        {
            m_GasolineType = i_GasolineType;
        }

        public eGasolineType GasolineType
        {
            get { return m_GasolineType; }
        }

        public void AddGas(eGasolineType i_GasType, float i_GasAmunt)
        {
            if(i_GasType == m_GasolineType)
            {
                AddEnergy(i_GasAmunt); // Range check is performed inside the method
            }
            else
            {
                throw new ArgumentException("Given gas type is not a suitable gas type for this vehicle.");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Gasoline type: {0}
Gasoline left in tank: {1}",
                m_GasolineType.ToString(),
                base.ToString());
        }

        public enum eGasolineType
        {
            Octan95 = 1,
            Octan96,
            Octan98
        }
    }
}

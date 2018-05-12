using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaximumAmountOfEnergy;
        private float m_CurrentAmountOfEnergy;

        public Engine(float i_MaximumAmountOfEnergy, float i_CurrentAmountOfEnergy)
        {
            r_MaximumAmountOfEnergy = i_MaximumAmountOfEnergy;
            m_CurrentAmountOfEnergy = 0;
            AddEnergy(i_CurrentAmountOfEnergy);
        }

        public float CurrentAmountOfEnergy
        {
            get { return m_CurrentAmountOfEnergy; }
        }

        public float MaximumAmountOfEnergy
        {
            get { return r_MaximumAmountOfEnergy; }
        }

        protected void AddEnergy(float i_EnergyAmount)
        {
            bool isInRange = i_EnergyAmount >= 0 && m_CurrentAmountOfEnergy + i_EnergyAmount <= r_MaximumAmountOfEnergy;

            if (isInRange)
            {
                m_CurrentAmountOfEnergy += i_EnergyAmount;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaximumAmountOfEnergy - m_CurrentAmountOfEnergy);
            }
        }

        public override string ToString()
        {
            return m_CurrentAmountOfEnergy.ToString();
        }
    }
}

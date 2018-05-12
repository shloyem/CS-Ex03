using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaximumBatteryTime, float i_CurrentBatteryRemainingTime)
            : base(i_MaximumBatteryTime, i_CurrentBatteryRemainingTime)
        {
        }

        public void Charge(float i_TimeToCharge)
        {
            AddEnergy(i_TimeToCharge);
        }

        public override string ToString()
        {
            return string.Format("Battery left: {0}", base.ToString());
        }
    }
}

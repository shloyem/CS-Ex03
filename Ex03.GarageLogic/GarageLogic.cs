using System; 
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageLogic
    {
        private Dictionary<string, OrderInformation> m_CarsInGarage;

        public GarageLogic()
        {
            m_CarsInGarage = new Dictionary<string, OrderInformation>();
        }

        public void AddVehicle(string i_ClientName, string i_ClientPhoneNumber, Vehicle i_Vehicle)
        {
            if(!m_CarsInGarage.ContainsKey(i_Vehicle.LicenseID))
            {
                m_CarsInGarage.Add(i_Vehicle.LicenseID, new OrderInformation(i_ClientName, i_ClientPhoneNumber, i_Vehicle));
            }
            else
            {
                OrderInformation order = m_CarsInGarage[i_Vehicle.LicenseID];

                order.Status = OrderInformation.eStatus.InProgress;     // Orderinfo is a value type and therefore must be update manually in m_CarsInGarage
                m_CarsInGarage.Remove(i_Vehicle.LicenseID);
                m_CarsInGarage.Add(i_Vehicle.LicenseID, order); 
            }
        }

        public string[] GetAllLicenseNumbers()
        {
            List<string> LicenseIDs = new List<string>();

            foreach(OrderInformation order in m_CarsInGarage.Values)
            {
               LicenseIDs.Add(order.Vehicle.LicenseID);
            }

            return LicenseIDs.ToArray();
        }

        public string[] GetAllLicenseNumbersFilteredByStatus(OrderInformation.eStatus i_Status)
        {
            List<string> LicenseIDs = new List<string>();

            foreach (OrderInformation order in m_CarsInGarage.Values)
            {
                if (order.Status == i_Status)
                {
                    LicenseIDs.Add(order.Vehicle.LicenseID);
                }
            }

            return LicenseIDs.ToArray();
        }

        public void ChangeStatus(string i_LicenseNumber, OrderInformation.eStatus i_NewStatus)
        {
            if(m_CarsInGarage.ContainsKey(i_LicenseNumber))
            {
                OrderInformation order = m_CarsInGarage[i_LicenseNumber];

                order.Status = i_NewStatus;
                m_CarsInGarage.Remove(i_LicenseNumber); // Orderinfo is a value type and therefore must be update manually in m_CarsInGarage
                m_CarsInGarage.Add(i_LicenseNumber, order);
            }
            else
            {
                throw new ArgumentException("Vehicle is not in the garage.");
            }
        }

        public void PumpVehicleWheelsToMax(string i_LicenseNumber)
        {
            bool isVehicleInGarage = m_CarsInGarage.ContainsKey(i_LicenseNumber);

            if (isVehicleInGarage)
            {
                OrderInformation order = m_CarsInGarage[i_LicenseNumber];

                order.Vehicle.PumpWheels(order.Vehicle.WheelsMaxAirPreassure); // Range check is being preformed inside method.
            }
            else
            {
                throw new ArgumentException("Vehicle is not in garage.");
            }
        }

        public void FuelGasolineVehicle(string i_LicenseNumber, GasolineEngine.eGasolineType i_GasolineType, float i_AmountToFuel)
        {
            bool isCarInGarage = m_CarsInGarage.ContainsKey(i_LicenseNumber);

            if(isCarInGarage)
            {
                OrderInformation order = m_CarsInGarage[i_LicenseNumber];
                GasolineEngine vehicleEngine = order.Vehicle.EngineType as GasolineEngine;

                if(vehicleEngine != null)
                {
                    vehicleEngine.AddGas(i_GasolineType, i_AmountToFuel); // Range check is being preformed inside method.
                }
                else
                {
                    throw new ArgumentException("Vehicle is not powered by a gasoline engine.");
                }
            }
            else
            {
                throw new ArgumentException("Vehicle is not in garage.");
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_AmountToCharge)
        {
            bool isCarInGarage = m_CarsInGarage.ContainsKey(i_LicenseNumber);

            if (isCarInGarage)
            {
                OrderInformation order = m_CarsInGarage[i_LicenseNumber];
                ElectricEngine vehicleEngine = order.Vehicle.EngineType as ElectricEngine;

                if (vehicleEngine != null)
                {
                    vehicleEngine.Charge(i_AmountToCharge); // Range check is being preformed inside method.
                }
                else
                {
                    throw new ArgumentException("Vehicle is not powered by an electric engine.");
                }
            }
            else
            {
                throw new ArgumentException("Vehicle is not in garage.");
            }
        }

        public string GetVehicleFullInformationByLicenseNumber(string i_LicenseNumber)
        {
            string fullInfo = string.Empty;

            if (m_CarsInGarage.ContainsKey(i_LicenseNumber))
            {
                fullInfo = m_CarsInGarage[i_LicenseNumber].ToString();
            }
            else
            {
                throw new ArgumentException("Vehicle is not in garage.");
            }

            return fullInfo;
        }

        public struct OrderInformation
        {
            private ClientInformation m_ClientInfo;
            private eStatus m_Status;
            private Vehicle m_Vehicle;

            public OrderInformation(string i_Name, string i_PhoneNumber, Vehicle i_Vehicle)
            {
                m_ClientInfo = new ClientInformation(i_Name, i_PhoneNumber);
                m_Status = eStatus.InProgress;
                m_Vehicle = i_Vehicle;
            }

            public string ClientName
            {
                get { return m_ClientInfo.Name; }
                set { m_ClientInfo.Name = value; }
            }

            public string ClientPhoneNumber
            {
                get { return m_ClientInfo.PhoneNumber; }
                set { m_ClientInfo.PhoneNumber = value; }
            }

            public eStatus Status
            {
                get { return m_Status; }
                set { m_Status = value; }
            }

            public Vehicle Vehicle
            {
                get { return m_Vehicle; }
            }

            public override bool Equals(object obj)
            {
                Vehicle objAsVehicle = null;
                bool equals = false;

                if ((objAsVehicle = obj as Vehicle) != null)
                {
                    equals = m_Vehicle.LicenseID == objAsVehicle.LicenseID;
                }

                return equals;
            }

            public override int GetHashCode()
            {
                return m_Vehicle.LicenseID.GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(
@"{0}
Status: {1}
{2}",
                    m_ClientInfo.ToString(),
                    m_Status.ToString(),
                    m_Vehicle.ToString());
            }

            public enum eStatus
            {
                InProgress = 1,
                Done = 2,
                Paid = 3
            }

            public struct ClientInformation
            {
                private string m_Name;
                private string m_PhoneNumber;

                public ClientInformation(string i_Name, string i_PhoneNumber)
                {
                    m_Name = i_Name;
                    m_PhoneNumber = i_PhoneNumber;
                }

                public string Name
                {
                    get { return m_Name; }
                    set { m_Name = value; }
                }

                public string PhoneNumber
                {
                    get { return m_PhoneNumber; }
                    set { m_PhoneNumber = value; }
                }

                public override string ToString()
                {
                    return string.Format(
@"Owner name: {0}
Owner phone: {1}",
                        m_Name,
                        m_PhoneNumber);
                }
            }
        }
    }
}

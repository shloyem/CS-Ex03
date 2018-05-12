using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        private static void getVehicleTypeSetUp(
            eVehiclesTypes i_VehicleKind,
            out Type o_VehicleType, 
            out Type o_EngineType)
        {
            o_VehicleType = null;
            o_EngineType = null;

            switch (i_VehicleKind)
            {
                case eVehiclesTypes.ElectricCar:
                {
                    o_VehicleType = typeof(Car);
                    o_EngineType = typeof(ElectricEngine);
                    break;
                }

                case eVehiclesTypes.GasolineCar:
                {
                    o_VehicleType = typeof(Car);
                    o_EngineType = typeof(GasolineEngine);
                    break;
                }

                case eVehiclesTypes.ElectricMotorcycle:
                {
                    o_VehicleType = typeof(Motorcycle);
                    o_EngineType = typeof(ElectricEngine);
                    break;
                }

                case eVehiclesTypes.GasolineMotorcycle:
                {
                    o_VehicleType = typeof(Motorcycle);
                    o_EngineType = typeof(GasolineEngine);
                    break;
                }

                case eVehiclesTypes.Truck:
                {
                    o_VehicleType = typeof(Truck);
                    o_EngineType = typeof(GasolineEngine);
                    break;
                }
            }
        }

        public static ParameterInfo[] GetConstarctorArguments(eVehiclesTypes i_VehicleKind)
        {
            Type vehicleType = null;
            Type engineType = null;
            List<ParameterInfo> specificVehicleConstractorArguments = new List<ParameterInfo>();
            ParameterInfo[] constructorParameters;

            getVehicleTypeSetUp(i_VehicleKind, out vehicleType, out engineType);

            constructorParameters = vehicleType.GetConstructors()[0].GetParameters();

            foreach (ParameterInfo pi in constructorParameters)
            {
                if(!pi.ParameterType.Equals(typeof(Engine)))
                {
                    specificVehicleConstractorArguments.Add(pi);
                }
                else
                {
                    specificVehicleConstractorArguments.AddRange(engineType.GetConstructors()[0].GetParameters());
                }
            }

            return specificVehicleConstractorArguments.ToArray();
        }

        public static Vehicle CreateVehicle(
            eVehiclesTypes i_VehicleType,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelsManufacturerName,
            float i_WheelsCurrentAirPreassure,
            params object[] i_SpecificVehicleTypeArguments)
        {
            Vehicle vehicleToReturn = null;

            switch(i_VehicleType)
            {
                case eVehiclesTypes.GasolineMotorcycle:
                    {
                        vehicleToReturn = new Motorcycle(
                            i_ModelName,
                            i_LicenseNumber,
                            i_WheelsManufacturerName,
                            i_WheelsCurrentAirPreassure,
                            new GasolineEngine(5.5f, GasolineEngine.eGasolineType.Octan95, (float)i_SpecificVehicleTypeArguments[0]),
                            (int)i_SpecificVehicleTypeArguments[1],
                            // $G$ DSN-001 (-10) You should have used polymorphism to implement this. (for example each vehicle providing a virtual dictionary of properties)
                            (Motorcycle.eLicenseLevel)i_SpecificVehicleTypeArguments[2]);
                        break;
                    }

                case eVehiclesTypes.ElectricMotorcycle:
                    {
                        vehicleToReturn = new Motorcycle(
                            i_ModelName,
                            i_LicenseNumber,
                            i_WheelsManufacturerName,
                            i_WheelsCurrentAirPreassure,
                            new ElectricEngine(2.7f, (float)i_SpecificVehicleTypeArguments[0]),
                            (int)i_SpecificVehicleTypeArguments[1],
                            (Motorcycle.eLicenseLevel)i_SpecificVehicleTypeArguments[2]);
                        break;
                    }

                case eVehiclesTypes.GasolineCar:
                    {
                        vehicleToReturn = new Car(
                            i_ModelName,
                            i_LicenseNumber,
                            i_WheelsManufacturerName,
                            i_WheelsCurrentAirPreassure,
                            new GasolineEngine(42f, GasolineEngine.eGasolineType.Octan98, (float)i_SpecificVehicleTypeArguments[0]),
                            (Car.eCarColor)i_SpecificVehicleTypeArguments[1],
                            (Car.eDoorsAmount)i_SpecificVehicleTypeArguments[2]);
                        break;
                    }
                    
                case eVehiclesTypes.ElectricCar:
                    {
                        vehicleToReturn = new Car(
                            i_ModelName,
                            i_LicenseNumber,
                            i_WheelsManufacturerName,
                            i_WheelsCurrentAirPreassure,
                            new ElectricEngine(2.5f, (float)i_SpecificVehicleTypeArguments[0]),
                            (Car.eCarColor)i_SpecificVehicleTypeArguments[1],
                            (Car.eDoorsAmount)i_SpecificVehicleTypeArguments[2]);
                        break;
                    }

                case eVehiclesTypes.Truck:
                    {
                        vehicleToReturn = new Truck(
                            i_ModelName,
                            i_LicenseNumber,
                            i_WheelsManufacturerName,
                            i_WheelsCurrentAirPreassure,
                            new GasolineEngine(135f, GasolineEngine.eGasolineType.Octan96, (float)i_SpecificVehicleTypeArguments[0]),
                            (bool)i_SpecificVehicleTypeArguments[1],
                            (float)i_SpecificVehicleTypeArguments[2]);
                        break;
                    }
            }

            return vehicleToReturn;
        }

        public static float getEngineMaximumEnergyCapacityByVehicleType(eVehiclesTypes i_VehicleType)
        {
            float returnedValue = 0;

            switch (i_VehicleType)
            {
                case eVehiclesTypes.GasolineCar:
                    {
                        returnedValue = 42f;
                        break;
                    }

                case eVehiclesTypes.ElectricCar:
                    {
                        returnedValue = 2.5f;
                        break;
                    }

                case eVehiclesTypes.GasolineMotorcycle:
                    {
                        returnedValue = 5.5f;
                        break;
                    }

                case eVehiclesTypes.ElectricMotorcycle:
                    {
                        returnedValue = 2.7f;
                        break;
                    }

                case eVehiclesTypes.Truck:
                    {
                        returnedValue = 135f;
                        break;
                    }
            }

            return returnedValue;
        }

        public static float getWheelsMaximumAirPreassureByVehicleType(eVehiclesTypes i_VehicleType)
        {
            float returnedValue = 0;

            switch(i_VehicleType)
            {
                case eVehiclesTypes.GasolineCar:
                case eVehiclesTypes.ElectricCar:
                {
                    returnedValue = 30f;
                    break;
                }

                case eVehiclesTypes.GasolineMotorcycle:
                case eVehiclesTypes.ElectricMotorcycle:
                {
                    returnedValue = 33f;
                    break;
                }

                case eVehiclesTypes.Truck:
                {
                    returnedValue = 32f;
                    break;
                }
            }

            return returnedValue;
        }

        public enum eVehiclesTypes
        {
            GasolineMotorcycle = 1,
            ElectricMotorcycle,
            GasolineCar,
            ElectricCar,
            Truck
        }
    }
}

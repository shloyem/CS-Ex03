using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UserInterface
    {
        private const string k_MainMenu =
@"What would you like to do next? Enter the number next to the desired option
    0 - Exit
    1 - Add a new vehicle to the garage
    2 - Display a license Number list of the vehicles in the garage
    3 - Change a vehicle state in the garage
    4 - Pump a vehicle wheels to their maximum value
    5 - Fuel a vehicle powered by a gasoline engine
    6 - Charge a vehicle powered by an electric engine
    7 - Show full vehicle details of a specific license Number";

        // $G$ DSN-999 (-3) This field should be readonly.
        private GarageLogic.GarageLogic m_GarageLogic;
        private Dictionary<string, string> m_ParametersNamesToMsg;

        public UserInterface()
        {
            m_GarageLogic = new GarageLogic.GarageLogic();
            m_ParametersNamesToMsg = new Dictionary<string, string>();

            m_ParametersNamesToMsg.Add("i_ModelName", "Please insert vehicle model name:");
            m_ParametersNamesToMsg.Add("i_LicenseNumber", "Please insert vehicle license number:");
            m_ParametersNamesToMsg.Add("i_WheelsManufacturerName", "Please insert vehicle wheels manufacturer name:");
            m_ParametersNamesToMsg.Add("i_WheelsCurrentAirPreassure", "Please insert current air preassure in vehicle wheels:");
            m_ParametersNamesToMsg.Add("i_EngineSize", "Please insert engine size:");
            m_ParametersNamesToMsg.Add("i_CurrentAmountOfGasoline", "Please insert current amount of gasoline in vehicle tank:");
            m_ParametersNamesToMsg.Add("i_CurrentBatteryRemainingTime", "Please insert current amount of 'hours of use' in vehicle battery:");
            m_ParametersNamesToMsg.Add("i_LicenseLevel", "Please choose motorcycle license level out of the following options:");
            m_ParametersNamesToMsg.Add("i_CarColor", "Please choose car color out of the following options:");
            m_ParametersNamesToMsg.Add("i_DoorsAmount", "Please choose car doors amount out of the following options:");
            m_ParametersNamesToMsg.Add("i_IsDangerous", "Does truck carry any dangerous matter?:");
            m_ParametersNamesToMsg.Add("i_MaximumAllowedCarriageWeight", "Please insert truck maximum carriage weight:");
        }

        public enum eMenuOption
        {
            Exit = 0,
            AddNewVehicle,
            DisplayLicenseIDList,
            ChangeVehicleState,
            PumpVehicleWheels,
            FuelGasolineVehicle,
            ChargeElectricVehicle,
            ShowFullVehicleDetails
        }

        public void RunGarageManager()
        {
            bool userExited = false;
            int selectedOption;

            while (!userExited)
            {
                Console.WriteLine(k_MainMenu);
                while (!int.TryParse(Console.ReadLine(), out selectedOption) || !Enum.IsDefined(typeof(eMenuOption), selectedOption))
                {
                    Console.WriteLine("Invalid input, please try again.");
                }

                switch((eMenuOption)selectedOption)
                {
                    case eMenuOption.Exit:
                    {
                        userExited = true;
                        break;
                    }

                    case eMenuOption.AddNewVehicle:
                    {
                        addVehicleLogic();
                        continue;
                    }

                    case eMenuOption.DisplayLicenseIDList:
                    {
                        displayLicenseIDListLogic();
                        continue;
                    }

                    case eMenuOption.ChangeVehicleState:
                    {
                        changeVehicleStateLogic();
                        continue;
                    }

                    case eMenuOption.PumpVehicleWheels:
                    {
                        pumpVehicleWheelsLogic();
                        continue;
                    }

                    case eMenuOption.FuelGasolineVehicle:
                    {
                        fuelGasolineVehicleLogic();
                        continue;
                    }

                    case eMenuOption.ChargeElectricVehicle:
                    {
                        chargeElectricVehicleLogic();
                        continue;
                    }

                    case eMenuOption.ShowFullVehicleDetails:
                    {
                        showFullVehicleDetailsLogic();
                        continue;
                    }
                }
            }
        }

        private string getLicenseNumberFromUser()
        {
            Console.WriteLine("Please insert license number of desired vehicle: ");
            return Console.ReadLine();
        }

        private bool getBoolFromUser(string i_Msg)
        {
            int intAnswer;
            bool isValid;

            do
            {
                Console.WriteLine(i_Msg);
                Console.WriteLine(
@"    1 - Yes
    2 - No");

                isValid = integerTryParseWithRangeValidation(Console.ReadLine(), 1, 2, out intAnswer);
                if (!isValid)
                {
                    Console.WriteLine("Invliad input try again");
                    Console.WriteLine();
                }
            }
            while (!isValid);

            return intAnswer == 1;
        }

        private float getFloatFromUser(string i_Msg)
        {
            bool isFloat;
            float inputAsFloat;

            do
            {
                Console.WriteLine(i_Msg);
                isFloat = float.TryParse(Console.ReadLine(), out inputAsFloat);
                if (!isFloat)
                {
                    Console.WriteLine("Invalid input, Please try again.");
                    Console.WriteLine();
                }
            }
            while (!isFloat);

            return inputAsFloat;
        }

        private float getFloatFromUserWithRangeChecking(string i_Msg, float i_Min, float i_Max)
        {
            bool isFloat = false;
            bool isInRange = false;
            float inputAsFloat;

            do
            {
                Console.WriteLine(i_Msg);
                isFloat = float.TryParse(Console.ReadLine(), out inputAsFloat);
                if(!isFloat)
                {
                    Console.WriteLine("Invalid input, Please try again.");
                    Console.WriteLine();
                }
                else
                {
                    isInRange = inputAsFloat >= i_Min && inputAsFloat <= i_Max;
                    if (!isInRange)
                    {
                        Console.WriteLine(string.Format("Given value is out of range. Allowed Values are: {0} - {1}.", i_Min, i_Max));
                        Console.WriteLine();
                    }
                }
            }
            while (!isFloat || !isInRange);

            return inputAsFloat;
        }

        private int getIntFromUser(string i_Msg)
        {
            bool isInt;
            int inputAsint;

            do
            {
                Console.WriteLine(i_Msg);
                isInt = int.TryParse(Console.ReadLine(), out inputAsint);
                if (!isInt)
                {
                    Console.WriteLine("Invalid input, Please try again.");
                    Console.WriteLine();
                }
            }
            while (!isInt);

            return inputAsint;
        }

        private int getEnumSelectionIdentifierFromUser<T>(string i_Msg)
        {
            bool isValidChoice;
            int amountOfAvailableChoices;
            int selectedChoice;

            do
            {
                Console.WriteLine(i_Msg);
                Console.Write(enumValuesNextToName<T>(out amountOfAvailableChoices));

                isValidChoice = integerTryParseWithRangeValidation(Console.ReadLine(), 1, amountOfAvailableChoices, out selectedChoice);
                if (!isValidChoice)
                {
                    Console.WriteLine("Invalid choice, please try again.");
                    Console.WriteLine();
                }
            }
            while (!isValidChoice);

            return selectedChoice;
        }

        private string enumValuesNextToName<T>(out int o_AmountOfOptions)
        {
            StringBuilder msg = new StringBuilder();
            Array enumNames = Enum.GetNames(typeof(T));
            int index = 1;
            o_AmountOfOptions = enumNames.Length;

            for (int i = 0; i < o_AmountOfOptions; i++)
            {
                msg.AppendLine(string.Format(@"     {0}. {1}", index, normalizeEnumName(enumNames.GetValue(i).ToString())));
                index++;
            }

            return msg.ToString();
        }

        private string normalizeEnumName(string i_EnumName)
        {
            StringBuilder normalizedEnumName = new StringBuilder(i_EnumName);

            for(int i = 1; i < normalizedEnumName.Length; i++)
            {
                if(char.IsLower(normalizedEnumName[i - 1]) && char.IsUpper(normalizedEnumName[i]))
                {
                    normalizedEnumName.Replace(normalizedEnumName[i].ToString(), string.Format(" {0}", char.ToLower(normalizedEnumName[i])));
                }
            }

            return normalizedEnumName.ToString();
        }

        private bool integerTryParseWithRangeValidation(string i_IntegerAsString, int i_MinValue, int i_MaxValue, out int o_ParsedIntVal)
        {
            bool isInteger;
            bool isValid;

            isInteger = int.TryParse(i_IntegerAsString, out o_ParsedIntVal);
            isValid = isInteger && o_ParsedIntVal >= i_MinValue && o_ParsedIntVal <= i_MaxValue;

            return isValid;
        }

        private void showFullVehicleDetailsLogic()
        {
            string licenseNumber = getLicenseNumberFromUser();

            try
            {
                Console.WriteLine("Vehicle number {0} full info:", licenseNumber);
                Console.WriteLine(m_GarageLogic.GetVehicleFullInformationByLicenseNumber(licenseNumber));
            }
            catch(ArgumentException argumntException)
            {
                Console.WriteLine(argumntException.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private void chargeElectricVehicleLogic()
        {
            string licenseNumber;
            float amountToCharge;

            licenseNumber = getLicenseNumberFromUser();
            amountToCharge = getFloatFromUser("Please insert amount of hours to charge:");

            try
            {
                m_GarageLogic.ChargeElectricVehicle(licenseNumber, amountToCharge);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (ValueOutOfRangeException valueException)
            {
                Console.WriteLine(valueException.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private void fuelGasolineVehicleLogic()
        {
            string licenseNumber;
            GasolineEngine.eGasolineType gasolineType;
            float amountToFuel;

            licenseNumber = getLicenseNumberFromUser();
            gasolineType = (GasolineEngine.eGasolineType)getEnumSelectionIdentifierFromUser<GasolineEngine.eGasolineType>("Please select gasoline type: (Number)");
            amountToFuel = getFloatFromUser("Please insert amount of gas to fuel:");

            try
            {
                m_GarageLogic.FuelGasolineVehicle(licenseNumber, gasolineType, amountToFuel);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (ValueOutOfRangeException valueException)
            {
                Console.WriteLine(valueException.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }
        
        private void pumpVehicleWheelsLogic()
        {
            try
            {
                string licenseNumber = getLicenseNumberFromUser();
                m_GarageLogic.PumpVehicleWheelsToMax(licenseNumber);
            }
            catch(ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private void changeVehicleStateLogic()
        {
            string licenseId;
            int selectedStatus;
            GarageLogic.GarageLogic.OrderInformation.eStatus status;

            licenseId = getLicenseNumberFromUser();
            selectedStatus = getEnumSelectionIdentifierFromUser<GarageLogic.GarageLogic.OrderInformation.eStatus>("Please choose desired status out of the following: (Number)");
            status = (GarageLogic.GarageLogic.OrderInformation.eStatus)selectedStatus;

            try
            {
                m_GarageLogic.ChangeStatus(licenseId, status);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private void displayLicenseIDListLogic()
        {
            bool isFilterByStatus;
            int selectedStatusAsInt;
            GarageLogic.GarageLogic.OrderInformation.eStatus selectedStatus;

            isFilterByStatus = getBoolFromUser("Would you like to filter Licenses numbers by status?");
            
            if (isFilterByStatus)
            {
                // Row was too long, so we devided it into two phases for readability.
                selectedStatusAsInt = getEnumSelectionIdentifierFromUser<GarageLogic.GarageLogic.OrderInformation.eStatus>("Please select desired status:");
                selectedStatus = (GarageLogic.GarageLogic.OrderInformation.eStatus)selectedStatusAsInt;
                Console.WriteLine("The license numbers of vehicles in garage which are in status '{0}' are:", normalizeEnumName(selectedStatus.ToString()));
                Console.WriteLine(string.Join(Environment.NewLine, m_GarageLogic.GetAllLicenseNumbersFilteredByStatus(selectedStatus)));
            }
            else
            {
                Console.WriteLine("License numbers of all vehicles in garage are:");
                Console.WriteLine(string.Join(Environment.NewLine, m_GarageLogic.GetAllLicenseNumbers()));
            }

            Console.WriteLine();
        }

        private void addVehicleLogic()
        {
            const int amountOfVehicleBaseConstructorParameters = 4;
            Dictionary<string, Range> parametersWhichShouldBeChecked = new Dictionary<string, Range>();
            ParameterInfo[] constructorParameters;
            VehicleFactory.eVehiclesTypes vehicleType;
            List<object> parameters = new List<object>();
            Vehicle createdVehicle;
            string ownerName;
            string ownerPhone;

            vehicleType = (VehicleFactory.eVehiclesTypes)getEnumSelectionIdentifierFromUser<VehicleFactory.eVehiclesTypes>(
                "Please choose vehicle type from the following options:");
            constructorParameters = VehicleFactory.GetConstarctorArguments(vehicleType);

            parametersWhichShouldBeChecked.Add(
                "i_CurrentAmountOfGasoline", 
                new Range(0f, VehicleFactory.getEngineMaximumEnergyCapacityByVehicleType(vehicleType)));
            parametersWhichShouldBeChecked.Add(
                "i_CurrentBatteryRemainingTime",
                new Range(0f, VehicleFactory.getEngineMaximumEnergyCapacityByVehicleType(vehicleType)));
            parametersWhichShouldBeChecked.Add(
                "i_WheelsCurrentAirPreassure",
                new Range(0f, VehicleFactory.getWheelsMaximumAirPreassureByVehicleType(vehicleType)));

            Console.WriteLine("Please insert vehicle owner name:");
            ownerName = Console.ReadLine();
            Console.WriteLine("Please insert vehicle owner phone number:");
            ownerPhone = Console.ReadLine();

            foreach (ParameterInfo parameter in constructorParameters)
            {
                if (parameter.ParameterType.Equals(typeof(string)))
                {
                    Console.WriteLine(m_ParametersNamesToMsg[parameter.Name]);
                    parameters.Add(Console.ReadLine());
                    continue;
                }

                if (parameter.ParameterType.Equals(typeof(float)))
                {
                    float userInputAsFloat;

                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        if(parametersWhichShouldBeChecked.ContainsKey(parameter.Name))
                        {
                            Range range = parametersWhichShouldBeChecked[parameter.Name];

                            userInputAsFloat = getFloatFromUserWithRangeChecking(
                                m_ParametersNamesToMsg[parameter.Name],
                                range.Min,
                                range.Max);
                        }
                        else
                        {
                            userInputAsFloat = getFloatFromUser(m_ParametersNamesToMsg[parameter.Name]);
                        }

                        parameters.Add(userInputAsFloat);
                    }

                    continue;
                }

                if (parameter.ParameterType.Equals(typeof(int)))
                {
                    int userInputAsInt;
                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        userInputAsInt = getIntFromUser(m_ParametersNamesToMsg[parameter.Name]);
                        parameters.Add(userInputAsInt);
                    }

                    continue;
                }

                if (parameter.ParameterType.Equals(typeof(bool)))
                {
                    bool userInputAsBool;

                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        userInputAsBool = getBoolFromUser(m_ParametersNamesToMsg[parameter.Name]);
                        parameters.Add(userInputAsBool);
                    }

                    continue;
                }

                if (parameter.ParameterType.Equals(typeof(Motorcycle.eLicenseLevel)))
                {
                    Motorcycle.eLicenseLevel licenseLevel;
                    string msg;

                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        msg = m_ParametersNamesToMsg[parameter.Name];
                        licenseLevel = (Motorcycle.eLicenseLevel)getEnumSelectionIdentifierFromUser<Motorcycle.eLicenseLevel>(msg);
                        parameters.Add(licenseLevel);
                    }

                    continue;
                }

                if (parameter.ParameterType.Equals(typeof(GasolineEngine.eGasolineType)))
                {
                    GasolineEngine.eGasolineType gasolineType;
                    string msg;

                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        msg = m_ParametersNamesToMsg[parameter.Name];
                        gasolineType = (GasolineEngine.eGasolineType)getEnumSelectionIdentifierFromUser<GasolineEngine.eGasolineType>(msg);
                        parameters.Add(gasolineType);
                    }

                    continue;
                }
                // $G$ DSN-001 (-20) The UI must not know specific types and their properties concretely! It means that when adding a new type you'll have to change the code here too!
                if (parameter.ParameterType.Equals(typeof(Car.eCarColor)))
                {
                    Car.eCarColor carColor;
                    string msg;

                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        msg = m_ParametersNamesToMsg[parameter.Name];
                        carColor = (Car.eCarColor)getEnumSelectionIdentifierFromUser<Car.eCarColor>(msg);
                        parameters.Add(carColor);
                    }

                    continue;
                }

                if (parameter.ParameterType.Equals(typeof(Car.eDoorsAmount)))
                {
                    Car.eDoorsAmount carDoorsAmount;
                    string msg;

                    if (m_ParametersNamesToMsg.ContainsKey(parameter.Name))
                    {
                        msg = m_ParametersNamesToMsg[parameter.Name];
                        carDoorsAmount = (Car.eDoorsAmount)getEnumSelectionIdentifierFromUser<Car.eDoorsAmount>(msg);
                        parameters.Add(carDoorsAmount);
                    }

                    continue;
                }
            }

            createdVehicle = VehicleFactory.CreateVehicle(
                vehicleType,
                parameters[0].ToString(),
                parameters[1].ToString(),
                parameters[2].ToString(),
                (float)parameters[3],
                parameters.GetRange(amountOfVehicleBaseConstructorParameters, parameters.Count - amountOfVehicleBaseConstructorParameters).ToArray());

            m_GarageLogic.AddVehicle(ownerName, ownerPhone, createdVehicle);
            Console.WriteLine();
        }

        private struct Range
        {
            private readonly float m_Min;
            private readonly float m_Max;

            public Range(float i_Min, float i_Max)
            {
                m_Min = i_Min;
                m_Max = i_Max;
            }

            public float Min => m_Min;

            public float Max => m_Max;
        }
    } 
}
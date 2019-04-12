using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Sweden
// TODO: Pre-1973 plates, Diplomatic plates
namespace plate.wtf.Core.Plates
{
    public class SePlate : ISePlate
    {
        private static string Standard1973Regex = @"^(([A-Z]{3})([0-9]{2})([A-Z0-9]{1}))$";
        private static string Military1906Regex = @"^(([0-9]{4,6}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1973Regex))
            {
                plateReturn.Info = Parse1973Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Military1906Regex))
            {
                plateReturn.Info = ParseMilitary1906Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "se",
                Flag = "🇸🇪",
                Letter = "S",
                Name = "Sweden"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo Parse1973Plate(string plate)
        {
            Regex regex = new Regex(Standard1973Regex);
            Match match = regex.Match(plate);

            string inspectionPeriod = match.Groups[4].Value;

            string inspectionPeriodString = GetInspectionPeriodCode(inspectionPeriod);
            string specialString = "No";

            if(match.Groups[2].Value == "MLB")
            {
                specialString = "Fictitious/Example";
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Se_Standard1973;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                InspectionPeriod = inspectionPeriodString,
                Special = specialString
            };

            return returnModel;
        }

        private static PlateInfo ParseMilitary1906Plate(string plate)
        {
            Regex regex = new Regex(Standard1973Regex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Se_Military1906;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static string GetInspectionPeriodCode(string code)
        {
            if(InspectionPeriodCodes.ContainsKey(code))
            {
                string decodedInspectionPeriod;
                InspectionPeriodCodes.TryGetValue(code, out decodedInspectionPeriod);
                return decodedInspectionPeriod;  
            }
            else
            {
                return "Unknown";
            }
        }

        private static Dictionary<string, string> InspectionPeriodCodes = new Dictionary<string, string>()
        {
            {"1", "November–March"},
            {"2", "December–April"},
            {"3", "January–May"},
            {"4", "February–June"},
            {"5", "May–September"},
            {"6", "June–October"},
            {"7", "July–November"},
            {"8", "August–December"},
            {"9", "September–January"},
            {"0", "October–February"}
        };
    }
}
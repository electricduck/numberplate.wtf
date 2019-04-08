using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Poland
namespace plate.wtf.Core.Plates
{
    public class PlPlate : IPlPlate
    {
        private static string Standard2000Regex = @"^([A-Z]{2,3})(\d{1,5}|\d{3,4}[A-Z]{1,}|\d{1,}[A-Z]{1,}\d{1,}|[A-Z]{1,}\d{1,}|\d{2}[A-Z]{2})$";
        private static string Standard2000ReducedRegex = @"^([A-Z]{1})(\d{1,3}|\d{1,3}[A-Z]{1,}|\d{1,}[A-Z]{1,}\d{1,}|[A-Z]{1,}\d{1,}|[A-Z]{1}\d{1}[A-Z]{1})$";
        private static string Classic2000Regex = @"^([A-Z]{2,3})(\d{1,2}[A-Z]{1})$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard2000Regex))
            {
                plateReturn.Info = Parse2000Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Standard2000ReducedRegex))
            {
                plateReturn.Info = Parse2000Plate(plate, reducedSize: true);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Classic2000Regex))
            {
                plateReturn.Info = ParseClassic2000Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "pl",
                Flag = "🇵🇱",
                Letter = "PL",
                Name = "Poland"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo Parse2000Plate(string plate, bool reducedSize = false)
        {
            Regex regex = new Regex(Standard2000Regex);
            Match match = regex.Match(plate);

            string region = match.Groups[1].Value;

            string regionString = GetRegionCode(region);
            string specialString = "No";

            if(reducedSize)
            {
                specialString = "Reduced size (US-style)";
            }
        
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Pl_Standard2000;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString,
                Special = specialString
            };

            return returnModel;
        }

        private static PlateInfo ParseClassic2000Plate(string plate, bool reducedSize = false)
        {
            Regex regex = new Regex(Classic2000Regex);
            Match match = regex.Match(plate);

            string region = match.Groups[1].Value;

            string regionString = GetRegionCode(region);
        
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Pl_Classic2000;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString
            };

            return returnModel;
        }

        private static string GetRegionCode(string code)
        {
            if(RegionCodes.ContainsKey(code))
            {
                string decodedRegion;
                RegionCodes.TryGetValue(code, out decodedRegion);
                return decodedRegion;  
            }
            else
            {
                return "Unknown";
            }
        }

        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"B", "Podlaskie Voviodeship"},
            {"C", "Kuyavian-Pomeranian Voviodeship"},
            {"D", "Lower Silesian Voviodeship"},
            {"E", "Łódź Voviodeship"},
            {"F", "Lubusz Voviodeship"},
            {"G", "Pommerania Voviodeship"},
            {"K", "Lesser Poland Voviodeship"},
            {"L", "Lublin Voviodeship"},
            {"N", "Warmian-Masurian Voviodeship"},
            {"O", "Opole Voviodeship"},
            {"P", "Greater Poland Voviodeship"},
            {"R", "Subcarpathian Voviodeship"},
            {"S", "Silesian Voviodeship"},
            {"T", "Świętokrzyskie Voviodeship"},
            {"W", "Masovia Voviodeship"},
            {"Z", "West Pomerania Voviodeship"},
            {"RBI", "Bieszczadzki County"},
            {"SZ", "Zabrze"}
        };
    }
}
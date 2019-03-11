using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Finland
// TODO: Åland islands
namespace plate.wtf.Core.Plates
{
    public class FiPlate : IFiPlate
    {
        private static string Standard1960Regex = @"^(([A-ZÄÖ]{1})([A-ZÄÖ]{2})-([0-9]{2}))$";
        private static string Standard1972Regex = @"^(([A-ZÄÖ]{2,3})([-]{0,1})([0-9]{3}))$";
        private static string ExportRegex = @"^(([A-ZÄÖ]{1})-([0-9]{3,4}))$";
        private static string DiplomaticRegex = @"^(C([D]{0,1})([-]{0,1})([0-9A-Z]{2,5}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1960Regex))
            {
                plateReturn.Info = ParseStandard1960Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Standard1972Regex))
            {
                plateReturn.Info = ParseStandard1972Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, ExportRegex))
            {
                plateReturn.Info = ParseExportPlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, DiplomaticRegex))
            {
                plateReturn.Info = ParseDiplomaticPlate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "fi",
                Flag = "🇫🇮",
                Letter = "FIN",
                Name = "Finland"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        public static PlateInfo ParseStandard1960Plate(string plate)
        {
            Regex regex = new Regex(Standard1960Regex);
            Match match = regex.Match(plate);

            string issue = match.Groups[4].Value;
            string region = match.Groups[2].Value;
            string series = match.Groups[3].Value;

            int issueInt = Convert.ToInt32(issue);
            string regionString = GetPre1972RegionCode(region);
            string seriesString = series;

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Fi_Standard1960;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issueInt,
                Region = regionString,
                Series = seriesString
            };

            return returnModel;
        }

        public static PlateInfo ParseStandard1972Plate(string plate)
        {
            Regex regex = new Regex(Standard1972Regex);
            Match match = regex.Match(plate);

            string issue = match.Groups[4].Value;
            string series = match.Groups[2].Value;

            int issueInt = Convert.ToInt32(issue);
            string seriesString = series;
            string vehicleTypeString = Get1972VehicleType(plate[0], series);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Fi_Standard1972;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issueInt,
                Series = seriesString,
                VehicleType = vehicleTypeString
            };

            return returnModel;
        }

        public static PlateInfo ParseExportPlate(string plate)
        {
            Regex regex = new Regex(ExportRegex);
            Match match = regex.Match(plate);

            string issue = match.Groups[3].Value;
            string series = match.Groups[2].Value;

            int issueInt = Convert.ToInt32(issue);
            string seriesString = series;

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Fi_Export;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issueInt,
                Series = series
            };

            return returnModel;
        }

        public static PlateInfo ParseDiplomaticPlate(string plate)
        {
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Fi_Diplomatic;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static string GetPre1972RegionCode(string code)
        {
            if(Pre1972RegionCodes.ContainsKey(code))
            {
                string decodedRegion;
                Pre1972RegionCodes.TryGetValue(code, out decodedRegion);
                return decodedRegion;
            }
            else
            {
                return null;
            }
        }

        private static string Get1972VehicleType(char code, string firstLetters)
        {
            if(firstLetters.Length == 2)
            {
                return "Motorcycle";
            }
            else
            {
                switch(code)
                {
                    case 'D':
                    case 'P':
                    case 'W':
                        return "Trailer";
                    default:
                        return null;
                }
            }
        }

        private static Dictionary<string, string> Pre1972RegionCodes = new Dictionary<string, string>()
        {
            {"A", "Helsinki city"},
            {"B", "Helsinki"},
            {"C", "Helsinki"},
            {"E", "Turku"},
            {"F", "Turku"},
            {"G", "Kymi"},
            {"I", "Häme"},
            {"K", "Kuopio"},
            {"L", "Lappi"},
            {"M", "Mikkeli"},
            {"N", "Häme (until 1961)"},
            {"O", "Oulu"},
            {"R", "Kymi"},
            {"S", "Pohjois-Karjala"},
            {"T", "Turku"},
            {"U", "Uusimaa"},
            {"V", "Vaasa"},
            {"X", "Keski-Suomi"},
            {"Y", "Vaasa"},
            {"Z", "Uusimaa"}
        };
    }
}
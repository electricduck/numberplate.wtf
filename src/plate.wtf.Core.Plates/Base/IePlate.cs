using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_the_Republic_of_Ireland
// TODO: ZZ and ZV pre-1987 plates
//       Pre-1987 standard plates
namespace plate.wtf.Core.Plates
{
    public class IePlate : IIePlate
    {
        private static string Standard1987Regex = @"^(([0-9]{2})([0-9]{0,1})-([A-Z]{1,2})([-]{0,1})([0-9]{1,6}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1987Regex))
            {
                plateReturn.Info = Parse1987Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "ie",
                Flag = "🇮🇪",
                Letter = "IRL",
                Name = "Republic of Ireland"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo Parse1987Plate(string plate, bool reducedSize = false)
        {
            Regex regex = new Regex(Standard1987Regex);
            Match match = regex.Match(plate);

            int month = String.IsNullOrEmpty(match.Groups[3].Value) == false ? Convert.ToInt32(match.Groups[3].Value) : 0;
            string region = match.Groups[4].Value;
            int issue = Convert.ToInt32(match.Groups[6].Value);
            int year = Convert.ToInt32(match.Groups[2].Value);

            string regionString = GetRegionCode(region);
            string yearString = GetYearCode(year, month);
        
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Ie_Standard1987;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issue,
                Region = regionString,
                RegistrationYear = yearString
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

        private static string GetYearCode(int year, int month)
        {
            string paddedYear = year.ToString("00");

            if(year >= 87 && year <= 99)
            {
                return $"19{year}";
            }

            if(year >= 00 && year <= 12)
            {
                return $"20{paddedYear}";
            }

            if(year >= 13 && year <= 88)
            {
                if(month == 1)
                {
                    return $"20{paddedYear} (Jan-Jun)";
                }
                else if(month == 2)
                {
                    return $"20{paddedYear} (Jul-Dec)";
                }
            }

            return "Unknown";
        }

        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"C", "Cork"},
            {"CE", "Clare"},
            {"CN", "Cavan"},
            {"CW", "Carlow"},
            {"D", "Dublin"},
            {"DL", "Donegal"},
            {"G", "Galway"},
            {"KE", "Kildare"},
            {"KK", "Kilkenny"},
            {"KY", "Kerry"},
            {"L", "Limerick"},
            {"LD", "Longford"},
            {"LH", "Louth"},
            {"LK", "County Limerick"},
            {"LM", "Leitrim"},
            {"LS", "Laois"},
            {"MH", "Meath"},
            {"MN", "Monaghan"},
            {"MO", "Mayo"},
            {"OY", "Offaly"},
            {"RN", "Roscommon"},
            {"SO", "Sligo"},
            {"T", "Tipperary"},
            {"TN", "North Tipperary"},
            {"TS", "South Tipperary"},
            {"W", "Waterford"},
            {"WD", "County Waterford"},
            {"WH", "Westmeath"},
            {"WX", "Wexford"},
            {"WW", "Wicklow"}
        };
    }
}
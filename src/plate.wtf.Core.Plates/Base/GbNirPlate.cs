using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class GbNirPlate : IGbNirPlate
    {
        private static string Standard1966Regex = @"^(([A-Z]{1})([A-Z]{2})([0-9]{1,4}))$";
        
        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1966Regex))
            {
                plateReturn.Info = ParseStandard1966Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "gb-nir",
                Flag = "🇬🇧",
                Letter = "GB",
                Name = "Northern Ireland"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandard1966Plate(string plate)
        {
            Regex regex = new Regex(Standard1966Regex);
            Match match = regex.Match(plate);

            string issue = match.Groups[4].Value;
            string region = match.Groups[3].Value;
            string special = match.Groups[2].Value;

            int issueInt = Convert.ToInt32(issue);
            string regionString = null;
            string specialString = null;

            if(special == "L" && region == "TZ")
            {
                specialString = "NI-built bus, intended for TfL";
            }
            else if(special == "Q" && region == "NI")
            {
                specialString = "Indeterminate age, or kit car";
            }
            else
            {
                regionString = GetRegionCode(region);
            }
            
            Enums.PlateFormat plateFormat = Enums.PlateFormat.GbNir_Standard1966;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issueInt,
                Region = regionString,
                Special = specialString
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
                return null;
            }
        }

        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"AZ", "Belfast"},
            {"BZ", "Down"},
            {"CZ", "Belfast"},
            {"DZ", "Antrim"},
            {"EZ", "Belfast"},
            {"FZ", "Belfast"},
            {"GZ", "Belfast"},
            {"HZ", "Tyrone"},
            {"IA", "Antrim"},
            {"IB", "Armagh"},
            {"IG", "Farmanagh"},
            {"IJ", "Down"},
            {"IL", "Fermanagh"},
            {"IW", "County Londonderry"},
            {"JI", "Tyrone"},
            {"JZ", "Down"},
            {"KZ", "Antrim"},
            {"LZ", "Armagh"},
            {"MZ", "Belfast"},
            {"NZ", "County Londonderry"},
            {"OI", "Belfast"},
            {"OZ", "Belfast"},
            {"PZ", "Belfast"},
            {"RZ", "Antrim"},
            {"SZ", "Down"},
            {"TZ", "Belfast"},
            {"UI", "Derry"},
            {"VZ", "Tyrone"},
            {"UZ", "Belfast"},
            {"WZ", "Belfast"},
            {"XI", "Belfast"},
            {"XZ", "Armagh"},
            {"YZ", "County Londonderry"}
        };
    }

}

// REFERENCES:
//  - https://www.tripsavvy.com/northern-irish-numberplates-1541604
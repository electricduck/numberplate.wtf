using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class NoPlate : INoPlate
    {
        private static string Standard1971Regex = @"^(([A-Z]{2})([-]{0,1})([0-9]{5}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1971Regex))
            {
                plateReturn.Info = ParseStandard1971Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "no",
                Flag = "🇳🇴"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandard1971Plate(string plate)
        {
            Regex regex = new Regex(Standard1971Regex);
            Match match = regex.Match(plate);

            int issue = Convert.ToInt32(match.Groups[4].Value);
            string regionOrSpecial = match.Groups[2].Value;

            string regionString = GetRegionCode(regionOrSpecial);
            string specialString = GetSpecialCode(regionOrSpecial);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.No_Standard1971;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = (issue-9999),
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

        private static string GetSpecialCode(string code)
        {
            if(SpecialCodes.ContainsKey(code))
            {
                string decodedSpecial;
                SpecialCodes.TryGetValue(code, out decodedSpecial);
                return decodedSpecial;  
            }
            else
            {
                return null;
            }
        }

        // TODO: Finish this
        // SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Norway
        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"AA", "Halden, Østfold"},
            {"AB", "Halden, Østfold"},
            {"AC", "Halden, Østfold"},
            {"AD", "Sarpsborg, Østfold"},
            {"AE", "Sarpsborg, Østfold"},
            {"AF", "Fredrikstad, Østfold"},
            {"AH", "Fredrikstad, Østfold"},
            {"AR", "Fredrikstad, Østfold"},
            {"AS", "Fredrikstad, Østfold"},
            {"AT", "Fredrikstad, Østfold"},
            {"AU", "Fredrikstad, Østfold"},
            {"AV", "Fredrikstad, Østfold"},
            {"AW", "Fredrikstad, Østfold"},
            {"DW", "Fredrikstad, Østfold"},
        };

        private static Dictionary<string, string> SpecialCodes = new Dictionary<string, string>()
        {
            {"CD", "Corps Diplomatique"},
            {"EL", "Electric Vehicle"},
            {"EK", "Electric Vehicle"},
            {"EV", "Electric Vehicle"},
            {"FE", "Former Military Vehicle (Civ.)"},
            {"GA", "LPG/AutoGas Vehicle"},
            {"HY", "Hydrogen Vehicle"},
        };
    }
}
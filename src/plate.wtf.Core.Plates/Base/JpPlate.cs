using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class JpPlate : IJpPlate
    {
        private static string Standard1962Regex = @"^(([\u4E00-\u9FFF]{1,4})([0-9]{2,3})?([\u3040-\u309Fー]{1})([0-9]{1,2})([-]{0,1})([0-9]{1,2}))$";
        private static string Export1962Regex = @"^(([A-Z]{2,3})([0-9]{2,3})([A-Z]{1,3})([0-9]{1,2})([-]{0,1})([0-9]{1,2}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1962Regex))
            {
                plateReturn.Info = ParseStandardOrExport1962Plate(plate, false);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Export1962Regex))
            {
                plateReturn.Info = ParseStandardOrExport1962Plate(plate, true);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "jp",
                Flag = "🇯🇵",
                Letter = "J",
                Name = "Japan"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandardOrExport1962Plate(string plate, bool export)
        {
            Regex regex = export ? new Regex(Export1962Regex) : new Regex(Standard1962Regex);
            Match match = regex.Match(plate);

            string region = match.Groups[2].Value;
            int vehicleType = 0;
            string special = match.Groups[4].Value;

            if(!String.IsNullOrEmpty(match.Groups[3].Value))
            {
                vehicleType = Convert.ToInt32(match.Groups[3].Value[0].ToString());
            }

            string regionString = GetRegionCode(region);
            string vehicleTypeString = GetVehicleCode(vehicleType);
            string specialString = GetSpecialCode(special);

            Enums.PlateFormat plateFormat = export ? Enums.PlateFormat.Jp_Export1962 : Enums.PlateFormat.Jp_Standard1962;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString,
                Special = specialString,
                VehicleType = vehicleTypeString
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

        private static string GetVehicleCode(int code)
        {
            if(VehicleCodes.ContainsKey(code))
            {
                string decodedVehicle;
                VehicleCodes.TryGetValue(code, out decodedVehicle);
                return decodedVehicle;
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"尾張小牧", "Komaki, Aichi"},  {"ACO", "Komaki, Aichi"},
            {"一宮", "Komaki, Aichi"},      {"ACI", "Komaki, Aichi"},
            {"春日井", "Komaki, Aichi"},    {"ACK", "Komaki, Aichi"},
            {"名古屋", "Nagoya, Aichi"},    {"ACN", "Nagoya, Aichi"},
            {"愛", "Nagoya, Aichi"},        {"AC", "Nagoya, Aichi"},
            {"豊橋", "Toyohashi, Aichi"},   {"ACT", "Toyohashi, Aichi"},
            {"三河", "Toyota, Aichi"},      {"ACM", "Toyota, Aicihi"},
            {"岡崎", "Toyota, Aichi"},      {"ACZ", "Toyota, Aicihi"},
            {"豊田", "Toyota, Aichi"},      {"ACY", "Toyota, Aicihi"},
            {"秋田", "Akita, Akita"},       {"ATA", "Akita, Akita"},
            {"秋", "Akita, Akita"},         {"AT", "Akita, Akita"},
            {"青森", "Aomori, Aomori"},     {"AMA", "Aomori, Aomori"},
            {"青", "Aomoro, Aomori"},       {"AM", "Aomori, Aomori"},
            {"八戸", "Hachinohe, Aomori"},  {"AMH", "Hachinohe, Aomori"},

            {"長野", "Nagano, Nagano"},     {"NNN", "Nagano, Nagano"},  {"野長", "Nagano, Nagano"},
            {"長", "Nagano, Nagano"},       {"NN", "Nagano, Nagano"}
        };

        private static Dictionary<string, string> SpecialCodes = new Dictionary<string, string>()
        {
            {"わ", "Rental"},           {"WA", "Rental"},
            {"れ", "Rental"},           {"RE", "Rental"},
            {"よ", "Retired Military"}, {"YO", "Retired Military"}
        };

        private static Dictionary<int, string> VehicleCodes = new Dictionary<int, string>()
        {
            {0, "Motorbike"},
            {1, "Trucks (2000cc or above)"},
            {2, "Buses"},
            {3, "Cars (2000cc or above)"},
            {4, "Truck/Van (6660c to 2000cc)"},
            {5, "Car (660cc to 2000cc)"},
            {6, "Truck (360cc or below)"},
            {7, "Three-wheeler / Car (660cc to 2000cc)"},
            {8, "Special vehicle"},
            {9, "Tractor/Forklift"},
            {10, "Construction equipment"}
        };
    }
}
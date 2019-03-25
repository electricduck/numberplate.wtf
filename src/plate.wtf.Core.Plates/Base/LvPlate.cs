using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Latvia
//      http://www.worldlicenseplates.com/jpglps/EU_LATV_OT.jpg
// TODO: 1991-1993 (after Latvia gained independence from the Soviet Union)

namespace plate.wtf.Core.Plates
{
    public class LvPlate : ILvPlate
    {
        private static string Standard1993Regex = @"^(([A-Z]{1,2})([-]{0,1})([0-9]{4}))";
        private static string Diplomatic1993Regex = @"^((\b(?:C|D|CD))([-]{0,1})([0-9]{4}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Diplomatic1993Regex))
            {
                plateReturn.Info = ParseDiplomatic1993Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Standard1993Regex))
            {
                plateReturn.Info = Parse1993Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "lv",
                Flag = "🇱🇻",
                Letter = "LV",
                Name = "Latvia"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo Parse1993Plate(string plate)
        {
            Regex regex = new Regex(Standard1993Regex);
            Match match = regex.Match(plate);

            int issue = Convert.ToInt32(match.Groups[4].Value);
            string stringSeriesOrSpecialOrVehicleType = match.Groups[2].Value;

            int issueInt = issue;
            string seriesString = null;
            string specialString = GetSpecialCode(stringSeriesOrSpecialOrVehicleType);
            string vehicleTypeString = GetVehicleTypeCode(stringSeriesOrSpecialOrVehicleType);

            if(stringSeriesOrSpecialOrVehicleType.Length == 1)
            {
                seriesString = null;
                vehicleTypeString = "Trailer";
            }
            else
            {
                if(vehicleTypeString == "Motorbike")
                {
                    seriesString = stringSeriesOrSpecialOrVehicleType[1].ToString();
                }
                else
                {
                    if(specialString == null && vehicleTypeString == null)
                    {
                        seriesString = stringSeriesOrSpecialOrVehicleType;
                    }
                }
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lv_Standard1993;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issueInt,
                Series = seriesString,
                Special = specialString,
                VehicleType = vehicleTypeString
            };

            return returnModel;
        }

        private static PlateInfo ParseDiplomatic1993Plate(string plate)
        {
            Regex regex = new Regex(Standard1993Regex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lv_Diplomatic1993;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static string GetSpecialCode(string code)
        {
            if(SpecialCodes.ContainsKey(code))
            {
                string decodedSpecialCode;
                SpecialCodes.TryGetValue(code, out decodedSpecialCode);
                return decodedSpecialCode;  
            }
            else
            {
                return "No";
            }
        }

        private static string GetVehicleTypeCode(string code)
        {
            if(VehicleTypeCodes.ContainsKey(code))
            {
                string decodedVehicleType;
                VehicleTypeCodes.TryGetValue(code, out decodedVehicleType);
                return decodedVehicleType;  
            }
            else
            {
                return "Unknown";
            }
        }

        private static Dictionary<string, string> SpecialCodes = new Dictionary<string, string>()
        {
            {"EX", "Electric Vehicle"},
            {"TQ", "Taxi"},
            {"TX", "Taxi"},
            {"VS", "Historic Vehicle"}
        };

        private static Dictionary<string, string> VehicleTypeCodes = new Dictionary<string, string>()
        {
            {"TA", "Motorbike"},
            {"TB", "Motorbike"},
            {"TC", "Motorbike"}
        };
    }
}
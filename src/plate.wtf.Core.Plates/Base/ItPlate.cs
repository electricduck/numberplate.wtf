using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Italy
namespace plate.wtf.Core.Plates
{
    public class ItPlate : IItPlate
    {
        private static string Standard1994Regex = @"^(([A-Z]{2})([-]{0,1})([0-9]{3})([A-Z]{2})([A-Z]{0,2}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1994Regex))
            {
                plateReturn.Info = ParseStandard1994Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "it",
                Flag = "🇮🇹",
                Letter = "I",
                Name = "Italy"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandard1994Plate(string plate)
        {
            Regex regex = new Regex(Standard1994Regex);
            Match match = regex.Match(plate);

            string region = match.Groups[6].Value;

            string regionString = GetPost1927RegionCode(region);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.It_Standard1994;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString
            };

            return returnModel;
        }

        private static string GetPost1927RegionCode(string code)
        {
            if(Post1927RegionCodes.ContainsKey(code))
            {
                string decodedRegion;
                Post1927RegionCodes.TryGetValue(code, out decodedRegion);
                return decodedRegion;
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<string, string> Post1927RegionCodes = new Dictionary<string, string>()
        {
            {"AG", "Agrigento"},
            {"AL", "Alessandria"},
            {"AN", "Ancona"},
            {"AO", "Aosta / Aoste"},
            {"AP", "Ascoli Piceno"},
            {"AQ", "L'Aquila"},
            {"AR", "Arezzo"},
            {"AT", "Asti"},
            {"AV", "Avellino"}
        };
    }
}
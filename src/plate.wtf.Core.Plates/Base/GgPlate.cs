using System;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class GgPlate : IGgPlate
    {
        private static string Standard1908Regex = @"^(([0-9]{1,5}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1908Regex))
            {
                plateReturn.Info = ParseStandard1908Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "gg",
                Flag = "🇬🇬"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandard1908Plate(string plate)
        {
            int issue = Convert.ToInt32(plate);
            string specialString = null;

            if(issue == 1)
            {
                specialString = "Bailiff of Guernsey";
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Gg_Standard1908;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issue,
                Special = specialString
            };

            return returnModel;
        }
    }
}
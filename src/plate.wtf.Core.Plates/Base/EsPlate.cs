using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class EsPlate : IEsPlate
    {
        private static string Standard2000Regex = @"^(([0-9]{4})([B|C|D|F|G|H|J|K|L|M|N|P|R|S|T|V|W|X|Y]{3})([C|E|H|P|R|S|T|V]{0,1}))$";
        private static string Standard1971Regex = @"^(([A-Z]{1,3})-([0-9]{4})-([A-Z]{1,2}))$";
        private static string Standard1900Regex = @"^(([A-Z]{1,3})-([0-9]{1,6}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard2000Regex))
            {
                plateReturn.Info = ParseStandard2000Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "es",
                Flag = "🇪🇸"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandard2000Plate(string plate)
        {
            Regex regex = new Regex(Standard2000Regex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Es_Standard2000;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }
    }
}
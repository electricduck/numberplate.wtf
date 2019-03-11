using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Hungary
namespace plate.wtf.Core.Plates
{
    public class HuPlate : IHuPlate
    {
        private static string Standard1990Regex = @"^(([A-Z]{1})([A-Z]{2})-([0-9]{3}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1990Regex))
            {
                plateReturn.Info = Parse1990Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "hu",
                Flag = "🇭🇺",
                Letter = "H",
                Name = "Hungary"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo Parse1990Plate(string plate)
        {
            Regex regex = new Regex(Standard1990Regex);
            Match match = regex.Match(plate);

            string registrationYear = match.Groups[2].Value;

            string registrationYearString = GetRegistrationYearCode(registrationYear[0]);
        
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Hu_Standard1990;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = registrationYearString
            };

            return returnModel;
        }

        private static string GetRegistrationYearCode(char code)
        {
            if(RegistrationYearCodes.ContainsKey(code))
            {
                string decodedRegistrationYear;
                RegistrationYearCodes.TryGetValue(code, out decodedRegistrationYear);
                return decodedRegistrationYear;  
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<char, string> RegistrationYearCodes = new Dictionary<char, string>()
        {
            {'A', "1990"},
            {'B', "1991"},
            {'C', "1991"},
            {'D', "1992"},
            {'E', "1993"},
            {'F', "1994-1995"},
            {'G', "1996-1998"},
            {'H', "1999-2000"},
            {'I', "2001-2002"},
            {'J', "2003-2004"},
            {'K', "2005-2006"},
            {'L', "2007-2010"},
            {'M', "2011-2013"},
            {'N', "2014-2015"},
            {'P', "2016-2018"},
            {'R', "2018"},
            {'S', "2020"}
        };
    }
}
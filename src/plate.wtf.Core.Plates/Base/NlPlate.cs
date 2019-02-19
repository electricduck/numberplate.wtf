using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class NlPlate : INlPlate
    {
        private static string Y1898Regex = @"^(([A|B|D|E|G|H|K|M|N|L|P]{1})([Z|X]{0,1})([0-9]{1,5}))$";
        private static string SideCode1Regex = @"^(([A-Z]{2})-([0-9]{2})-([0-9]{2}))$";
        private static string SideCode2Regex = @"^(([0-9]{2})-([0-9]{2})-([A-Z]{2}))$";
        private static string SideCode3Regex = @"^(([0-9]{2})-([A-Z]{2})-([0-9]{2}))$";
        private static string SideCode4Regex = @"^(([A-Z]{2})-([0-9]{2})-([A-Z]{2}))$";
        private static string SideCode5Regex = @"^(([A-Z]{2})-([A-Z]{2})-([0-9]{2}))$";
        private static string SideCode6Regex = @"^(([0-9]{2})-([A-Z]{1})([A-Z]{1})-([A-Z]{2}))$";
        private static string SideCode7Regex = @"^(([0-9]{2})-([A-Z]{1})([A-Z]{2})-([0-9]{1}))$";
        private static string SideCode8Regex = @"^(([0-9]{1})-([A-Z]{1})([A-Z]{2})-([0-9]{2}))$";
        private static string SideCode9Regex = @"^(([A-Z]{1})([A-Z]{1})-([0-9]{3})-([A-Z]{1}))$";
        private static string SideCode10Regex = @"^(([A-Z]{1})-([0-9]{3})-([A-Z]{2}))$";
        private static string SideCode11Regex = @"^(([A-Z]{1})([A-Z]{2})-([0-9]{2})-([A-Z]{1}))$";
    
        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Y1898Regex))
            {
                plateReturn.Info = ParseY1898Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode1Regex))
            {
                plateReturn.Info = ParseSideCode1(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode2Regex))
            {
                plateReturn.Info = ParseSideCode2(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode3Regex))
            {
                plateReturn.Info = ParseSideCode3(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode4Regex))
            {
                plateReturn.Info = ParseSideCode4(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode5Regex))
            {
                plateReturn.Info = ParseSideCode5(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode6Regex))
            {
                plateReturn.Info = ParseSideCode6(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode7Regex))
            {
                plateReturn.Info = ParseSideCode7(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode8Regex))
            {
                plateReturn.Info = ParseSideCode8(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode9Regex))
            {
                plateReturn.Info = ParseSideCode9(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode10Regex))
            {
                plateReturn.Info = ParseSideCode10(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SideCode11Regex))
            {
                plateReturn.Info = ParseSideCode11(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "nl",
                Flag = "🇳🇱",
                Letter = "NL",
                Name = "Netherlands"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseY1898Plate(string plate)
        {
            Regex regex = new Regex(Y1898Regex);
            Match match = regex.Match(plate);

            string region = match.Groups[2].Value;

            string regionString;

            switch(region) {
                case "A":
                    regionString = "Groningen";
                    break;
                case "B":
                    regionString = "Friesland";
                    break;
                case "D":
                    regionString = "Drenthe";
                    break;
                case "E":
                    regionString = "Overijssel";
                    break;
                case "G":
                    regionString = "Noord Holland";
                    break;
                case "H":
                    regionString = "Zuid Holland";
                    break;
                case "K":
                    regionString = "Zeeland";
                    break;
                case "M":
                    regionString = "Gelderland";
                    break;
                case "N":
                    regionString = "Noord Brabant";
                    break;
                case "L":
                    regionString = "Utrecht";
                    break;
                case "P":
                    regionString = "Limburg";
                    break;
                default:
                    regionString = null;
                    break;
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_1898;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString
            };

            return returnModel;
        }
    
        private static PlateInfo ParseSideCode1(string plate)
        {
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode1;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode2(string plate)
        {
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode2;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode3(string plate)
        {
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode3;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode4(string plate)
        {
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode4;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode5(string plate)
        {
            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode5;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode6(string plate)
        {
            Regex regex = new Regex(SideCode6Regex);
            Match match = regex.Match(plate);

            string year = match.Groups[3].Value;

            string yearString;

            switch(year) {
                case "D":
                    yearString = "1999/2000";
                    break;
                case "F":
                    yearString = "2000";
                    break;
                case "G":
                    yearString = "2000/2001";
                    break;
                case "H":
                    yearString = "2001/2002";
                    break;
                case "J":
                    yearString = "2002";
                    break;
                case "L":
                    yearString = "2002/2003";
                    break;
                case "N":
                    yearString = "2003/2004";
                    break;
                case "P":
                    yearString = "2004/2005";
                    break;
                case "R":
                    yearString = "2005";
                    break;
                case "S":
                    yearString = "2005/2006";
                    break;
                case "T":
                    yearString = "2006/2007";
                    break;
                case "X":
                    yearString = "2007";
                    break;
                case "Z":
                    yearString = "2007/2008";
                    break;
                case "M":
                    yearString = "2011";
                    break;
                case "V":
                    yearString = "1998-2001";
                    break;
                case "B":
                    yearString = "2001-2006";
                    break;
                case "W":
                    yearString = "2008";
                    break;
                default:
                    yearString = null;
                    break;
            };

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode6;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = yearString
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode7(string plate)
        {
            Regex regex = new Regex(SideCode7Regex);
            Match match = regex.Match(plate);

            string year = match.Groups[3].Value;

            string yearString;

            switch(year) {
                case "G":
                    yearString = "2008";
                    break;
                case "H":
                    yearString = "2008/2009";
                    break;
                case "J":
                    yearString = "2009";
                    break;
                case "K":
                    yearString = "2009/2010";
                    break;
                case "L":
                    yearString = "2010";
                    break;
                case "N":
                    yearString = "2010/2011";
                    break;
                case "P":
                case "R":
                case "S":
                    yearString = "2011";
                    break;
                case "T":
                case "X":
                    yearString = "2012";
                    break;
                case "Z":
                    yearString = "2012/2013";
                    break;
                case "V":
                    yearString = "2006-2009";
                    break;
                case "B":
                    yearString = "2012";
                    break;
                case "D":
                    yearString = "2005/2006";
                    break;
                case "F":
                    yearString = "2006";
                    break;
                default:
                    yearString = null;
                    break;
            };

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode7;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = yearString
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode8(string plate)
        {
            Regex regex = new Regex(SideCode8Regex);
            Match match = regex.Match(plate);

            string year = match.Groups[3].Value;

            string specialString = null;
            string yearString = null;

            switch(year) {
                case "K":
                case "S":
                    yearString = "2013";
                    break;
                case "T":
                    yearString = "2013/2014";
                    break;
                case "X":
                    yearString = "2014";
                    break;
                case "Z":
                    yearString = "2014/2015";
                    break;
                case "V":
                    yearString = "2009-2012";
                    break;
                default:
                    specialString = "Export";
                    break;
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode8;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = yearString,
                Special = specialString
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode9(string plate)
        {
            Regex regex = new Regex(SideCode9Regex);
            Match match = regex.Match(plate);

            string year = match.Groups[2].Value;

            string yearString = null;

            switch(year) {
                case "G":
                    yearString = "2015";
                    break;
                case "H":
                    yearString = "2015/2016";
                    break;
                case "J":
                case "K":
                    yearString = "2015";
                    break;
                case "N":
                    yearString = "2016/2017";
                    break;
                case "P":
                    yearString = "2017";
                    break;
                case "R":
                    yearString = "2017/2018";
                    break;
                case "S":
                case "T":
                    yearString = "2018";
                    break;
                case "X":
                    yearString = "2018/2019";
                    break;
                case "Z":
                    yearString = "2019";
                    break;
                case "V":
                    yearString = "2012-2016";
                    break;
                case "D":
                    yearString = "2006";
                    break;
                case "F":
                    yearString = "2006-2008";
                    break;
                default:
                    yearString = null;
                    break;
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode9;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = yearString
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode10(string plate)
        {
            Regex regex = new Regex(SideCode10Regex);
            Match match = regex.Match(plate);

            string year = match.Groups[2].Value;

            string yearString = null;

            switch(year) {
                case "D":
                    yearString = "2008-2011";
                    break;
                case "F":
                    yearString = "2011-2015";
                    break;
                default:
                    yearString = null;
                    break;
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode10;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = yearString
            };

            return returnModel;
        }

        private static PlateInfo ParseSideCode11(string plate)
        {
            Regex regex = new Regex(SideCode11Regex);
            Match match = regex.Match(plate);

            string year = match.Groups[2].Value;

            string yearString = null;

            switch(year) {
                case "D":
                    yearString = "2015";
                    break;
                default:
                    yearString = null;
                    break;
            }

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Nl_SideCode11;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                RegistrationYear = yearString
            };

            return returnModel;
        }
    }
}
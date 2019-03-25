using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class LtPlate : ILtPlate
    {
        private static string StandardRegex = @"^(([A-Z]{1})([A-Z]{1})([A-Z]{1})([-]{0,1})([0-9]{3}))$";
        private static string ImportExportRegex = @"^(([0-9]{5})([A-Z]{2}))$";
        private static string TradeRegex = @"^(([A-Z]{1})([0-9]{6}))$";
        private static string DiplomaticRegex = @"^(([0-9]{2})([0-9]{1})([0-9]{3}))$";
        private static string TaxicabRegex = @"^(T([0-9]{5}))$";
        private static string MilitaryTransportRegex = @"^(([A-Z]{2})([0-9]{3})([A-Z]{1}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, StandardRegex))
            {
                plateReturn.Info = ParseStandardPlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, ImportExportRegex))
            {
                plateReturn.Info = ParseImportExportPlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, TradeRegex))
            {
                plateReturn.Info = ParseTradePlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, DiplomaticRegex))
            {
                plateReturn.Info = ParseDiplomaticPlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, TaxicabRegex))
            {
                plateReturn.Info = ParseTaxicabPlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, MilitaryTransportRegex))
            {
                plateReturn.Info = ParseMilitaryTransportPlate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "lt",
                Flag = "🇱🇹",
                Letter = "LT",
                Name = "Lithuania"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandardPlate(string plate)
        {
            Regex regex = new Regex(StandardRegex);
            Match match = regex.Match(plate);

            string region = match.Groups[3].Value;

            string regionString = GetRegionCode(region[0]);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lt_Standard;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString
            };

            return returnModel;
        }

        private static PlateInfo ParseImportExportPlate(string plate)
        {
            Regex regex = new Regex(ImportExportRegex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lt_ImportExport;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseTradePlate(string plate)
        {
            Regex regex = new Regex(TradeRegex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lt_Trade;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseDiplomaticPlate(string plate)
        {
            Regex regex = new Regex(DiplomaticRegex);
            Match match = regex.Match(plate);

            string diplomaticOrganisation = match.Groups[2].Value;

            string diplomaticOrganisationString = GetDiplomaticOrganisationCode(Convert.ToInt32(diplomaticOrganisation));

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lt_Diplomatic;

            PlateInfo returnModel = new PlateInfo
            {
                Diplomatic = new Diplomatic
                {
                    Organisation = diplomaticOrganisationString
                },
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseTaxicabPlate(string plate)
        {
            Regex regex = new Regex(TradeRegex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lt_Taxi;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static PlateInfo ParseMilitaryTransportPlate(string plate)
        {
            Regex regex = new Regex(TradeRegex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Lt_Military;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }

        private static string GetRegionCode(char code)
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

        private static string GetDiplomaticOrganisationCode(int code)
        {
            if(DiplomaticOrganisationCodes.ContainsKey(code))
            {
                string decodedDiplomaticOrganisation;
                DiplomaticOrganisationCodes.TryGetValue(code, out decodedDiplomaticOrganisation);
                return decodedDiplomaticOrganisation;
            }
            else
            {
                return "Unknown";
            }
        }

        private static Dictionary<char, string> RegionCodes = new Dictionary<char, string>()
        {
            {'A', "Alytus County"},
            {'J', "Tauragė County "},
            {'K', "Kaunas County"},
            {'L', "Klaipėda County"},
            {'M', "Marijampolė County"},
            {'P', "Panevėžys County"},
            {'S', "Šiauliai County"},
            {'T', "Telšiai County"},
            {'U', "Utena County"},
            {'V', "Vilnius County"}
        };

        private static Dictionary<int, string> DiplomaticOrganisationCodes = new Dictionary<int, string>()
        {
            {1, "Sweden"},
            {2, "Germany"},
            {3, "France"},
            {4, "Latvia"},
            {5, "Denmark"},
            {6, "Canada"},
            {7, "United Kingdom"},
            {8, "Italy"},
            {9, "Norway"},
            {10, "Finland"},
            {11, "Holy See"},
            {12, "Turkey"},
            {13, "Czech Republic"},
            {14, "United States"},
            {15, "China"},
            {16, "Poland"},
            {17, "Poland"},
            {18, "Estonia"},
            {19, "Russia"},
            {20, "Russia"},
            {21, "Romania"},
            {22, "Ukraine"},
            {23, "Belarus"},
            {24, "Kazakhstan"},
            {25, "Georgia"},
            {26, "Japan"},
            {27, "Austria"},
            {28, "Belgium"},
            {29, "Netherlands"},
            {30, "Hungary"},
            {31, "Spain"},
            {32, "Sovereign Military Order of Malta"},
            {33, "Democratic Republic of Congo"},
            {34, "Ireland"},
            {35, "Portugal"},
            {36, "Moldova"},
            {37, "Azerbaijan"},
            {38, "Beulgaria"},
            {39, "Armenia"},
            {41, "Israel"},
            {80, "Nordic Council of Ministers"},
            {81, "World Bank"},
            {82, "European Bank for Reconstruction and Development"},
            {83, "World Health Organization"},
            {84, "United Nations Development Program"},
            {85, "International Organization for Migration"},
            {86, "European Commission"},
            {87, "United Nations"},
            {88, "European Institute for Gender Equality"}
        };
    }
}
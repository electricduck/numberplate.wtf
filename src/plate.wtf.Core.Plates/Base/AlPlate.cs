using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Albania
//      http://www.licenseplatemania.com/landenpaginas/albanie.htm
namespace plate.wtf.Core.Plates
{
    public class AlPlate : IAlPlate
    {
        private static string Standard2011Regex = @"^(([A-Z]{2})([-]{0,1})([0-9]{3})([A-Z]{2}))$";
        private static string Standard1993Regex = @"^(([A-Z]{2})([-]{0,1})([0-9]{1,4})([-]{0,1})([A-Z]{1}))$";
        private static string Diplomatic2011Regex = @"^(CD([-]{0,1})([0-9]{2})([0-9]{2})([A-Z]{1}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Diplomatic2011Regex))
            {
                plateReturn.Info = ParseDiplomatic2011Plate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Standard2011Regex))
            {
                plateReturn.Info = Parse2011Plate(plate);
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
                Code = "al",
                Flag = "🇦🇱",
                Letter = "AL",
                Name = "Albania"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo Parse2011Plate(string plate)
        {
            Regex regex = new Regex(Standard2011Regex);
            Match match = regex.Match(plate);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Al_Standard2011;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat
            };

            return returnModel;
        }
        
        private static PlateInfo Parse1993Plate(string plate)
        {
            Regex regex = new Regex(Standard1993Regex);
            Match match = regex.Match(plate);

            string regionOrSpecial = match.Groups[2].Value;

            string regionString = GetRegionCode(regionOrSpecial);
            string specialString = GetSpecialCode(regionOrSpecial);

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Al_Standard1993;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString,
                Special = specialString
            };

            return returnModel;
        }

        private static PlateInfo ParseDiplomatic2011Plate(string plate)
        {
            Regex regex = new Regex(Diplomatic2011Regex);
            Match match = regex.Match(plate);

            var diplomaticOrganisation = match.Groups[3].Value;

            var diplomaticOrganisationString = GetDiplomaticOrganisationCode(Convert.ToInt32(diplomaticOrganisation));

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Al_Diplomatic2011;

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
            if(RegionCodes.ContainsKey(code))
            {
                string decodedSpecial;
                RegionCodes.TryGetValue(code, out decodedSpecial);
                return decodedSpecial;  
            }
            else
            {
                return null;
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
                return null;
            }
        }

        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"BC", "Tropojë"},
            {"BR", "Berat"},
            {"BZ", "Bulqizë"},
            {"DI", "Dibër"},
            {"DL", "Delvinë"},
            {"DR", "Durrës"},
            {"DV", "Devoll"},
            {"EL", "Elbasan"},
            {"ER", "Kolonjë"},
            {"FR", "Fier"},
            {"GJ", "Gjirokastër"},
            {"GR", "Gramsh"},
            {"HS", "Has"},
            {"KJ", "Kavajë"},
            {"KO", "Korçë"},
            {"KR", "Krujë"},
            {"KU", "Kukës"},
            {"KV", "Kuçovë"},
            {"LA", "Kurbin"},
            {"LB", "Librazhd"},
            {"LE", "Lezhë"},
            {"LU", "Lushnjë"},
            {"MA", "Malësi e Madhe"},
            {"MK", "Mallakastër"},
            {"MR", "Mirditë"},
            {"MT", "Mat"},
            {"PE", "Peqin"},
            {"PG", "Pogradec"},
            {"PR", "Përmet"},
            {"PU", "Pukë"},
            {"SH", "Shkodër"},
            {"SK", "Skrapar"},
            {"SR", "Sarandë"},
            {"TP", "Tepelenë"},
            {"TR", "Tirana"},
            {"VL", "Vlorë"}
        };

        private static Dictionary<string, string> SpecialCodes = new Dictionary<string, string>()
        {
            {"AA", "Taxi"},
            {"MB", "Police Car"}
        };

        private static Dictionary<int, string> DiplomaticOrganisationCodes = new Dictionary<int, string>()
        {
            {1, "Austria"},
            {2, "United Kingdom"},
            {3, "Bulgaria"},
            {5, "Czech Republic"},
            {6, "Egypt"},
            {7, "France"},
            {8, "Greece"},
            {9, "Germany"},
            {10, "Hungary"},
            {11, "Italy"},
            {12, "Iran"},
            {13, "Serbia"},
            {14, "China"},
            {15, "Croatia"},
            {17, "Macedonia"},
            {18, "Palestine"},
            {19, "Poland"},
            {20, "Romania (Honorary Consulate)"},
            {21, "Russia"},
            {23, "United States"},
            {24, "Turkey"},
            {25, "Switzerland"},
            {27, "Saudi Arabia"},
            {28, "Sweden"},
            {29, "Spain"},
            {33, "UNESCO"},
            {41, "European Union"},
            {42, "United Nations"},
            {43, "UNICEF"},
            {45, "World Health Organisation"},
            {48, "World Bank"},
            {52, "Kuwait"},
            {55, "Kosovo"},
            {56, "Netherlands"},
            {60, "Denmark"},
            {66, "Slovakia"},
            {77, "Brazil"},
            {79, "Israel"},
            {81, "Qatar"}
        };
    }
}
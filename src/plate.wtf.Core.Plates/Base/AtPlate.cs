using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

// TODO: Add support for diplomatic plates (GK, KK, SD, SK, TD, TK, VK, WD, WK)
// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Austria
namespace plate.wtf.Core.Plates
{
    public class AtPlate : IAtPlate
    {
        private static string Standard1990Regex = @"^(([A-Z]{1,2})-([A-Z0-9]{4,6}))$";
        private static string Standard1990OfficialRegex = @"^(([A-Z]{1,2})-([0-9]{1,5}))$"; // FIX: These don't work!

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, Standard1990OfficialRegex))
            {
                plateReturn.Info = ParseStandard1990Plate(plate, true);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, Standard1990Regex))
            {
                plateReturn.Info = ParseStandard1990Plate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "at",
                Flag = "🇦🇹"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseStandard1990Plate(string plate, bool officialSpecial = false)
        {
            Match match = null;

            if(!officialSpecial) {
                Regex regex = new Regex(Standard1990Regex);
                match = regex.Match(plate);
            } else {
                Regex regex = new Regex(Standard1990OfficialRegex);
                match = regex.Match(plate);
            }

            string regionOrSpecial = match.Groups[2].Value;

            Enums.PlateFormat plateFormat = Enums.PlateFormat.At_Standard1990;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = GetRegionCode(regionOrSpecial),
                Special = GetSpecialCode(regionOrSpecial)
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
                SpecialCodes.TryGetValue(code, out decodedSpecial);
                return decodedSpecial;  
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            {"AM", "Amstetten"},
            {"BA", "Bad Aussee"},
            {"BL", "Bruck an der Leitha"},
            {"BM", "Bruck-Mürzzuschlag / Bruck an der Mur"},
            {"BN", "Baden"},
            {"BR", "Braunau am Inn"},
            {"BZ", "Bludenz"},
            {"DL", "Deutschlandsberg"},
            {"DO", "Dornbin"},
            {"E", "Eisenstadt"},
            {"EF", "Eferding"},
            {"EU", "Eisenstadt-Umgebung"},
            {"FB", "Feldbach"},
            {"FE", "Feldkirchen"},
            {"FF", "Fürstenfeld"},
            {"FK", "Feldkirch"},
            {"FR", "Freistadt"},
            {"G", "Graz"},
            {"GB", "Gröbming"},
            {"GD", "Gmünd"},
            {"GF", "Gänserndorf"},
            {"GM", "Gmunden"},
            {"GR", "Grieskirchen"},
            {"GS", "Güssing"},
            {"GU", "Graz-Umgebung"},
            {"HA", "Hallein"},
            {"HB", "Hartberg"},
            {"HF", "Hartberg-Fürstenfeld"},
            {"HL", "Hollabrunn"},
            {"HO", "Horn"},
            {"I", "Innsbruck"},
            {"IL", "Innsbruck-Land"},
            {"IM", "Imst"},
            {"JE", "Jennersdorf"},
            {"JO", "St. Johann im Pongau"},
            {"JU", "Judenburd"},
            {"K", "Klagenfurt"},
            {"KB", "Kitzbühel"},
            {"KI", "Kirchdorf an der Krems"},
            {"KF", "Knittelfeld"},
            {"KL", "Klagenfurt-Land"},
            {"KO", "Korneuburg"},
            {"KR", "Krems-Land"},
            {"KS", "Krems City"},
            {"KU", "Kufstein"},
            {"L", "Linz"},
            {"LA", "Landeck"},
            {"LB", "Leibnitz"},
            {"LE", "Leoben City"},
            {"LF", "Lillienfeld"},
            {"LI", "Liezen"},
            {"LL", "Linz-Land"},
            {"LN", "Leoben"},
            {"LZ", "Lienz"},
            {"MA", "Mattersburg"},
            {"MD", "Mödling"},
            {"ME", "Melk"},
            {"MI", "Mistelbach"},
            {"MT", "Murtal"},
            {"MU", "Murau"},
            {"MZ", "Mürzzuschlag"},
            {"ND", "Neusiedl am See"},
            {"NK", "Neunkirchen"},
            {"OP", "Oberpullendorf"},
            {"OW", "Oberwart"},
            {"P", "St. Pölten"},
            {"PE", "Perg"},
            {"PL", "St. Pölten-Land"},
            {"RA", "Bad Radkersburg"},
            {"RE", "Reutte"},
            {"RI", "Ried im Innkreis"},
            {"RO", "Rohrbach"},
            {"S", "Salzburg City"},
            {"SB", "Scheibbs"},
            {"SE", "Steyr-Land"},
            {"SL", "Salzburg-Umgebung"},
            {"SP", "Spittal an der Drau"},
            {"SO", "Südoststeiermark"},
            {"SR", "Steyr City"},
            {"SV", "St. Veit an der Glan"},
            {"SW", "Schwechat City"},
            {"SZ", "Schwaz"},
            {"TA", "Tamsweg"},
            {"TU", "Tulln"},
            {"UU", "Urfahr-Umgebung"},
            {"VB", "Vöcklabruck"},
            {"VI", "Villach City"},
            {"VL", "Villach-Land"},
            {"VO", "Voitsberg"},
            {"W", "Vienna"},
            {"WB", "Wiener Neustadt-Land"},
            {"WE", "Wels City"},
            {"WL", "Wels-Land"},
            {"WN", "Wiener Neustadt City"},
            {"WO", "Wolfsberg"},
            {"WT", "Waidhofen an der Thaya"},
            {"WU", "Wien-Umgebung"},
            {"WY", "Waidhofen an der Ybbs"},
            {"WZ", "Weiz"},
            {"ZE", "Zell am See"},
            {"ZT", "Zwettl"}
        };

        private static Dictionary<string, string> SpecialCodes = new Dictionary<string, string>()
        {
            {"A", "Federal official"},
            {"B", "Bregenz, Burgenland official"},
            {"BB", "Bundesbahnen (Federal Railways)"},
            {"BD", "Kraftfahrlinien Bundesbus (bus service)"},
            {"BE", "Bestattung (funeral services)"},
            {"BG", "Bundesgendarmerie (Federal Police)"},
            {"BH", "Bundesheer (Federal Army)"},
            {"BP", "Bundespolizei (Federal Police)"},
            {"EW", "E-Werk (electric power company)"},
            //{"FF", "Freiwillige Feuerwehr (volunteer firemen)"}, // NOTE: Clashes with FF (above)
            {"FV", "Finanzverwaltung (financial administration)"},
            {"FW", "Feuerwehr (firemen)"},
            {"GW", "Gaswerk (gas power company)"},
            {"GT", "Gütertransport (vehicles transporting goods)"},
            {"JW", "Justizwache (justice police)"},
            {"KT", "Kleintransport (private vehicles transporting parcels)"},
            {"LO", "Linienomnibus (public service buse)"},
            {"LR", "Landesregierung (Local government of Niederösterreich)"},
            {"LV", "Landesregierung (Local government of Tyrol)"},
            {"MA", "Magistrat Wien (Local government of Vienna)"},
            {"MW", "Mietwagen (private hire car/bus service)"},
            {"N", "Lower Austria official"},
            {"O", "Upper Austria official"},
            {"PT", "Post & Telekom Austria"},
            {"RD", "Rettungsdienst (ambulance)"},
            {"RK", "Rotes Kreuz (Red Cross)"},
            {"T", "Tirol official"},
            {"TX", "Taxi"},
            {"V", "Vorarlberg official"},
            {"ZW", "Zollwache (customs official)"}
        };
    }
}
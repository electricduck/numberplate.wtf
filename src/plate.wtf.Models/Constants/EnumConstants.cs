using System.Collections.Generic;
using plate.wtf.Models;

namespace plate.wtf.Models.Constants
{
    public class EnumConstants
    {
        public static Dictionary<Enums.PlateFormat, string> PlateFormatConstants = new Dictionary<Enums.PlateFormat, string>()
        {
            // AT
            {Enums.PlateFormat.At_Standard1990, "Current (1990 onwards)"},

            // DE
            {Enums.PlateFormat.De_Diplomatic1956, "Diplomatic"},
            {Enums.PlateFormat.De_Standard1956, "Current (1956 onwards)"},

            // ES
            {Enums.PlateFormat.Es_Standard1900, "1900 to 1971"},
            {Enums.PlateFormat.Es_Standard1971, "1971 to 2000"},
            {Enums.PlateFormat.Es_Standard2000, "Current (2000 onwards)"},

            // FR
            {Enums.PlateFormat.Fr_Fni, "FNI (1950 to 2009)"},
            {Enums.PlateFormat.Fr_Siv, "SIV (2009 onwards)"},

            // GB
            {Enums.PlateFormat.Gb_1902, "1902 to 1932"},
            {Enums.PlateFormat.Gb_1932, "1932 to 1953"},
            {Enums.PlateFormat.Gb_1953, "1953 to 1963"},
            {Enums.PlateFormat.Gb_Suffix, "Suffix (1963 to 1982)"},
            {Enums.PlateFormat.Gb_Prefix, "Prefix (1982 to 2001)"},
            {Enums.PlateFormat.Gb_Current, "Current (2001 to 2051)"},
            {Enums.PlateFormat.Gb_Trade2015, "Trade (2015 onwards)"},
            {Enums.PlateFormat.Gb_Diplomatic1979, "Diplomatic (1979 onwards)"},

            // GG
            {Enums.PlateFormat.Gg_Standard1908, "Current (1908 onwards)"},

            // NL
            {Enums.PlateFormat.Nl_1898, "1898 to 1951"},
            {Enums.PlateFormat.Nl_SideCode1, "Side Code 1 (1951 to 1956"},
            {Enums.PlateFormat.Nl_SideCode2, "Side Code 2 (1956 to 1973)"},
            {Enums.PlateFormat.Nl_SideCode3, "Side Code 3 (1973 to 1978)"},
            {Enums.PlateFormat.Nl_SideCode4, "Side Code 4 (1978 to 1991)"},
            {Enums.PlateFormat.Nl_SideCode5, "Side Code 5 (1991 to 1999)"},
            {Enums.PlateFormat.Nl_SideCode6, "Side Code 6 (1999 to 2008)"},
            {Enums.PlateFormat.Nl_SideCode7, "Side Code 7 (2006 onwards)"},
            {Enums.PlateFormat.Nl_SideCode8, "Side Code 8 (2006 onwards)"},
            {Enums.PlateFormat.Nl_SideCode9, "Side Code 9 (2006 onwards)"},
            {Enums.PlateFormat.Nl_SideCode10, "Side Code 10 (2011 to 2015)"},
            {Enums.PlateFormat.Nl_SideCode11, "Side Code 11 (2015 onwards)"},

            // NO
            {Enums.PlateFormat.No_Standard1971, "Current (1971 onwards)"},

            // RU
            {Enums.PlateFormat.Ru_Standard1993, "Current (1993 onwards)"}
        };

        public static string GetPlateFormatConstant(Enums.PlateFormat plateFormatEnum)
        {
            if(PlateFormatConstants.ContainsKey(plateFormatEnum))
            {
                string plateFormatString;
                PlateFormatConstants.TryGetValue(plateFormatEnum, out plateFormatString);
                return plateFormatString;  
            }
            else
            {
                return "Unknown";
            }
        }
    }
}

namespace plate.wtf.Models
{
    public class Enums
    {
        public enum PlateFormat
        {
            Unspecified = 0,

            // AL / Albania
            Al_Standard2011 = 42,
            Al_Standard1993 = 43,
            Al_Diplomatic2011 = 44,

            // AT / Austria
            At_Standard1990 = 6,

            // DE / Germany
            De_Diplomatic1956 = 8,
            De_Standard1956 = 7,

            // ES / Spain
            Es_Standard1900 = 1,
            Es_Standard1971 = 2,
            Es_Standard2000 = 3,

            // FR / France
            Fr_Fni = 10,
            Fr_Siv = 11,

            // GB / Great Britain
            Gb_1902 = 25,           // 1902 to 1932
            Gb_1932 = 26,           // 1032 to 1953
            Gb_1953 = 27,           // 1953 to 1963
            Gb_Suffix = 28,         // 1963 to 1982 (Suffix)
            Gb_Prefix = 29,         // 1982 to 2001 (Prefix)
            Gb_Current = 30,        // 2001 to 2051 (Current)
            Gb_Trade2015 = 31,      // Trade (2015 onwards)
            Gb_Diplomatic1979 = 32, // Displomatic (1979 onwards)

            // GB-NIR / Northern Ireland
            GbNir_Standard1966 = 41,

            // GG / Guernsey
            Gg_Standard1908 = 5,

            // HU / Hungary
            Hu_Standard1990 = 45,

            // JP / Japan
            Jp_Standard1962 = 33,
            Jp_Export1962 = 34,

            // LT / Lithuania
            Lt_Standard = 35,
            Lt_ImportExport = 36,
            Lt_Trade = 37,
            Lt_Diplomatic = 38,
            Lt_Taxi = 39,
            Lt_Military = 40,

            // NL / Netherlands
            Nl_1898 = 12,           // 1898 to 1951
            Nl_SideCode1 = 13,      // 1951 to 1956 (Side Code 1)
            Nl_SideCode2 = 14,      // 1956 to 1973 (Side Code 2)
            Nl_SideCode3 = 15,      // 1973 to 1978 (Side Code 3)
            Nl_SideCode4 = 16,      // 1978 to 1991 (Side Code 4)
            Nl_SideCode5 = 17,      // 1991 to 1999 (Side Code 5)
            Nl_SideCode6 = 18,      // 1999 to 2008 (Side Code 6)
            Nl_SideCode7 = 19,      // 2006 onwards (Side Code 7)
            Nl_SideCode8 = 20,      // 2006 onwards (Side Code 8)
            Nl_SideCode9 = 21,      // 2006 onwards (Side Code 9)
            Nl_SideCode10 = 22,     // 2011 to 2015 (Side Code 10)
            Nl_SideCode11 = 23,     // 2015 onwards (Side Code 11)
            Nl_SideCode12 = 24,     // 2016 onwards (Side Code 12)

            // NO / Norway
            No_Standard1971 = 9,

            // RU / Russia
            Ru_Standard1993 = 4
        }
    }
}
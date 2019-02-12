using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using plate.wtf.Core.Plates.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.Constants;

namespace plate.wtf.Core.Plates
{
    public class FrPlate : IFrPlate
    {
        private static string FniRegex = @"^(([0-9]{1,4})([A-Z]{1,3})([0-9]{1,2}))$";
        private static string SivRegex = @"^(([A-Z]{2})-([0-9]{3})-([A-Z]{2}))$";

        public Plate Parse(string plate)
        {
            Plate plateReturn = new Plate {};

            if(Regex.IsMatch(plate, FniRegex))
            {
                plateReturn.Info = ParseFniPlate(plate);
                plateReturn.Valid = true;
            }
            else if(Regex.IsMatch(plate, SivRegex))
            {
                plateReturn.Info = ParseSivPlate(plate);
                plateReturn.Valid = true;
            }
            else
            {
                plateReturn.Valid = false;
            }

            plateReturn.Country = new Country
            {
                Code = "fr",
                Flag = "🇫🇷"
            };

            plateReturn.Parsed = plate;

            return plateReturn;
        }

        private static PlateInfo ParseSivPlate(string plate)
        {
            Regex regex = new Regex(SivRegex);
            Match match = regex.Match(plate);

            int issue = GetSivIssue(
                match.Groups[2].Value,
                match.Groups[4].Value,
                Convert.ToInt32(match.Groups[3].Value)
            );
            string year = GuessSivYear(issue).ToString();

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Fr_Siv;

            PlateInfo returnModel = new PlateInfo
            {
                ApproxIssue = true,
                ApproxRegistrationYear = true,
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Issue = issue,
                RegistrationYear = year
            };

            return returnModel;
        }

        // TODO: Include special plates
        private static PlateInfo ParseFniPlate(string plate)
        {
            Regex regex = new Regex(FniRegex);
            Match match = regex.Match(plate);

            int region = Convert.ToInt32(match.Groups[4].Value);

            string regionString = GetFniRegionCode(region);
            string specialString = null;

            Enums.PlateFormat plateFormat = Enums.PlateFormat.Fr_Fni;

            PlateInfo returnModel = new PlateInfo
            {
                Format = EnumConstants.GetPlateFormatConstant(plateFormat),
                FormatEnum = plateFormat,
                Region = regionString,
                Special = specialString
            };

            return returnModel;
        }

        private static int GetSivIssue(string firstLetters, string lastLetters, int number)
        {
            string letters = $@"{firstLetters}{lastLetters}";

            int firstLetter = GetSivIssueCodeLocation(letters[3]);
            int secondLetter = GetSivIssueCodeLocation(letters[2]);
            int thirdLetter = GetSivIssueCodeLocation(letters[1]);
            int fourthLetter = GetSivIssueCodeLocation(letters[0]);

            int maxSecondLetter = GetSivIssueCodeLocation('Z')+1;
            int maxThirdLetter = (maxSecondLetter*maxSecondLetter);
            int maxFourthLetter = (maxThirdLetter*maxSecondLetter);

            //int letterTotal = (22*12167)+(22*529)+(22*23)+22

            int letterTotal = (fourthLetter * maxFourthLetter) +
                (thirdLetter * maxThirdLetter) +
                (secondLetter * maxSecondLetter) +
                firstLetter
                + 1;

            int plateTotal = (letterTotal-1) * 999 + number;

            return plateTotal;
        }

        private static int GuessSivYear(int issue)
        {
            int approxLifespanOfSIV = 80;
            int maxIssue = GetSivIssue("ZZ", "ZZ", 999);
            int averageRegistrationsPerYear = maxIssue/approxLifespanOfSIV;
            int year = 2003;
            int yearModifier = 0;

            do
            {
                yearModifier++;
                year++;
            } while(!(issue < averageRegistrationsPerYear*yearModifier));

            return year;
        }

        private static int GetSivIssueCodeLocation(char issueCode)
        {
            if(SivIssueCodeLocations.ContainsKey(issueCode)) {
                int issue;

                SivIssueCodeLocations.TryGetValue(issueCode, out issue);

                return issue;
            } else {
                return 0;
            }
        }

        private static string GetFniRegionCode(int locationCode)
        {
            if(FniRegionCodes.ContainsKey(locationCode)) {
                string location;

                FniRegionCodes.TryGetValue(locationCode, out location);

                return location;
            } else {
                return "";
            }
        }

        private static Dictionary<char, int> SivIssueCodeLocations = new Dictionary<char, int>()
        {
            {'A', 0},
            {'B', 1},
            {'C', 2},
            {'D', 3},
            {'E', 4},
            {'F', 5},
            {'G', 6},
            {'H', 7},
            {'J', 8},
            {'K', 9},
            {'L', 10},
            {'M', 11},
            {'N', 12},
            {'P', 13},
            {'Q', 14},
            {'R', 15},
            {'S', 16},
            {'T', 17},
            {'V', 18},
            {'W', 19},
            {'X', 20},
            {'Y', 21},
            {'Z', 22}
        };

        private static Dictionary<int, string> FniRegionCodes = new Dictionary<int, string>()
        {
            {01, "Ain, Bourg-en-Bresse"},
            {02, "Aisne, Laon"},
            {03, "Allier, Moulins"},
            {04, "Alpes-de-Haute-Provence, Digne-les-Bains"},
            {05, "Hautes-Alpes, Gap"},
            {06, "Alpes-Maritimes, Nice"},
            {07, "Ardèche, Privas"},
            {08, "Ardenes, Charleville-Mézières"},
            {09, "Ariège, Foix"},
            {10, "Aube, Troyes"},
            {11, "Aude, Carcassone"},
            {12, "Aveyron, Rodez"},
            {13, "Bouches-du-Rhône, Marseille"},
            {14, "Calvados, Caen"},
            {15, "Cantal, Aurillac"},
            {16, "Charente, Angoulême"},
            {17, "Charente-Maritime, La Rochelle"},
            {18, "Cher, Bourges"},
            {19, "Corrèze, Tulle"},
            {20, "Corse, Ajaccio"},
            {21, "Côte-d'Or, Dijon"},
            {22, "Côtes-d'Armor, Saint-Brieuc"},
            {23, "Creuse, Guéret"},
            {24, "Dordogne, Périgueux"},
            {25, "Doubs, Besançon"},
            {26, "Drôme, Valence"},
            {27, "Eure, Évreux"},
            {28, "Eure-et-Loir, Chartres"},
            {29, "Finistère, Quimper"},
            {30, "Gard, Nîmes"},
            {31, "Haute-Garonne, Toulouse"},
            {32, "Gers, Auch"},
            {33, "Gironde, Bordeaux"},
            {34, "Hérault, Montpellier"},
            {35, "Ille-et-Vilaine, Rennes"},
            {36, "Indre, Châteauroux"},
            {37, "Indre-et-Loire, Tours"},
            {38, "Isère, Grenoble"},
            {39, "Jura, Lons-le-Saunier"},
            {40, "Landes, Mont-de-Marsan"},
            {41, "Loir-et-Cher, Blois"},
            {42, "Loire, Saint-Étienne"},
            {43, "Haute-Loire, Le Puy-en-Velay"},
            {44, "Loire-Atlantique, Nantes"},
            {45, "Loiret, Orléans"},
            {46, "Lot, Cahros"},
            {47, "Lot-et-Garonne, Agen"},
            {48, "Lozère, Mende"},
            {49, "Maine-et-Loire, Angers"},
            {50, "Manche, Saint-Lô"},
            {51, "Marne, Châlons-en-Champagne"},
            {52, "Haute-Marne, Chaumont"},
            {53, "Mayenne, Laval"},
            {54, "Meurthe-et-Moselle, Nancy"},
            {55, "Meuse, Bar-le-DEc"},
            {56, "Morbihan, Vannes"},
            {57, "Moselle, Metz"},
            {58, "Nièvre, Nevers"},
            {59, "Nord, Lille"},
            {60, "Oise, Beauvais"},
            {61, "Orne, Alençon"},
            {62, "Pas-de-Calais, Arras"},
            {63, "Puy-de-Dôme, Clermont-Ferrand"},
            {64, "Pyrénées-Atlantiques, Pau"},
            {65, "Hautes-Pyrénées, Tarbes"},
            {66, "Pyrénées-Orientales, Perpignan"},
            {67, "Bas-Rhin, Strasbourg"},
            {68, "Haut-Rhin, Colmar"},
            {69, "Rhône, Lyon"},
            {70, "Haute-Saône, Vesoul"},
            {71, "Saône-et-Loire, Mâcon"},
            {72, "Sarthe, Le Mans"},
            {73, "Savoie, Chambéry"},
            {74, "Haute-Savoie, Annecy"},
            {75, "Paris, Paris"},
            {76, "Seine-Maritime, Rouen"},
            {77, "Seine-et-Marne, Melin"},
            {78, "Yvelines, Versailles"},
            {79, "Deux-Sèvres, Niort"},
            {80, "Somme, Amiens"},
            {81, "Tarn, Albi"},
            {82, "Tarn-et-Garonne, Montauban"},
            {83, "Var, Toulon"},
            {84, "Vaucluse, Avignon"},
            {85, "Vendée, La Roche-sur-Yon"},
            {86, "Vienne, Poitiers"},
            {87, "Haute-Vienne, Limoges"},
            {88, "Vosges, Épinal"},
            {89, "Yonne, Auxerre"},
            {90, "Territoire de Belfort, Belfort"},
            {91, "Essonne, Évry"},
            {92, "Hauts-de-Seine, Nanterre"},
            {93, "Seine-Saint-Denis, Bobigny"},
            {94, "Val-de-Marne, Créteil"},
            {95, "Val-d'Oise, Pontoise"}
        };
    }
}
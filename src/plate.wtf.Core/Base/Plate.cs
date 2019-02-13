using System.Collections.Generic;
using plate.wtf.Core.Interfaces;
using plate.wtf.Core.Plates.Interfaces;
using Model = plate.wtf.Models;

namespace plate.wtf.Core
{
    public class Plate : IPlate
    {
        public IAtPlate _atPlate { get; }
        public IDePlate _dePlate { get; }
        public IEsPlate _esPlate { get; }
        public IFrPlate _frPlate { get; }
        public IGbPlate _gbPlate { get; }
        public IGgPlate _ggPlate { get; }
        public IJpPlate _jpPlate { get; }
        public INlPlate _nlPlate { get; }
        public INoPlate _noPlate { get; }
        public IRuPlate _ruPlate { get; }

        public Plate
        (
            IAtPlate atPlate,
            IDePlate dePlate,
            IEsPlate esPlate,
            IFrPlate frPlate,
            IGbPlate gbPlate,
            IGgPlate ggPlate,
            IJpPlate jpPlate,
            INlPlate nlPlate,
            INoPlate noPlate,
            IRuPlate ruPlate
        )
        {
            _atPlate = atPlate;
            _dePlate = dePlate;
            _esPlate = esPlate;
            _frPlate = frPlate;
            _gbPlate = gbPlate;
            _ggPlate = ggPlate;
            _jpPlate = jpPlate;
            _nlPlate = nlPlate;
            _noPlate = noPlate;
            _ruPlate = ruPlate;
        }

        public List<Model.Plate> ParsePlate(string plate, string country = "")
        {
            List<Model.Plate> platesReturn = new List<Model.Plate>();

            country = country.ToLower();
            plate = plate
                .ToUpper()
                .Replace(" ", "");

            if(country.Length == 2)
            {
                switch(country)
                {
                    case "at":
                        var parsedAtPlate = _atPlate.Parse(plate);
                        platesReturn.Add(parsedAtPlate);
                        break;
                    case "de":
                        var parsedDePlate = _dePlate.Parse(plate);
                        platesReturn.Add(parsedDePlate);
                        break;
                    case "es":
                        var parsedEsPlate = _esPlate.Parse(plate);
                        platesReturn.Add(parsedEsPlate);
                        break;
                    case "fr":
                        var parsedFrPlate = _frPlate.Parse(plate);
                        platesReturn.Add(parsedFrPlate);
                        break;
                    case "gb":
                    case "uk":
                        var parsedGbPlate = _gbPlate.Parse(plate);
                        platesReturn.Add(parsedGbPlate);
                        break;
                    case "gg":
                        var parsedGgPlate = _ggPlate.Parse(plate);
                        platesReturn.Add(parsedGgPlate);
                        break;
                    case "jp":
                        var parsedJpPlate = _jpPlate.Parse(plate);
                        platesReturn.Add(parsedJpPlate);
                        break;
                    case "nl":
                        var parsedNlPlate = _nlPlate.Parse(plate);
                        platesReturn.Add(parsedNlPlate);
                        break;
                    case "no":
                        var parsedNoPlate = _noPlate.Parse(plate);
                        platesReturn.Add(parsedNoPlate);
                        break;
                    case "ru":
                        var parsedRuPlate = _ruPlate.Parse(plate);
                        platesReturn.Add(parsedRuPlate);
                        break;
                }
            }
            else if(country == "any")
            {
                platesReturn = ParseAnyPlate(plate);
            }

            return platesReturn;
        }

        public List<Model.Plate> ParseAnyPlate(string plate)
        {
            List<Model.Plate> matchesReturn = new List<Model.Plate>();

            var parsedAtPlate = _atPlate.Parse(plate);
            var parsedDePlate = _dePlate.Parse(plate);
            var parsedEsPlate = _esPlate.Parse(plate);
            var parsedFrPlate = _frPlate.Parse(plate);
            var parsedGbPlate = _gbPlate.Parse(plate);
            var parsedGgPlate = _ggPlate.Parse(plate);
            var parsedJpPlate = _jpPlate.Parse(plate);
            var parsedNlPlate = _nlPlate.Parse(plate);
            var parsedNoPlate = _noPlate.Parse(plate);
            var parsedRuPlate = _ruPlate.Parse(plate);

            if(parsedAtPlate.Valid) { matchesReturn.Add(parsedAtPlate); }
            if(parsedDePlate.Valid) { matchesReturn.Add(parsedDePlate); }
            if(parsedEsPlate.Valid) { matchesReturn.Add(parsedEsPlate); }
            if(parsedFrPlate.Valid) { matchesReturn.Add(parsedFrPlate); }
            if(parsedGbPlate.Valid) { matchesReturn.Add(parsedGbPlate); }
            if(parsedGgPlate.Valid) { matchesReturn.Add(parsedGgPlate); }
            if(parsedJpPlate.Valid) { matchesReturn.Add(parsedJpPlate); }
            if(parsedNlPlate.Valid) { matchesReturn.Add(parsedNlPlate); }
            if(parsedNoPlate.Valid) { matchesReturn.Add(parsedNoPlate); }
            if(parsedRuPlate.Valid) { matchesReturn.Add(parsedRuPlate); }

            return matchesReturn;
        }
    }
}
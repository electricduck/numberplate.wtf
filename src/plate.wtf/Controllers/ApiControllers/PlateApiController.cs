using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using plate.wtf.Core.Interfaces;
using plate.wtf.Models;
using plate.wtf.Models.ApiModels;

namespace plate.wtf.Controllers.ApiControllers
{
    [Route("/api/plate")]
    public class PlateApiController : Controller
    {
        public IPlate _plate { get; }

        public PlateApiController
        (
            IPlate plate
        )
        {
            _plate = plate;
        }

        [Route("{country}/{plate}")]
        public PlateApiModel Plate(string country, string plate)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            PlateApiModel returnModel = new PlateApiModel {};
            List<Plate> plates = null;

            if(country == "any")
            {
                plates = _plate.ParsePlate(plate, "any");
            }
            else
            {
                plates = _plate.ParsePlate(plate, country);
            }

            returnModel.Plates = plates;

            sw.Stop();
            returnModel.CalculationTime = sw.ElapsedMilliseconds;

            return returnModel;
        }
    }
}
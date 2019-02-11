using System.Collections.Generic;
using plate.wtf.Models;

namespace plate.wtf.Models.ApiModels
{
    public class PlateApiModel
    {
        public decimal CalculationTime { get; set; }
        public List<Plate> Plates { get; set; }
    }
}
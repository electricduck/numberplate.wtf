using System.Collections.Generic;
using Model = plate.wtf.Models;

namespace plate.wtf.Core.Interfaces
{
    public interface IPlate
    {
        List<Model.Plate> ParsePlate(string plate, string country = "");
    }
}
using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IPlPlate
    {
        Plate Parse(string plate);
    }
}
using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IAlPlate
    {
        Plate Parse(string plate);
    }
}
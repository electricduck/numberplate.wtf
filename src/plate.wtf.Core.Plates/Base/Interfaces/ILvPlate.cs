using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface ILvPlate
    {
        Plate Parse(string plate);
    }
}
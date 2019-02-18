using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IImPlate
    {
        Plate Parse(string plate);
    }
}
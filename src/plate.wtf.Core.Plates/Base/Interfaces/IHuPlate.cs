using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IHuPlate
    {
        Plate Parse(string plate);
    }
}
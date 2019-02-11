using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IFrPlate
    {
        Plate Parse(string plate);
    }
}
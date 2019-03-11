using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IFiPlate
    {
        Plate Parse(string plate);
    }
}
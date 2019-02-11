using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IEsPlate
    {
        Plate Parse(string plate);
    }
}
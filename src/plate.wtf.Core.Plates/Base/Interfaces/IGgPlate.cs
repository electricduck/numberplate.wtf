using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IGgPlate
    {
        Plate Parse(string plate);
    }
}
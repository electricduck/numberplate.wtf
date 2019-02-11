using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IRuPlate
    {
        Plate Parse(string plate);
    }
}
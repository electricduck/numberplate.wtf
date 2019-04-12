using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface ISePlate
    {
        Plate Parse(string plate);
    }
}
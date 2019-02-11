using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IDePlate
    {
        Plate Parse(string plate);
    }
}
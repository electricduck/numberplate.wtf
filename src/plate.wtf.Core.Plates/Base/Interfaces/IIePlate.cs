using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IIePlate
    {
        Plate Parse(string plate);
    }
}
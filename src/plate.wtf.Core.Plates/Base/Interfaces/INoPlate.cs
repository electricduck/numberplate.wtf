using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface INoPlate
    {
        Plate Parse(string plate);
    }
}
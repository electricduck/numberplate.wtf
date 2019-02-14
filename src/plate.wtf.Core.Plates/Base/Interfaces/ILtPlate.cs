using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface ILtPlate
    {
        Plate Parse(string plate);
    }
}
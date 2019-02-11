using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IAtPlate
    {
        Plate Parse(string plate);
    }
}
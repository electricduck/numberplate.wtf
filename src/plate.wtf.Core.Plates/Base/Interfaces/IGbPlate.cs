using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IGbPlate
    {
        Plate Parse(string plate);
    }
}
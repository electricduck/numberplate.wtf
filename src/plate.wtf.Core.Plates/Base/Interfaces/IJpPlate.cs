using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IJpPlate
    {
        Plate Parse(string plate);
    }
}
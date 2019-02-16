using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface IGbNirPlate
    {
        Plate Parse(string plate);
    }
}
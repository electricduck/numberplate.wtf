using plate.wtf.Models;

namespace plate.wtf.Core.Plates.Interfaces
{
    public interface INlPlate
    {
        Plate Parse(string plate);
    }
}
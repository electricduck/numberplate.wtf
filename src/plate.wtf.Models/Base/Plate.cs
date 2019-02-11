
namespace plate.wtf.Models
{
    public class Plate
    {
        public Country Country { get; set; }
        public PlateInfo Info { get; set; }
        public string Serial { get; set; }
        public bool Valid { get; set; }
    }
}
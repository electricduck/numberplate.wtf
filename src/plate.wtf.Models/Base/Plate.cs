
namespace plate.wtf.Models
{
    public class Plate
    {
        public Country Country { get; set; }
        public PlateInfo Info { get; set; }
        public string Parsed { get; set; }
        public bool Valid { get; set; }
    }
}
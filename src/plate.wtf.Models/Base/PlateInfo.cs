
namespace plate.wtf.Models
{
    public class PlateInfo
    {
        public bool ApproxIssue { get; set; }
        public bool ApproxRegistrationYear { get; set; }
        public Diplomatic Diplomatic { get; set; }
        public string Format { get; set; }
        public Enums.PlateFormat FormatEnum { get; set; }
        public int Issue { get; set; }
        public string Region { get; set; }
        public string RegistrationYear { get; set; }
        public string Series { get; set; }
        public string Special { get; set; }
        public string VehicleType { get; set; }
    }
}
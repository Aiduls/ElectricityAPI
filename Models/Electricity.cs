namespace ElectricityAPI1.Models
{
    public class Electricity
    {
        public int Id { get; set; }
        public string? Network { get; set; }
        public string? Name { get; set; }
        public string? ObjType { get; set; }
        public int? ObjNo { get; set; }
        public double? ElectricityUsed { get; set; }
        
        public double? ElectricityMade { get; set; }
        public DateTime? Date { get; set; }
        public double? ElectricityDifference { get; set; }
    }
}

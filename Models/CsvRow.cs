using CsvHelper.Configuration.Attributes;

namespace ElectricityAPI1.Models
{
    public class CsvRow
    {
        [Name("TINKLAS")]
        public string? Network { get; set; }

        [Name("OBT_PAVADINIMAS")]
        public string? Name { get; set; }

        [Name("OBJ_GV_TIPAS")]
        public string? ObjType { get; set; }

        [Name("OBJ_NUMERIS")]
        public int? ObjNo { get; set; }

        [Name("P+")]
        public double? ElectricityUsed { get; set; }

        [Name("PL_T")]
        public DateTime? Date { get; set; }

        [Name("P-")]
        public double? ElectricityMade { get; set; }

        [Name("P+-")]
        public double? ElectricityDifference { get; set; }
    }
}


/*public int Id { get; set; }
public string? Network { get; set; }
public string? Name { get; set; }
public string? ObjType { get; set; }
public int? ObjNo { get; set; }
public double? ElectricityUsed { get; set; }

public double? ElectricityMade { get; set; }
public DateTime? Date { get; set; }
public double? ElectricityDifference { get; set; }*/
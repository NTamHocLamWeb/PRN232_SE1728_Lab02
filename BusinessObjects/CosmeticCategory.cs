using System.Text.Json.Serialization;

namespace BusinessObjects;

public partial class CosmeticCategory
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string UsagePurpose { get; set; } = null!;

    public string FormulationType { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<CosmeticInformation> CosmeticInformations { get; set; } = new List<CosmeticInformation>();
}

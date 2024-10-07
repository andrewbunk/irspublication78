using System.Text.Json.Serialization;

namespace IRSPublication78.Server.Models
{
    public class DeductibilityCode
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string OrgType { get; set; }
        public required string DeductibilityLimitation { get; set; }
        [JsonIgnore]
        public virtual List<Organization> Organizations { get; } = [];
    }

    public class DeductibilityCodeOrganization
    {
        public int DeductibilityCodesId { get; set; }
        public int OrganizationsId { get; set; }
    }
}

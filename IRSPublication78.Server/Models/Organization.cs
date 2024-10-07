using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IRSPublication78.Server.Models
{
    public class Organization
    {
        public int Id { get; set; }
        [MaxLength(9)]
        public required string EIN { get; set; }
        [MaxLength(72)]
        public required string Name { get; set; }
        [MaxLength(23)]
        public required string City { get; set; }
        [MaxLength(2)]
        public required string State { get; set; }
        [MaxLength(22)]
        public required string Country { get; set; }
        public virtual List<DeductibilityCode> DeductibilityCodes { get; set; } = [];
    }

    public class OrgSearchResult
    {
        public List<Organization> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}

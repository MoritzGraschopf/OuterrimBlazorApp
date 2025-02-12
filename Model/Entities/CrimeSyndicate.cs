using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("CRIME_SYNDICATES")]
public class CrimeSyndicate
{
    public int Id { get; set; }

    [StringLength(120)]
    public string Name { get; set; }
    [StringLength(200)]
    public string Location { get; set; }

    public List<MercenaryReputation> MercenaryReputations { get; set; }
}
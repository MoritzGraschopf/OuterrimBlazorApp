using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("MERCENARY_REPUTATIONS")]
public class MercenaryReputation
{
    public int SyndicateId { get; set; }
    public int MercenaryId { get; set; }

    public CrimeSyndicate? CrimeSyndicate { get; set; }
    public Mercenary? Mercenary { get; set; }

    [StringLength(45)] public string ReputationChange { get; set; } = "";
}
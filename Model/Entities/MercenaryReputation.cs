using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class MercenaryReputation
{
    public int SyndicateId { get; set; }
    public int MercenaryId { get; set; }

    public CrimeSyndicate CrimeSyndicate { get; set; }
    public Mercenary Mercenary { get; set; }

    [StringLength(45)]
    public string ReputationChange { get; set; }
}
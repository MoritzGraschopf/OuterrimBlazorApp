using System.ComponentModel.DataAnnotations;

namespace View.Dto;

public class MercenaryReputationDto
{
    public int SyndicateId { get; set; }
    public int MercenaryId { get; set; }
    [StringLength(45)] public string ReputationChange { get; set; } = "";
}
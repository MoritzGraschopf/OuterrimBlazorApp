using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFT_CREW_JT")]
public class AircraftCrew
{
    public int AircraftId { get; set; }
    public int MercenaryId { get; set; }

    public Aircraft Aircraft { get; set; }
    public Mercenary Mercenary { get; set; }

    public DateTime JoinedAt { get; set; }
}
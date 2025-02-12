using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("AIRCRAFT_CREW_JT")]
public class AircraftCrew(int aircraftId, int mercenaryId, DateTime joinedAt)
{
    public int AircraftId { get; set; } = aircraftId;
    public int MercenaryId { get; set; } = mercenaryId;

    public Aircraft Aircraft { get; set; }
    public Mercenary Mercenary { get; set; }

    public DateTime JoinedAt { get; set; } = joinedAt;
}
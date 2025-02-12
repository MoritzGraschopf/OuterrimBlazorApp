using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("MERCENARIES")]
public class Mercenary
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int BodySkills { get; set; }
    public int CombatSkills { get; set; }

    public List<AircraftCrew> AircraftCrews { get; set; }
    public List<MercenaryReputation> MercenaryReputations { get; set; }
}
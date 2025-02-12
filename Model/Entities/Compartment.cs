namespace Model.Entities;

public class Compartment
{
    public int Id { get; set; }
    public int AircraftId { get; set; }

    public Aircraft Aircraft { get; set; }
}
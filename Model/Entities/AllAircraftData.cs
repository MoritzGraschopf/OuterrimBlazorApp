namespace Model.Entities;

public class AllAircraftData
{
    public Aircraft Aircraft { get; set; }
    public List<Compartment> Compartment { get; set; } = [];
    public List<Machinery> Machineries { get; set; } = [];
    public AircraftSpecification? Spec { get; set; }
}
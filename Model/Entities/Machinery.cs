using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("MACHINERIES")]
public class Machinery
{
    public int Id { get; set; }

    [StringLength(45)]
    public string Label { get; set; }
    public string Function { get; set; }

    public int CompartmentId { get; set; }
    public Compartment Compartment { get; set; }
}

public class EnergySystem : Machinery { }
public class EnviromentalSystem : Machinery { }
public class Weapon : Machinery { }
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[Table("ENVIROMENTAL_SYSTEMS")]
public class EnviromentalSystem
{
    public int MachineryId { get; set; }
    public Machinery Machinery { get; set; }
}
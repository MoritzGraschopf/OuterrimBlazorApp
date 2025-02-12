using System.Reflection.PortableExecutable;

namespace Model.Entities;

public class Weapon
{
    public int MachineryId { get; set; }
    public Machinery Machinery { get; set; }
}
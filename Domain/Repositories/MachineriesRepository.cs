using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class MachineriesRepository(OuterRimContext context) : ARepository<Machinery>(context)
{
    
}
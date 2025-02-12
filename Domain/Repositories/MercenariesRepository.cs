using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class MercenariesRepository(OuterRimContext context) : ARepository<Mercenary>(context)
{
    
}
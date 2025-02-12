using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class MercenaryReputationsRepository(OuterRimContext context) : ARepository<Mercenary>(context)
{
    
}
using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class CompartmentsRepository(OuterRimContext context) : ARepository<Compartment>(context)
{
    
}
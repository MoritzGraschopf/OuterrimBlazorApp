using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class AircraftSpecificationsRepository(OuterRimContext context) : ARepository<AircraftSpecification>(context)
{
    
}
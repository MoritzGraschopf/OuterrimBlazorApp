using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class AircraftCrewsRepository(OuterRimContext context): ARepository<AircraftCrew>(context)
{
    
}
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Context;
using Model.Entities;

namespace Domain.Repositories;
public class AircraftsRepository(OuterRimContext context) : ARepository<Aircraft>(context)
{

}


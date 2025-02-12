using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class CrimeSyndicatesRepository(OuterRimContext context) : ARepository<CrimeSyndicate>(context)
{
    
}
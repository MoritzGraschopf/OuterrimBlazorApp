using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.Context;
using Model.Entities;

namespace Domain.Repositories;
public class BookRepository(OuterRimContext context) : ARepository<>(context)
{
    private readonly OuterRimContext _context = context;

    public override async Task<List<Aircraft>> ReadAllAsync()
    {
        return await _context.Set<>()
            .Include(s => s.)
            .ToListAsync();
    }
}


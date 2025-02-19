using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Domain.Repositories;

public class AllAircraftDataRepository(OuterRimContext context) : IAllAircraftDataRepository
{
    public async Task<AllAircraftData?> ReadAsync(int id)
    {
        var newData = new AllAircraftData();
        var aircraft = await context.Set<Aircraft>().FindAsync(id);
        if (aircraft == null) return null;
        newData.Aircraft = aircraft;

        newData.Compartment = await context.Set<Compartment>().Where(c => c.AircraftId == aircraft.Id).ToListAsync();

        return newData;
    }

    public async Task<List<AllAircraftData>> ReadAsync(Expression<Func<AllAircraftData, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AllAircraftData>> ReadAsync(int start, int count)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AllAircraftData>> ReadAllAsync()
    {
        throw new NotImplementedException();
    }
}
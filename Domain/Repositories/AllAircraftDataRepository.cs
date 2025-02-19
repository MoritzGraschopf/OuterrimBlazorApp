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

        newData.Compartments = await context.Set<Compartment>().Where(c => c.AircraftId == aircraft.Id).ToListAsync();

        var compartmentIds = newData.Compartments.Select(c => c.Id).ToList();

        newData.Machineries = await context.Set<Machinery>()
            .Where(m => compartmentIds.Contains(m.CompartmentId))
            .ToListAsync();

        newData.Spec = await context.Set<AircraftSpecification>().FindAsync(aircraft.SpecificationId);

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
        var allAircrafts = await context.Set<Aircraft>().ToListAsync();
        var allAircraftsData = new List<AllAircraftData>();
        foreach (var aircraft in allAircrafts)
        {
            allAircraftsData.Add((await ReadAsync(aircraft.Id))!);
        }
        return allAircraftsData;
    }
}
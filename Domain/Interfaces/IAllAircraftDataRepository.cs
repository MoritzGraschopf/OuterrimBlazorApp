using System.Linq.Expressions;
using Model.Entities;

namespace Domain.Interfaces;

public interface IAllAircraftDataRepository
{
    Task<AllAircraftData?> ReadAsync(int id);
    Task<List<AllAircraftData>> ReadAsync(Expression<Func<AllAircraftData, bool>> filter);
    Task<List<AllAircraftData>> ReadAsync(int start, int count);
    Task<List<AllAircraftData>> ReadAllAsync();
}
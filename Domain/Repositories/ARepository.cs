using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Context;

namespace Domain.Repositories;

public abstract class ARepository<TEntity>(OuterRimContext context) : IRepository<TEntity> 
    where TEntity : class
{
    public virtual async Task<TEntity> CreateAsync(TEntity t)
    {
        await context.Set<TEntity>().AddAsync(t);
        await context.SaveChangesAsync();
        return t;
    }

    public virtual async Task<List<TEntity>> CreateRangeAsync(List<TEntity> list)
    {
        await context.Set<TEntity>().AddRangeAsync(list);
        await context.SaveChangesAsync();
        return list;
    }

    public virtual async Task UpdateAsync(int id, TEntity t)
    {
        var existingEntity = await context.Set<TEntity>().FindAsync(id) ?? throw new KeyNotFoundException("Entity not found");
        context.Entry(existingEntity).CurrentValues.SetValues(t);
        await context.SaveChangesAsync();
    }

    public virtual async Task UpdateRangeAsync(List<TEntity> list)
    {
        context.Set<TEntity>().UpdateRange(list);
        await context.SaveChangesAsync();
    }

    public virtual async Task<TEntity?> ReadAsync(int id) => await context.Set<TEntity>().FindAsync(id);
    
    public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter) =>
        await context.Set<TEntity>().Where(filter).ToListAsync();

    public virtual async Task<List<TEntity>> ReadAsync(int start, int count) =>
        await context.Set<TEntity>().Skip(start).Take(count).ToListAsync();

    public virtual async Task<List<TEntity>> ReadAllAsync() => await context.Set<TEntity>().ToListAsync();
    
    public virtual async Task DeleteAsync(int id, TEntity t)
    {
        var existingEntity = await context.Set<TEntity>().FindAsync(id) ?? throw new KeyNotFoundException("Entity not found");
        context.Set<TEntity>().Remove(existingEntity);
        await context.SaveChangesAsync();
    }
    public async Task DeleteRangeAsync(List<TEntity> list)
    {
        context.Set<TEntity>().RemoveRange(list);
        await context.SaveChangesAsync();
    }
}


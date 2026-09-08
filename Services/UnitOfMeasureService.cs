using FluentResults;
using groceries_helper.Persistence;
using groceries_helper.Persistence.Models;
using groceries_helper.Records;
using groceries_helper.Records.UnitOfMeasure;
using Microsoft.EntityFrameworkCore;

namespace groceries_helper.Services;

public class UnitOfMeasureService(GroceryHelperDbContext dbContext)
{
    public async Task<Result<GetUnitOfMeasureResponse>> GetAsync(int id, CancellationToken ct)
    {
        var unit = await dbContext.UnitOfMeasures
            .Select(i => new GetUnitOfMeasureResponse()
            {
                Id = i.Id,
                Name = i.Name,
            }).FirstOrDefaultAsync(c => c.Id == id, ct);
        
        if (unit is null)
        {
            return Result.Fail("Ingredient not found");
        }

        return Result.Ok(unit);
    }
    
    public async Task<Result<PaginationResponse<GetUnitOfMeasureResponse>>> GetAllAsync(CancellationToken ct)
    {
        var units = await dbContext.UnitOfMeasures.Select(i => new GetUnitOfMeasureResponse()
        {
            Id = i.Id,
            Name = i.Name,
        }).ToListAsync(ct);
        return Result.Ok(new PaginationResponse<GetUnitOfMeasureResponse>(units, units.Count));
    }

    public async Task<Result> AddAsync(AddUnitOfMeasureRequest request, CancellationToken ct)
    {
        await dbContext.UnitOfMeasures.AddAsync(new UnitOfMeasure
        {
            Name = request.Name,
            IsActive =  true,
        },ct);
        await dbContext.SaveChangesAsync(ct);
        return Result.Ok();
    }
    
    public async Task<Result> RemoveAsync(int id, CancellationToken ct)
    {
        var unit = await dbContext.UnitOfMeasures.FirstOrDefaultAsync(u => u.Id == id, ct);
        if (unit is null)
        {
            return Result.Fail("Unit of measure not found");
        }
        unit.IsActive = false;
        await dbContext.SaveChangesAsync(ct);
        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(UpdateUnitOfMeasureRequest request, CancellationToken ct)
    {
        var unit = await dbContext.UnitOfMeasures.FirstOrDefaultAsync(u => u.Id == request.Id, ct);
        if (unit is null)
        {
            return Result.Fail("Unit of measure not found");
        }
        unit.Name = request.Name;
        await dbContext.SaveChangesAsync(ct);
        return Result.Ok();
    }
}
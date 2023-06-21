using Microsoft.EntityFrameworkCore;
using TestWpf.Domain.Entities;
using TestWpf.Domain.Repsitory.Abstractions;

namespace TestWpf.Domain.Repsitory.EntityFramework;

public class EFInventoriesRepository : IInventoriesRepository
{
    private readonly ApplicationDbContext _context;

    public EFInventoriesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task DeleteInventoryByIdAsync(Guid id)
    {
        var inventory = await GetInventoryByIdAsync(id);

        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }
    }

    public async IAsyncEnumerable<Inventory> GetAllInventoriesAsync()
    {
        await foreach(var inventory in _context.Inventories)
            yield return inventory;
    }

    public async Task<Inventory> GetInventoryByIdAsync(Guid id)
    {
        return await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId  == id);
    }

    public async Task SaveInventoryAsync(Inventory inventory)
    {
        if (inventory.InventoryId == default)
            _context.Entry(inventory).State = EntityState.Added;

        else
            _context.Entry(inventory).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}

using TestWpf.Domain.Entities;

namespace TestWpf.Domain.Repsitory.Abstractions;

public interface IInventoriesRepository
{
    IAsyncEnumerable<Inventory> GetAllInventoriesAsync();

    Task<Inventory> GetInventoryByIdAsync(Guid id);

    Task SaveInventoryAsync(Inventory inventory);

    Task DeleteInventoryByIdAsync(Guid id);
}

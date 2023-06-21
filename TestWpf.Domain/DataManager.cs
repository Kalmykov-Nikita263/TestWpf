using TestWpf.Domain.Repsitory.Abstractions;

namespace TestWpf.Domain;

public class DataManager
{
    public DataManager(IAsssetsRepository asssetsRepository, IInventoriesRepository inventoriesRepository)
    {
        AsssetsRepository = asssetsRepository;
        InventoriesRepository = inventoriesRepository;
    }

    public IAsssetsRepository AsssetsRepository { get; set; }

    public IInventoriesRepository InventoriesRepository { get; set; }
}

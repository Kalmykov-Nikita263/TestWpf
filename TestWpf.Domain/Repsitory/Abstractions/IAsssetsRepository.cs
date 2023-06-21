using TestWpf.Domain.Entities;

namespace TestWpf.Domain.Repsitory.Abstractions;

public interface IAsssetsRepository
{
    IAsyncEnumerable<Asset> GetAllAssetsAsync();

    Task<Asset> GetAssetByIdAsync(Guid id);

    Task SaveAssetAsync(Asset asset);

    Task DeleteAssetByIdAsync(Guid id);
}

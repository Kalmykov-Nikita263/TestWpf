using Microsoft.EntityFrameworkCore;
using TestWpf.Domain.Entities;
using TestWpf.Domain.Repsitory.Abstractions;

namespace TestWpf.Domain.Repsitory.EntityFramework;

public class EFAssetsRepository : IAsssetsRepository
{
    private readonly ApplicationDbContext _context;

    public EFAssetsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAssetByIdAsync(Guid id)
    {
        var asset = await GetAssetByIdAsync(id);

        if (asset != null)
        {
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
        }
    }

    public async IAsyncEnumerable<Asset> GetAllAssetsAsync()
    {
        await foreach (var asset in _context.Assets)
            yield return asset;
    }

    public async Task<Asset> GetAssetByIdAsync(Guid id)
    {
        return await _context.Assets.FirstOrDefaultAsync(a => a.AssetId == id);
    }

    public async Task SaveAssetAsync(Asset asset)
    {
        if (asset.AssetId == default)
            _context.Entry(asset).State = EntityState.Added;

        else
            _context.Entry(asset).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}

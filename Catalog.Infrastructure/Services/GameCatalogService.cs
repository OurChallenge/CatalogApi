using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Db;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Services;

public class GameCatalogService : IGameCatalogService
{
    private readonly CloudGamesDbContext _context;

    public GameCatalogService(CloudGamesDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Game>> GetGamesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Games.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Game?> GetGameByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Games.FindAsync([id], cancellationToken);
    }

    public async Task<Game> CreateGameAsync(Game game, CancellationToken cancellationToken = default)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);
        return game;
    }

    public async Task<Game?> UpdateGameAsync(Game game, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Games.FindAsync([game.Id], cancellationToken);
        if (existing is null)
            return null;

        existing.UpdateTitle(game.Title);
        existing.UpdateDescription(game.Description);
        existing.UpdateGenre(game.Genre);
        existing.SetReleaseDate(game.ReleaseDate);
        existing.UpdatePrice(game.Price);
        existing.SetDiscount(game.Discount);

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteGameAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _context.Games.FindAsync([id], cancellationToken);
        if (game is null)
            return false;

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Game?> CreatePromotionAsync(Guid id, decimal discountPercentage, CancellationToken cancellationToken = default)
    {
        var game = await _context.Games.FindAsync([id], cancellationToken);
        if (game is null)
            return null;

        game.SetDiscount(discountPercentage / 100m);
        await _context.SaveChangesAsync(cancellationToken);
        return game;
    }
}

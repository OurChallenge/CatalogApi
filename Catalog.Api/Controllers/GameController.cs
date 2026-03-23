using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GameController : ControllerBase
{
    private readonly IGameCatalogService _gameCatalogService;

    public GameController(IGameCatalogService gameCatalogService)
    {
        _gameCatalogService = gameCatalogService;
    }

    [HttpGet("GetGame")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Game>>> GetGame(CancellationToken cancellationToken)
    {
        var games = await _gameCatalogService.GetGamesAsync(cancellationToken);
        return Ok(games);
    }

    [HttpGet("GetGameById")]
    [Authorize]
    public async Task<ActionResult<Game>> GetGameById(Guid id, CancellationToken cancellationToken)
    {
        var game = await _gameCatalogService.GetGameByIdAsync(id, cancellationToken);
        return game is null ? NotFound() : Ok(game);
    }

    [HttpPost("CreateGame")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Game>> CreateGame(Game game, CancellationToken cancellationToken)
    {
        var createdGame = await _gameCatalogService.CreateGameAsync(game, cancellationToken);
        return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
    }

    [HttpPut("UpdateGame")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Game>> UpdateGame(Game game, CancellationToken cancellationToken)
    {
        var updatedGame = await _gameCatalogService.UpdateGameAsync(game, cancellationToken);
        return updatedGame is null ? NotFound() : Ok(updatedGame);
    }

    [HttpDelete("DeleteGame")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGame(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _gameCatalogService.DeleteGameAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPut("CreatePromotion")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePromotion(Guid id, decimal discount, CancellationToken cancellationToken)
    {
        var promotedGame = await _gameCatalogService.CreatePromotionAsync(id, discount, cancellationToken);
        return promotedGame is null
            ? NotFound()
            : CreatedAtAction(nameof(GetGameById), new { id = promotedGame.Id }, promotedGame);
    }
}

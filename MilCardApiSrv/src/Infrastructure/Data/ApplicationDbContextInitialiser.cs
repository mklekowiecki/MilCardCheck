using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MilCardApiSrv.Domain.Constants;
using MilCardApiSrv.Domain.Entities;
using MilCardApiSrv.Domain.Enums;
using MilCardApiSrv.Infrastructure.Identity;

namespace MilCardApiSrv.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.CardActions.Any())
        {
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Ignore , "ACTION1"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Ignore, "ACTION2"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Any, PinSet.Ignore, "ACTION3"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Any, PinSet.Ignore, "ACTION4"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.Ignore, "ACTION5"));
            
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.Set, "ACTION6"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Set, "ACTION6"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Set, "ACTION6"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Blocked, PinSet.Set, "ACTION6"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.NotSet, "ACTION7"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.NotSet, "ACTION7"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.NotSet, "ACTION7"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Blocked, PinSet.Set, "ACTION7"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.Ignore, "ACTION8"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Ignore, "ACTION8"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Ignore, "ACTION8"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.Ignore, "ACTION8"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Blocked, PinSet.Ignore, "ACTION8"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Any, PinSet.Ignore, "ACTION9"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Ignore, "ACTION10"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Ignore, "ACTION10"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.Ignore, "ACTION10"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Ignore, "ACTION11"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Ignore, "ACTION11"));

            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Ignore, "ACTION12"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Ignore, "ACTION12"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Ordered, PinSet.Ignore, "ACTION12"));


            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Active, PinSet.Ignore, "ACTION13"));
            _context.CardActions.Add(new CardAction(CardType.Any, CardStatus.Inactive, PinSet.Ignore, "ACTION13"));


            await _context.SaveChangesAsync();
        }
    }
}

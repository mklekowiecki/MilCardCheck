using MilCardApiSrv.Domain.Entities;

namespace MilCardApiSrv.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<CardAction> CardActions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

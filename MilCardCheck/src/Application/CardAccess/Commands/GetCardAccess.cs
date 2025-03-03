using MilCardCheck.Application.Common.Interfaces;

namespace MilCardCheck.Application.CardAccess.Commands;
public record GetCardAccessCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class GetCardAccessCommandHandler : IRequestHandler<GetCardAccessCommand, int>
{
    private readonly IApplicationDbContext _context;

    public GetCardAccessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetCardAccessCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(1);
    }
}

using Microsoft.AspNetCore.Identity;
using MilCardApiSrv.Application.Common.Models;

namespace MilCardApiSrv.Infrastructure.Identity;
public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}

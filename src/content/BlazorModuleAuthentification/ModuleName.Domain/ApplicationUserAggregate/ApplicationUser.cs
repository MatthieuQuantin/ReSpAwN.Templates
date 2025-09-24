using Microsoft.AspNetCore.Identity;

namespace ModuleName.Domain.ApplicationUserAggregate;

public sealed class ApplicationUser : IdentityUser<Guid>
{
}
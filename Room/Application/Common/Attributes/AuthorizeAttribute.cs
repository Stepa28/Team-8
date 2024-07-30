using Team_8.Contracts.Enums;

namespace Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute()
    {
        Role = Role.None;
    }

    public AuthorizeAttribute(Role role)
    {
        Role = role;
    }

    public Role Role { get; }
}
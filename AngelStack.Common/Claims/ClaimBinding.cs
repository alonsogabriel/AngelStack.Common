namespace AngelStack.Common.Claims;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ClaimBinding(string claimType) : Attribute
{
    public string ClaimType { get; } = claimType;
}
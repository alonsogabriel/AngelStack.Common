using AngelStack.Common.Guards;
using AngelStack.Common.Strings;
using System.Security.Claims;

namespace AngelStack.Common.Claims;

public static class ClaimExtensions
{
    public static Dictionary<string, string> ToDictionary(this ClaimsPrincipal user)
    {
        return user.Claims.ToDictionary(c => c.Type, c => c.Value);
    }

    public static string GetClaim(this ClaimsPrincipal user, string name)
    {
        if (user.ToDictionary().TryGetValue(name, out var value) == false)
        {
            throw new ArgumentException($"Claim '{name}' not found.", nameof(name));
        }

        return value;
    }

    /// <summary>
    /// Return the claim value if it is one of the following types:
    /// <code>[sub, nameid, id, user_id]</code>
    /// </summary>
    /// <param name="user"></param>
    /// <returns>The claim value that represents the user id.</returns>
    public static string GetUserId(this ClaimsPrincipal user)
    {
        user.Guard();
        string[] claims = ["sub", "nameid", "id", "user_id"];
        var userId = user.ToDictionary().FirstOrDefault(pair => claims.Contains(pair.Key)).Value;

        return userId;
    }

    public static Guid GetUserGuid(this ClaimsPrincipal user) => user.GetUserId().ToGuid();

    public static int GetUserIntId(this ClaimsPrincipal user) => int.Parse(user.GetUserId());
}
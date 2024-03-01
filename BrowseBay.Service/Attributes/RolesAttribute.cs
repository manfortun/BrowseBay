namespace BrowseBay.Service.Attributes;

public class RolesAttribute : Attribute
{
    public string[] Roles { get; }
    public RolesAttribute(params Role[] roles)
    {
        Roles = roles.Select(r => r.ToString()).ToArray();
    }
}

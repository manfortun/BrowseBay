using BrowseBay.Service.Attributes;

namespace BrowseBay.Service;

public enum Policies
{
    [Roles([ Role.Seller, Role.Admin ])]
    SellerRights,

    [Roles([ Role.Admin ])]
    AdminRights,
}

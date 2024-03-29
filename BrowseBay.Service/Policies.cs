﻿using BrowseBay.Service.Attributes;

namespace BrowseBay.Service;

public enum Policy
{
    [Roles([ Role.Seller, Role.Admin ])]
    SellerRights,

    [Roles([ Role.Admin ])]
    AdminRights,

    [Roles([ Role.Buyer, Role.Seller, Role.Admin ])]
    BuyerRights,
}

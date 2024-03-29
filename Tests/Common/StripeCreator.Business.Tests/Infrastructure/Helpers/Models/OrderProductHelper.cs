﻿using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Infrastructure.Helpers.Models
{
    internal static class OrderProductHelper
    {
        public static readonly Guid TestProductId = new("e9e43c3f-cb55-4877-854f-b92263948506");
        public const int TestQuantity = 10;
        public static OrderProduct CreateOrderProduct(Guid? productId = null, int quantity = TestQuantity) =>
            new(productId ?? TestProductId, quantity);
    }
}

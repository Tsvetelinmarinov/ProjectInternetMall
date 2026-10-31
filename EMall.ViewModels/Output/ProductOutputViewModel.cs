using EMall.Core.ServiceModels.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMall.ViewModels.Output
{
    /// <summary>
    ///  Defines Product output view model.
    /// </summary>
    public class ProductOutputViewModel
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ProductClaimOutputViewModel> Claims { get; set; } = [];
        public double Price { get; set; }
        public CategoryOutputViewModel? Category { get; set; }
    }
}
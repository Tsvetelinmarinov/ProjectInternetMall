using EMall.Core.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMall.ViewModels
{
    /// <summary>
    ///  Defines Product output view model.
    /// </summary>
    public class ProductViewModel
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ProductClaimViewModel> Claims { get; set; } = [];
        public double Price { get; set; }
        public CategoryViewModel? Category { get; set; }
    }
}
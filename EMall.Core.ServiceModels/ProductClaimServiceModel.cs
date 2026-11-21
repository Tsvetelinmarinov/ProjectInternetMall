namespace EMall.Core.ServiceModels
{
    /// <summary>
    ///  Defines Product output service model.
    /// </summary>
    public class ProductClaimServiceModel
    {
        public required string ClaimName { get; set; }
        public required string ClaimValue { get; set; }
    }
}
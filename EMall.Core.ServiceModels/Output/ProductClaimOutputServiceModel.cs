namespace EMall.Core.ServiceModels.Output
{
    /// <summary>
    ///  Defines Product output service model.
    /// </summary>
    public class ProductClaimOutputServiceModel
    {
        public required string ClaimName { get; set; }
        public required string ClaimValue { get; set; }
    }
}
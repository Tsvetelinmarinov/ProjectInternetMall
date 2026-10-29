namespace EMall.Data.EntityModels
{
    /// <summary>
    ///  Describes single specification of a product.
    ///  Example of product claim: OS - Windows or Color - Red 
    /// </summary>
    public class ProductClaim
    {
        /// <summary>
        ///  The name of the claim.
        /// </summary>
        public required string ClaimName { get; set; }

        /// <summary>
        ///  The value of the claim.
        /// </summary>
        public required string ClaimValue { get; set; }
    }
}
namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// Valuation class is  of response object.
    /// </summary>
    public class Valuation
    {
        public Amount Amount { get; set; }
        public Regtax RegTax { get; set; }
        public Vat Vat { get; set; }
        public string Type { get; set; }
    }
}

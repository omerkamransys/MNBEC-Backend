namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// Amount class is  of response object.
    /// </summary>
    public class Amount
    {
        public decimal Value { get; set; }
        public decimal MinValue { get; set; }
        public string Currency { get; set; }
    }
}

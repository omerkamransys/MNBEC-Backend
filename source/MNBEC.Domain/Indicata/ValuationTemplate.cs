namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// ValuationTemplate class holds valiuation temaplate of response object.
    /// </summary>
    public class ValuationTemplate
    {
        public int[] Variants { get; set; }
        public int Seats { get; set; }
        public int Trim { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public string Rel { get; set; }
        public bool Templated { get; set; }
    }
}

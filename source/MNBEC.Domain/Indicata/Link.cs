namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// Link class is  of response object.
    /// </summary>
    public class Link
    {
        public string Href { get; set; }
        public string Type { get; set; }
        public string Rel { get; set; }
        public bool Templated { get; set; }
    }
}

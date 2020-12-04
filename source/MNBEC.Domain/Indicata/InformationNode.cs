namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// InformationNode class is  of response object.
    /// </summary>
    public class InformationNode
    {
        public uint[] Key { get; set; }
        public string Summary { get; set; }
        public string Name { get; set; }
        public Description[] Description { get; set; }
        public bool Assumed { get; set; }
        public Link Link { get; set; }
    }
}

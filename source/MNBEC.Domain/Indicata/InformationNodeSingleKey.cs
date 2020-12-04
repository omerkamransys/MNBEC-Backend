namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// InformationNodeSingleKey class is  of response object.
    /// </summary>
    public class InformationNodeSingleKey
    {
        public string Key { get; set; }
        public string Summary { get; set; }
        public string Name { get; set; }
        public Description[] Description { get; set; }
        public bool Assumed { get; set; }
        public Link Link { get; set; }
    }
}

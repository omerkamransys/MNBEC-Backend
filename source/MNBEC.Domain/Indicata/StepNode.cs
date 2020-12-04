namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// StepNode class is main data holder of response object.
    /// </summary>
    public class StepNode
    {
        #region Properties
        public string Summary { get; set; }
        public string Name { get; set; }
        public Description[] Description { get; set; }
        public Image[] Images { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public string Rel { get; set; }
        public bool Templated { get; set; }
        public string AcceptLangauage { get; set; }
        #endregion
    }
}

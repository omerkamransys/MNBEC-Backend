namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// IndicataResponse is Root of response object.
    /// </summary>
    public class IndicataResponse
    {
        public IndicataResponseTypeEnum ResponseType { get; set; }
        public StepNode[] NextStep { get; set; }
        public ValuationTemplate[] Valuation { get; set; }
        public Valuation[] Valuations { get; set; }
        public Odometer Odometer { get; set; }
        public Overallcondition OverallCondition { get; set; }
        public string VatReclaimable { get; set; }
        public string PriceDate { get; set; }
        public int CompetitiveVehiclesForSale { get; set; }
        public Competitivevehiclesaverageodometer CompetitiveVehiclesAverageOdometer { get; set; }
        public Competitivevehiclescriteria[] CompetitiveVehiclesCriteria { get; set; }
        public string Country { get; set; }
        public InformationNodeSingleKey Category { get; set; }
        public InformationNode Make { get; set; }
        public InformationNode Model { get; set; }
        public InformationNodeSingleKey RegDate { get; set; }
        public InformationNode Body { get; set; }
        public InformationNode Facelift { get; set; }
        public InformationNode Seats { get; set; }
        public InformationNode Engine { get; set; }
        public InformationNode Transmission { get; set; }
        public InformationNode WheelDrive { get; set; }
        public InformationNode Trim { get; set; }
        public bool UseDefaultLanguage { get; set; }
    }
}

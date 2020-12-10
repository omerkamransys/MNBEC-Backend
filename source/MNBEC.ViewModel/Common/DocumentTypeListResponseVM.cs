namespace MNBEC.API.Common.Controllers
{
    public class DocumentTypeListResponseVM
    {
        public int DocumentTypeId { get; set; }
        public int DocumentGroupId { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeNameTranslation { get; set; }
        public string DocumentTypeCode { get; set; }
        public int? OrderNumber { get; set; }
    }
}
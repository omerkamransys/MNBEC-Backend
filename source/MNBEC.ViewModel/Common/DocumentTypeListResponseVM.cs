namespace MNBEC.API.Common.Controllers
{
    public class DocumentTypeListResponseVM
    {
        public uint DocumentTypeId { get; set; }
        public uint DocumentGroupId { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeNameTranslation { get; set; }
        public string DocumentTypeCode { get; set; }
        public uint? OrderNumber { get; set; }
    }
}
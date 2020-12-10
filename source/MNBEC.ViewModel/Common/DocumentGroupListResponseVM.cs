namespace MNBEC.API.Common.Controllers
{
    public class DocumentGroupListResponseVM
    {
        public int DocumentGroupId { get; set; }
        public string DocumentGroupName { get; set; }
        public string DocumentGroupCode { get; set; }
        public int? OrderNumber { get; set; }
    }
}
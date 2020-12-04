namespace MNBEC.API.Common.Controllers
{
    public class DocumentGroupRequestVM
    {
        public uint DocumentGroupId { get; set; }
        public string DocumentGroupName { get; set; }
        public string DocumentGroupCode { get; set; }
        public uint? OrderNumber { get; set; }
    }
}
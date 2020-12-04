using MNBEC.ViewModel.Common;
using Microsoft.AspNetCore.Http;


namespace MNBEC.ViewModel.Document
{
    public class DocumentUploadRequestVM : BaseRequestVM
    {
        public string File { get; set; }
        public string ClientName { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }

    }
}

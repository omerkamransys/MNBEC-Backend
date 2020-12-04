
using MNBEC.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace MNBEC.ViewModel.Document
{
    /// <summary>
    /// DocumentRequestVM class holds basic properties for general Document request.
    /// </summary>
    public class DocumentRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public string File { get; set; }
        public string ClientName { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }

        #endregion
    }
}
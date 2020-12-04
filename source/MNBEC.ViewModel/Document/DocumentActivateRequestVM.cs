using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Document
{
    /// <summary>
    /// DocumentRequestVM class holds basic properties for general Document request.
    /// </summary>
    public class DocumentActivateRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public uint DocumentId { get; set; }
        public uint DocumentTypeId { get; set; }
        public string DocumentFile { get; set; }
        public string DocumentName { get; set; }
        public string DocumentExtension { get; set; }
        public string DocumentPath { get; set; }
        #endregion
    }
}
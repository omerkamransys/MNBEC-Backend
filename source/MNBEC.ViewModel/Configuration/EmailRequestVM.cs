using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// FormsRequestVM class holds basic properties for general Forms request.
    /// </summary>
    public class EmailRequestVM : BaseRequestVM
    {
        #region Properties and Data Members       
        public string Subject { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        #endregion
    }
}
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Document
{
    /// <summary>
    /// DocumentResponseVM class holds basic properties for full Document object response.
    /// </summary>
    public class DocumentResponseVM
    {
        #region Properties and Data Members
        
        public string Container { get; set; }
        public string Directory { get; set; }
        public string Name { get; set; }

        #endregion
    }
}
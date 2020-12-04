namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// ConfigurationRequestVM class holds basic properties for general Configuration request.
    /// </summary>
    public class ConfigurationRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public uint ConfigurationId { get; set; }
        public string ConfigurationKey { get; set; }
        #endregion
    }
}
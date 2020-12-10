namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// ConfigurationListResponseVM class holds basic properties for Configuration list object response.
    /// </summary>
    public class ConfigurationListResponseVM
    {
        #region Properties and Data Members
        public int ConfigurationId { get; set; }
        public string ConfigurationKey { get; set; }
        public string ConfigurationValue { get; set; }
        #endregion
    }
}
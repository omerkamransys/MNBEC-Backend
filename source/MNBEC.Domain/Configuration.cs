using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Configuration inherits BaseDomain and respesents Configuration object.
    /// </summary>
    public class Configuration : BaseDomain
    {
        #region Propeties
        public int ConfigurationId { get; set; }
        public string ConfigurationKey { get; set; }
        public string ConfigurationValue { get; set; }
        public string ConfigurationValueTranslation { get; set; }

        #endregion
    }
}

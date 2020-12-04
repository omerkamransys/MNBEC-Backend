namespace MNBEC.Core.Model
{
    using Interface;

    /// <summary>
    /// HeaderValue implements IHeaderValue interface and serves as header properties.
    /// </summary>
    public class HeaderValue : IHeaderValue
    {
        #region Properties
        public string ApplicationKey { get; set; }
        public string AcceptLangauage { get; set; }
        #endregion
    }
}
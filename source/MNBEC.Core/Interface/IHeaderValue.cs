namespace MNBEC.Core.Interface
{
    /// <summary>
    /// IHeaderValue interface provides required header properties.
    /// </summary>
    public interface IHeaderValue
    {
        #region Properties
        string ApplicationKey { get; set; }
        string AcceptLangauage { get; set; }
        #endregion
    }
}
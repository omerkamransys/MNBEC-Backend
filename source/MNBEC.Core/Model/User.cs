namespace MNBEC.Core.Model
{
    using Interface;

    /// <summary>
    /// HeaderValue implements IHeaderValue interface and serves as header properties.
    /// </summary>
    public class User : IUser
    {
        #region Properties
       public uint UserId { get; set; }      
        #endregion
    }
}
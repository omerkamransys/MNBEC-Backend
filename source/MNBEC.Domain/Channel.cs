using MNBEC.Domain.Common;


namespace MNBEC.Domain
{
    /// <summary>
    /// Make inherits BaseDomain and respesents Vehicle Make object.
    /// </summary>
    public class Channel : BaseDomain
    {
        #region Propeties
        public uint ChannelId { get; set; }
        public uint ClientId { get; set; }
        public string ChannelName { get; set; }
        public string Description { get; set; }
       
        #endregion
    }
}

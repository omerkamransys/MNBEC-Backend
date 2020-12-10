using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelRequestVM class holds basic properties for general Channel request.
    /// </summary>
    public class ChannelRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public int ChannelId { get; set; }
        public int ClientId { get; set; }   
        public string Description { get; set; }
        public string ChannelName { get; set; }
        #endregion
    }
}
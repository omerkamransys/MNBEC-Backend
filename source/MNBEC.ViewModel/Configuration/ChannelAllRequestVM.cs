using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelAllRequestVM class holds basic properties for Channel get all request.
    /// </summary>
    public class ChannelAllRequestVM : BaseAllRequestVM
    {
        #region Properties and Data Members
        public int ChannelId { get; set; }
        public int ClientId { get; set; }
        public string ChannelName { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
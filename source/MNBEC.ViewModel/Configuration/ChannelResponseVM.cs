using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelResponseVM class holds basic properties for full Channel object response.
    /// </summary>
    public class ChannelResponseVM : BaseResponseVM
    {
        #region Properties and Data Members
        public uint ChannelId { get; set; }       
        public string ChannelName { get; set; }
        public string Description { get; set; }

        #endregion
    }
}
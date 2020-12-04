namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelListResponseVM class holds basic properties for Channel list object response.
    /// </summary>
    public class ChannelListResponseVM
    {
        #region Properties and Data Members
        public uint ChannelId { get; set; }
        public uint ClientId { get; set; }
        public string ChannelName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}
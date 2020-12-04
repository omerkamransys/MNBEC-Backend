namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelAllResponseVM class holds basic properties for Channel object Get all response.
    /// </summary>
    public class ChannelAllResponseVM
    {
        #region Properties and Data Members
        public uint ChannelId { get; set; }
        public uint ClientId { get; set; }

        public string ChannelName { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
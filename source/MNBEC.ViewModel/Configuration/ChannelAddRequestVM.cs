using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelAddRequestVM class holds basic properties for general Channel request.
    /// </summary>
    public class ChannelAddRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public uint ChannelId { get; set; }
        [Required]
        public uint ClientId { get; set; }
        public string ChannelName { get; set; }
        public string Description { get; set; }

        #endregion
    }
}
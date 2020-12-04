using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Configuration
{
    /// <summary>
    /// ChannelActivateRequestVM class holds basic properties for general Channel request.
    /// </summary>
    public class ChannelActivateRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        [Required]
        [Range(1, uint.MaxValue)]
        public uint ChannelId { get; set; }
        #endregion
    }
}
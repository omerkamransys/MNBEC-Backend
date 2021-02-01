using MNBEC.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MNBEC.ViewModel.LookUp
{
    public class LookUpRequestVM : BaseDomain
    {
        #region Propeties
        public int Id { get; set; }
        [Required]
        [StringLength(512, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Title { get; set; }

        #endregion
    }
}

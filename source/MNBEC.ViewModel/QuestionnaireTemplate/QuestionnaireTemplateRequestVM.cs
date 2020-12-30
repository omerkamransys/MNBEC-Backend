using MNBEC.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MNBEC.ViewModel.QuestionnaireTemplate
{
    public class QuestionnaireTemplateRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
       
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
       

        #endregion
    }
}

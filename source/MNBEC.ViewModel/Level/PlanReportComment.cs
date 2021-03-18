using MNBEC.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel.Level
{
    public class PlanReportComment : BaseDomain
    {
        #region Propeties
        public int Id { get; set; }
        public int LevelId { get; set; }
       
        public string Strengths { get; set; }
        public string OFI { get; set; }
        public string Opportunities { get; set; }
        public string Recommendations { get; set; }
        public string Conclusion { get; set; }

        #endregion
    }
}

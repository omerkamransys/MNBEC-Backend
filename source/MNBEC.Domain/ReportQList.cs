using MNBEC.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Domain
{
    public class ReportQList : BaseDomain
    {
        #region Propeties
        public string Title { get; set; }
        public decimal Desired { get; set; }
        public decimal Current { get; set; }
        

        #endregion
    }
}

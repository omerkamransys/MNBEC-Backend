using MNBEC.ViewModel.ReportResponseVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel
{
    public class FourP
    {
        public int FounrPId { get; set; }
        public int Max { get; set; }
        public decimal Desired { get; set; }
        public decimal Current { get; set; }
        public decimal DesiredPST
        {
            get { return Desired > 0 ? Current / Desired : 0; }
        }
        public decimal TotalPST
        {
            get { return Max > 0 ? Current / Max : 0; }
        }

        public static implicit operator FourP(SPResponseVM v)
        {
            throw new NotImplementedException();
        }
    }
}

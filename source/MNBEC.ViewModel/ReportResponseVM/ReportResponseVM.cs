using MNBEC.Domain;
using System;
using System.Collections.Generic;
using System.Text;
namespace MNBEC.ViewModel.ReportResponseVM
{
    public class ReportResponseVM
    {
        public List<FourP> Report { get; set; }
        public List<ReportQList> ReportQList { get; set; } = new List<ReportQList>();
    }
}

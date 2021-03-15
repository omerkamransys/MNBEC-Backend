using MNBEC.Domain;
using System;
using System.Collections.Generic;
using System.Text;
namespace MNBEC.ViewModel.ReportResponseVM
{
    public class ParentReportResponseVM
    {
        public int LevelId;
        public int? ParentId;
        public decimal wf;
        public string modelTitle { get; set; }
        public List<ParentReportResponseVM> ChilderenList { get; set; }
        public List<FourP> FourPReport { get; set; } = new List<FourP>();
        public FourP TotalFourPReport { get; set; } = new FourP();

    }
    public class SPResponseVM
    {
        public int LevelId;
        public int ParentId;
        public int FourPId;
        public decimal Max;
        public decimal Desired;
        public decimal Current;
        public decimal wf;
        public string modelTitle { get; set; }
    }
}

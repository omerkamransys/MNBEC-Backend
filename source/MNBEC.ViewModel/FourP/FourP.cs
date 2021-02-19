using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel
{
    public class FourP
    {
        public int FounrPId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public decimal Avg
        {
            get { return Count > 0 ?  Sum/Count : 0; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Domain.MLAPIQuatationRequest
{
    public class MLAPIQuatationResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public decimal?[] Results { get; set; }
    }
}

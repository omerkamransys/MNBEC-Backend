using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Domain.MLAPIQuatationRequest
{
    public class MLAPIQuatationObject
    {

        //Note: keys are camelCased as they are required like this
        public string brand { get; set; }
        public string model { get; set; }
        public ushort? year { get; set; }
        public string trim { get; set; }

        public string transmission { get; set; }
        public string color { get; set; }
        public double mileage { get; set; }
        public string registration { get; set; }
        public string date { get; set; }
    }

}

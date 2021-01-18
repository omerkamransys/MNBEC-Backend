using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.InfrastructureInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Infrastructure
{
    public class AnswerInfrastructure : BaseSQLInfrastructure, IAnswerInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  AnswerInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AnswerInfrastructure(IConfiguration configuration, ILogger<AnswerInfrastructure> logger) : base(configuration, logger)
        {

        }
        
        #endregion

    }
}

using MNBEC.Domain.Common;
using System;

namespace MNBEC.Domain
{
    /// <summary>
    /// A model for the database table claims
    /// </summary>
    public class UserActivity 
    {
        public uint UserId { get; set; }
        public string UserName { get; set; }
        public uint TotalRecords { get; set; }
        public string UserActivityName { get; set; }
        public string UserActivityDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        
    }
}

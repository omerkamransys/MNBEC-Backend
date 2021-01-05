using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    public class StakeholderLevel : BaseDomain
    {
        public int StakeholderLevelId { get; set; }
        public int UserId { get; set; }
        public int LevelId { get; set; }
    }
}


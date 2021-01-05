using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    public class ReviewerLevel : BaseDomain
    {
        public int ReviewerLevelId { get; set; }
        public int UserId { get; set; }
        public int LevelId { get; set; }
    }
}

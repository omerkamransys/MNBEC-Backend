using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// AuctionUpdateAllStatus inherits BaseDomain and respesents Auction Update All Status domain object.
    /// </summary>
    public class BulkInsertResponse : BaseDomain
    {
        #region Propeties 

        public uint FirstId { get; set; }
        public int RowCount { get; set; }
        #endregion

    }
}

namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// BaseAllRequestVM class holds required propeties for GetAll request.
    /// </summary>
    public class BaseAllRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public uint Offset { get; set; }
        public uint PageSize { get; set; }
        public string SortColumn { get; set; }
        public bool SortAscending { get; set; }
        public uint FilterColumnId { get; set; }
        public string ColumnName { get; set; }
        public string SearchText { get; set; }
        public bool SearchAll { get; set; }
        #endregion
    }
}
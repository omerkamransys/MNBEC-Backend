using System.Collections.Generic;

namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// AllResponseVM T class is used as response to GetAll calls. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AllResponseVM<T>
    {
        #region Properties and Data Members
        public List<T> Data { get; set; }
        public uint Offset { get; set; }
        public uint PageSize { get; set; }
        public uint TotalRecord { get; set; }
        public string SortColumn { get; set; }
        public bool SortAscending { get; set; }
        public string SearchText { get; set; }
        #endregion
    }
}
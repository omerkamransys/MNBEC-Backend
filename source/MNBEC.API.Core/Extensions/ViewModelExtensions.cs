using System.Collections.Generic;
using MNBEC.Domain.Common;
using MNBEC.ViewModel.Common;

namespace MNBEC.API.Core.Extensions
{
    /// <summary>
    /// ViewModelExtensions class provides implementation for extension methods for Common ViewModel and Domain classes.
    /// </summary>
    public static class ViewModelExtensions
    {
        #region Common
        /// <summary>
        /// Convert converts object of BaseRequestAllVM type to AllRequest.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static AllRequest<T> Convert<T>(this BaseAllRequestVM vm) where T : new()
        {
            var request = new AllRequest<T>
            {
                Data = new T(),
                Offset = vm.Offset,
                PageSize = vm.PageSize > 0 ? vm.PageSize : int.MaxValue,
                SortColumn = vm.SortColumn,
                SortAscending = vm.SortAscending,
                FilterColumnId = vm.FilterColumnId
            };

            return request;
        }

        /// <summary>
        /// Convert converts object of AllResponse type to AllResponseVM type.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AllResponseVM<U> Convert<T, U>(this AllResponse<T> model)
        {
            var response = new AllResponseVM<U>
            {
                Data = new List<U>(),
                Offset = model.Offset,
                PageSize = model.PageSize,
                TotalRecord = model.TotalRecord,
                SortColumn = model.SortColumn,
                SortAscending = model.SortAscending
            };

            return response;
        }

        /// <summary>
        /// GetLanguageText returns text based of default selection.
        /// </summary>
        /// <param name="useDefault"></param>
        /// <param name="defaultText"></param>
        /// <param name="translationText"></param>
        /// <returns></returns>
        public static string GetLanguageText(this bool useDefault, string defaultText, string translationText)
        {
            if (useDefault)
            {
                return defaultText;
            }
            else
            {
                return !string.IsNullOrWhiteSpace(translationText) ? translationText : defaultText;
            }
        }
        #endregion
    }
}
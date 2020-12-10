using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.Domain;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// MakeRequestVM class holds basic properties for general make request.
    /// </summary>
    public class UserGetByRoleRequestVM : BaseRequestVM
    {
        #region Propeties
        public int RoleId { get; set; }

        #endregion
    }
}

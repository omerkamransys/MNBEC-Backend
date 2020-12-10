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
    public class UserRequestVM : BaseRequestVM
    {
        #region Propeties
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int UserTypeId { get; set; }
        public string Role { get; set; }
        public string IdentificationNumber { get; set; }

        public string Code { get; set; }
        public IEnumerable<UserRoles> Roles { get; set; }
        #endregion
    }
}

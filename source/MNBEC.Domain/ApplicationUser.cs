using System;
using System.Collections.Generic;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// applicationuser inherits BaseDomain and respesents Vehicle applicationuser object.
    /// </summary>
    public class ApplicationUser : BaseDomain
    {
        #region Propeties
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DealershipName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string ResetUrlKey { get; set; }

        public string IdentificationNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }

        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<ApplicationRole> Roles { get; set; }
        public IEnumerable<UserRoles> UserRoles { get; set; }
        public IList<UserRoles> ApplicationUserRoles { get; set; }
        public bool? ActiveColumn { get; set; }
        public string UserTypeCode { get; set; }

        #endregion
    }
}

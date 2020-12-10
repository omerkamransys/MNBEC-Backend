using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
   public class UserResponseVM 
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public bool Active { get; set; }
        public string PhoneNumber { get; set; }

        public List<RoleListResponseVM> Roles { get; set; }
        public string IdentificationNumber { get; set; }
    }
}

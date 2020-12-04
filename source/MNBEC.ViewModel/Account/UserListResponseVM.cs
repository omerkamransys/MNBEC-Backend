using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel.Account
{
    public class UserListResponseVM
    {
        public uint UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentificationNumber { get; set; }
    }
}

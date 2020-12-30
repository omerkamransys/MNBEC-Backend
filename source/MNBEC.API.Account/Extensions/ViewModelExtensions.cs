using System.Collections.Generic;
using System.Linq;
using MNBEC.API.Core.Extensions;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.ViewModel.Account;
using MNBEC.ViewModel.Common;

namespace MNBEC.API.Account.Extensions
{
    public static class ViewModelExtensions
    {
   
        #region ApplicationUser
        /// <summary>
        /// Convert converts object of RequestMakeVM type to Make type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationUser Convert(this UserRequestVM vm)
        {
            var model = new ApplicationUser
            {
                UserId = vm.UserId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                EmailConfirmed = vm.EmailConfirmed,
                UserName = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                Active = vm.Active,
                CurrentUserId = vm.CurrentUserId,
                UserTypeId = vm.UserTypeId,
                UserRoles = vm.Roles,
                IdentificationNumber = vm.IdentificationNumber

            };

            return model;
        }

        public static ApplicationUser Convert(this UserGetByRoleRequestVM vm)
        {
            var model = new ApplicationUser
            {
                RoleId = vm.RoleId
            };

            return model;
        }

        public static ApplicationUser ConvertAdd(this UserRequestVM vm)
        {
            var model = new ApplicationUser
            {

                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                EmailConfirmed = vm.EmailConfirmed,
                UserName = vm.Email,
                PhoneNumber = vm.PhoneNumber,
                Active = vm.Active,
                CurrentUserId = vm.CurrentUserId,
                UserTypeId = vm.UserTypeId,
                UserRoles = vm.Roles,
                IdentificationNumber = vm.IdentificationNumber ?? ""

            };

            return model;
        }


      
        //private static string GenerateResetLink(this ApplicationUser user)
        //{
        //    return "?userId=" + user.UserId + "&code=" ;
        //}

        //private static string GetResetMessage(string callbackUrl)
        //{
        //    return $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>";
        //}
        #endregion

        #region ApplicationRole
        /// <summary>
        /// Convert converts object of RequestMakeVM type to State type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationRole Convert(this RoleRequestVM vm)
        {
            var model = new ApplicationRole
            {
                RoleId = vm.RoleId,
                RoleName = vm.RoleName,
                Active = vm.Active,
                CurrentUserId = vm.CurrentUserId
            };

            return model;
        }

        /// <summary>
        /// ConvertActivate converts object of RoleActivateRequestVM type to State type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationRole ConvertActivate(this RoleActivateRequestVM vm)
        {
            var model = new ApplicationRole
            {
                RoleId = vm.RoleId,
                Active = vm.Active,
                CurrentUserId = vm.CurrentUserId
            };

            return model;
        }

        /// <summary>
        /// ConvertAdd converts object of RoleAddRequestVM type to State type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationRole ConvertAdd(this RoleAddRequestVM vm)
        {
            var model = new ApplicationRole
            {
                RoleId = vm.RoleId,
                RoleName = vm.RoleName,
                RoleNameCode = vm.RoleNameCode,
                RoleNameTranslation = vm.RoleNameTranslation ?? "",
                Active = vm.Active,
                CurrentUserId = vm.CurrentUserId
            };

            return model;
        }

        /// <summary>
        /// ConvertAdd converts object of RoleAddRequestVM type to State type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationRole ConvertUpdate(this RoleUpdateRequestVM vm)
        {
            var model = new ApplicationRole
            {
                RoleId = vm.RoleId,
                RoleName = vm.RoleName,
                RoleNameCode = vm.RoleNameCode,
                RoleNameTranslation = vm.RoleNameTranslation,
                CurrentUserId = vm.CurrentUserId,
                Active = vm.Active,
                ClaimGroups = new List<ApplicationClaimGroup>()
            };

            if (vm.ClaimGroups != null)
            {
                foreach (var claimGroupVM in vm.ClaimGroups)
                {
                    model.ClaimGroups.Add(claimGroupVM.ConvertUpdate());
                }
            }

            return model;
        }

        /// <summary>
        /// Convert converts ApplicationRole object to RoleResponseVM object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RoleResponseVM Convert(this ApplicationRole model, bool useLanguage)
        {
            var vm = new RoleResponseVM
            {
                RoleId = model.RoleId,
                RoleName = useLanguage.GetLanguageText(model.RoleName, model.RoleNameTranslation),
                RoleNameCode = model.RoleNameCode,
                RoleNameTranslation = model.RoleNameTranslation,
                Active = model.Active,
                ClaimGroups = new List<ClaimGroupResponseVM>()
            };

            if (model.ClaimGroups != null)
            {
                foreach (var claimGroup in model.ClaimGroups)
                {
                    vm.ClaimGroups.Add(claimGroup.Convert(useLanguage));
                }
            }

            return vm;
        }

        /// <summary>
        /// Convert converts object of State type to RequestMakeVM type.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<RoleListResponseVM> ConvertList(this List<ApplicationRole> models, bool useLanguage)
        {
            List<RoleListResponseVM> vms = new List<RoleListResponseVM>();

            foreach (var model in models)
            {
                vms.Add(
                    new RoleListResponseVM
                    {
                        RoleId = model.RoleId,
                        RoleName = useLanguage.GetLanguageText(model.RoleName, model.RoleNameTranslation),
                        RoleCode = model.RoleNameCode
                    });
            }

            return vms;
        }

        public static UserResponseVM Convert(this ApplicationUser model,bool UseLanguage)
        {
            var vm = new UserResponseVM
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserTypeId = model.UserTypeId,
                UserTypeName = model.UserTypeName,
                IdentificationNumber = model.IdentificationNumber,
                Active = model.Active,
                PhoneNumber = model.PhoneNumber,
                Roles = ConvertList(model.Roles.ToList(), UseLanguage)
            };

            return vm;
        }
        public static AllRequest<ApplicationUser> ConvertAll(this UserAllRequestVM allRequestVM)
        {
            var allRequest = allRequestVM.Convert<ApplicationUser>();

            allRequest.Data.FirstName = allRequestVM.FirstName;
            allRequest.Data.UserId = allRequestVM.UserId;
            allRequest.Data.LastName = allRequestVM.LastName;
            allRequest.Data.CurrentUserId = allRequestVM.CurrentUserId;
            allRequest.Data.UserTypeId = allRequestVM.UserTypeId;
            allRequest.Data.ActiveColumn = allRequestVM.ActiveColumn ;
            allRequest.Data.RoleId = allRequestVM.RoleId;
            allRequest.Data.SearchText = allRequestVM.SearchText ?? string.Empty;


            return allRequest;
        }
        public static AllRequest<ApplicationRole> ConvertAll(this RoleAllRequestVM allRequestVM)
        {
            var allRequest = allRequestVM.Convert<ApplicationRole>();

            allRequest.Data.RoleId = allRequestVM.RoleId;
            allRequest.Data.RoleName = allRequestVM.RoleName;
            allRequest.Data.Active = allRequestVM.Active;
            allRequest.Data.CurrentUserId = allRequestVM.CurrentUserId;
            return allRequest;
        }
        public static AllResponseVM<UserAllResponseVM> ConvertAll(this AllResponse<ApplicationUser> allResponse)
        {
            var allResponseVM = allResponse.Convert<ApplicationUser, UserAllResponseVM>();

            allResponseVM.Data = new List<UserAllResponseVM>();

            foreach (var dataItem in allResponse.Data)
            {
                allResponseVM.Data.Add(
                        new UserAllResponseVM
                        {
                            UserId = dataItem.UserId,
                            FirstName = dataItem.FirstName,
                            LastName = dataItem.LastName,
                            Email = dataItem.Email,
                            UserTypeId = dataItem.UserTypeId,
                            UserTypeName = dataItem.UserTypeName,
                            Active = dataItem.Active,
                            UserRoles = dataItem.ApplicationUserRoles.ToList()

                        }
                    );
            }

            return allResponseVM;
        }
        public static AllResponseVM<RoleAllResponseVM> ConvertAll(this AllResponse<ApplicationRole> allResponse,bool useLanguage)
        {
            var allResponseVM = allResponse.Convert<ApplicationRole, RoleAllResponseVM>();

            allResponseVM.Data = new List<RoleAllResponseVM>();

            foreach (var dataItem in allResponse.Data)
            {
                allResponseVM.Data.Add(
                        new RoleAllResponseVM
                        {
                            RoleId = dataItem.RoleId,
                            RoleName = useLanguage.GetLanguageText(dataItem.RoleName, dataItem.RoleNameTranslation),
                            Active = dataItem.Active
                        }
                    );
            }

            return allResponseVM;
        }
        public static List<UserListResponseVM> ConvertList(this List<ApplicationUser> models)
        {
            List<UserListResponseVM> vms = new List<UserListResponseVM>();

            foreach (var model in models)
            {
                vms.Add(
                    new UserListResponseVM
                    {
                        UserId = model.UserId,
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        IdentificationNumber = model.IdentificationNumber
                    });
            }

            return vms;
        }


        #endregion State

        #region ApplicationClaim
        /// <summary>
        /// Convert converts object of RequestApplicationClaimVM type to ApplicationClaim type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationClaim Convert(this ClaimListRequestVM vm)
        {
            var model = new ApplicationClaim
            {
                ClaimId = vm.ClaimId,
                ClaimGroupId = vm.ClaimGroupId,
                ClaimType = vm.ClaimType,
                ClaimLabel = vm.ClaimLabel,
                CurrentUserId = vm.CurrentUserId
            };

            return model;
        }

        ///// <summary>
        ///// Convert converts object of RequestApplicationClaimVM type to ApplicationClaim type.
        ///// </summary>
        ///// <param name="vm"></param>
        ///// <returns></returns>
        //public static ApplicationRole Convert(this ApplicationClaimListByRoleRequestVM vm)
        //{
        //    var model = new ApplicationClaim
        //    {
        //        CreatedById = vm.CurrentUserId
        //    };

        //    return model;
        //}

        ///// <summary>
        ///// Convert converts object of RequestApplicationClaimVM type to ApplicationClaim type.
        ///// </summary>
        ///// <param name="vm"></param>
        ///// <returns></returns>
        //public static ApplicationClaim Convert(this ApplicationClaimListRequestVM vm)
        //{
        //    var model = new ApplicationClaim
        //    {
        //        ClaimId = vm.ClaimId,
        //        ClaimType = vm.ClaimType,
        //        ClaimLabel = vm.ClaimLabel,
        //        ClaimGroupLable = vm.ClaimGroupLable,
        //        CreatedById = vm.CurrentUserId
        //    };

        //    return model;
        //}

        ///// <summary>
        ///// Convert converts object of ApplicationClaim type to RequestApplicationClaimVM type.
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static ApplicationClaimResponseVM Convert(this ApplicationClaim model)
        //{
        //    ApplicationClaimResponseVM vm = null;

        //    if (model != null)
        //    {
        //        vm = new ApplicationClaimResponseVM
        //        {
        //            ClaimId = model.ClaimId,
        //            ClaimType = model.ClaimType,
        //            CreatedById = model.CreatedById,
        //            CreatedByName = model.CreatedByName,
        //            CreatedDate = model.CreatedDate,
        //            ModifiedById = model.ModifiedById,
        //            ModifiedByName = model.ModifiedByName,
        //            ModifiedDate = model.ModifiedDate
        //        };
        //    }

        //    return vm;
        //}

        /// <summary>
        /// Convert converts object of ApplicationClaim type to RequestApplicationClaimVM type.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<ClaimListResponseVM> ConvertList(this List<ApplicationClaim> models,bool useLanguage)
        {
            List<ClaimListResponseVM> vms = new List<ClaimListResponseVM>();

            foreach (var model in models)
            {
                vms.Add(
                    new ClaimListResponseVM
                    {
                        ClaimId = model.ClaimId,
                        //ClaimGroupId = model.ClaimGroupId,
                        ClaimType = model.ClaimType,
                        ClaimLabel = useLanguage.GetLanguageText(model.ClaimLabel, model.ClaimLabelTranslation),
                        ClaimCode = model.ClaimCode
                    });
            }

            return vms;
        }

        ///// <summary>
        ///// Convert converts object of ApplicationClaim type to RequestApplicationClaimVM type.
        ///// </summary>
        ///// <param name="models"></param>
        ///// <returns></returns>
        //public static List<ClaimListRoleResponseVM> ConvertListByRole(this List<ApplicationClaim> models)
        //{
        //    List<ClaimListRoleResponseVM> vms = new List<ClaimListRoleResponseVM>();

        //    foreach (var model in models)
        //    {
        //        vms.Add(
        //            new ClaimListRoleResponseVM
        //            {
        //                ClaimId = model.ClaimId
        //            });
        //    }

        //    return vms;
        //}

        ///// <summary>
        ///// Convert converts object of ApplicationClaim type to RequestApplicationClaimVM type.
        ///// </summary>
        ///// <param name="models"></param>
        ///// <returns></returns>
        //public static List<ClaimListUserResponseVM> ConvertListByUser(this List<ApplicationClaim> models)
        //{
        //    List<ClaimListUserResponseVM> vms = new List<ClaimListUserResponseVM>();

        //    foreach (var model in models)
        //    {
        //        vms.Add(
        //            new ClaimListUserResponseVM
        //            {
        //                ClaimId = model.ClaimId,
        //                ClaimType = model.ClaimType,
        //                ClaimGroupLable = model.ClaimGroupLable
        //            });
        //    }

        //    return vms;
        //}

        ///// <summary>
        ///// Convert converts object of RequestApplicationClaimAllVM type to AllRequest ApplicationClaim.
        ///// </summary>
        ///// <param name="allRequestVM"></param>
        ///// <returns></returns>
        //public static AllRequest<ApplicationClaim> ConvertAll(this ApplicationClaimAllRequestVM allRequestVM)
        //{
        //    var allRequest = allRequestVM.Convert<ApplicationClaim>();

        //    allRequest.Data.ClaimId = allRequestVM.ClaimId;
        //    allRequest.Data.ClaimType = allRequestVM.ClaimType;
        //    allRequest.Data.CreatedById = allRequestVM.CurrentUserId;

        //    return allRequest;
        //}

        ///// <summary>
        ///// Convert converts object of AllResponse ApplicationClaim type to AllResponseVM ResponseApplicationClaimAllVM.
        ///// </summary>
        ///// <param name="allResponse"></param>
        ///// <returns></returns>
        //public static AllResponseVM<ApplicationClaimAllResponseVM> ConvertAll(this AllResponse<ApplicationClaim> allResponse)
        //{
        //    var allResponseVM = allResponse.Convert<ApplicationClaim, ApplicationClaimAllResponseVM>();

        //    allResponseVM.Data = new List<ApplicationClaimAllResponseVM>();

        //    foreach (var dataItem in allResponse.Data)
        //    {
        //        allResponseVM.Data.Add(
        //                new ApplicationClaimAllResponseVM
        //                {
        //                    ClaimId = dataItem.ClaimId,
        //                    ClaimType = dataItem.ClaimType
        //                }
        //            );
        //    }

        //    return allResponseVM;
        //}
        #endregion

        #region ApplicationClaimGroup
        /// <summary>
        /// Convert converts object of RequestApplicationClaimGroupVM type to ApplicationClaimGroup type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static ApplicationClaimGroup Convert(this ClaimGroupRequestVM vm)
        {
            var model = new ApplicationClaimGroup
            {
                ClaimGroupId = vm.ClaimGroupId,
                ClaimGroupLabel = vm.ClaimGroupLabel,
                ClaimGroupCode = vm.ClaimGroupCode,
                CurrentUserId = vm.CurrentUserId
            };

            return model;
        }

        ///// <summary>
        ///// Convert converts object of ApplicationClaimGroup type to RequestApplicationClaimGroupVM type.
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static ApplicationClaimGroupResponseVM Convert(this ApplicationClaimGroup model)
        //{
        //    ApplicationClaimGroupResponseVM vm = null;

        //    if (model != null)
        //    {
        //        vm = new ApplicationClaimGroupResponseVM
        //        {
        //            ApplicationClaimGroupId = model.ApplicationClaimGroupId,
        //            ApplicationClaimGroupName = model.ApplicationClaimGroupName,
        //            CreatedById = model.CreatedById,
        //            CreatedByName = model.CreatedByName,
        //            CreatedDate = model.CreatedDate,
        //            ModifiedById = model.ModifiedById,
        //            ModifiedByName = model.ModifiedByName,
        //            ModifiedDate = model.ModifiedDate
        //        };
        //    }

        //    return vm;
        //}

        /// <summary>
        /// Convert converts object of ApplicationClaimGroup type to RequestApplicationClaimGroupVM type.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<ClaimGroupListResponseVM> ConvertList(this List<ApplicationClaimGroup> models, bool Uselanguage)
        {
            List<ClaimGroupListResponseVM> vms = new List<ClaimGroupListResponseVM>();

            foreach (var model in models)
            {
                vms.Add(
                    new ClaimGroupListResponseVM
                    {
                        ClaimGroupId = model.ClaimGroupId,
                        ClaimGroupLabel = Uselanguage.GetLanguageText(model.ClaimGroupLabel, model.ClaimGroupLabelTranslation),
                        ClaimGroupCode = model.ClaimGroupCode
                    });
            }

            return vms;
        }

        /// <summary>
        /// Convert converts object of RequestApplicationClaimGroupAllVM type to AllRequest ApplicationClaimGroup.
        /// </summary>
        /// <param name="allRequestVM"></param>
        /// <returns></returns>
        public static AllRequest<ApplicationClaimGroup> ConvertAll(this ClaimGroupAllRequestVM allRequestVM)
        {
            var allRequest = allRequestVM.Convert<ApplicationClaimGroup>();

            allRequest.Data.ClaimGroupId = allRequestVM.ApplicationClaimGroupId;
            allRequest.Data.ClaimGroupLabel = allRequestVM.ApplicationClaimGroupName;
            allRequest.Data.ClaimGroupCode = allRequestVM.ApplicationClaimGroupCode;
            allRequest.Data.CurrentUserId = allRequestVM.CurrentUserId;

            return allRequest;
        }

        /// <summary>
        /// Convert converts object of AllResponse ApplicationClaimGroup type to AllResponseVM ResponseApplicationClaimGroupAllVM.
        /// </summary>
        /// <param name="allResponse"></param>
        /// <returns></returns>
        public static AllResponseVM<ClaimGroupResponseVM> ConvertAll(this AllResponse<ApplicationClaimGroup> allResponse,bool useLanguage)
        {
            var allResponseVM = allResponse.Convert<ApplicationClaimGroup, ClaimGroupResponseVM>();

            allResponseVM.Data = new List<ClaimGroupResponseVM>();

            foreach (var dataItem in allResponse.Data)
            {
                allResponseVM.Data.Add(dataItem.Convert(useLanguage));
            }

            return allResponseVM;
        }

        /// <summary>
        /// Convert converts object of ApplicationClaimGroup type to ClaimGroupResponseVM.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static ClaimGroupResponseVM Convert(this ApplicationClaimGroup model, bool useLanguage)
        {
            ClaimGroupResponseVM claimGroupVM = new ClaimGroupResponseVM
            {
                ClaimGroupId = model.ClaimGroupId,
                ClaimGroupLabel = useLanguage.GetLanguageText(model.ClaimGroupLabel, model.ClaimGroupLabelTranslation),
                ClaimGroupCode = model.ClaimGroupCode,
                Claims = new List<ClaimResponseVM>()
            };

            if (model.Claims != null)
            {
                foreach (var claimItem in model.Claims)
                {
                    claimGroupVM.Claims.Add(new ClaimResponseVM
                    {
                        ClaimId = claimItem.ClaimId,
                        ClaimGroupId = claimItem.ClaimGroupId,
                        ClaimLabel = useLanguage.GetLanguageText(claimItem.ClaimLabel, claimItem.ClaimLabelTranslation),
                        ClaimType = claimItem.ClaimType,
                        ClaimCode = claimItem.ClaimCode,
                        Active = claimItem.Active
                    });
                }
            }

            return claimGroupVM;
        }

        /// <summary>
        /// ConvertUpdate converts object of ClaimGroupResponseVM type to ApplicationClaimGroup.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private static ApplicationClaimGroup ConvertUpdate(this ClaimGroupResponseVM vm)
        {
            ApplicationClaimGroup model = new ApplicationClaimGroup
            {
                ClaimGroupId = vm.ClaimGroupId,
                ClaimGroupLabel = vm.ClaimGroupLabel,
                ClaimGroupCode = vm.ClaimGroupCode,
                Claims = new List<ApplicationClaim>()
            };

            if (vm.Claims != null)
            {
                foreach (var claimItem in vm.Claims)
                {
                    model.Claims.Add(new ApplicationClaim
                    {
                        ClaimId = claimItem.ClaimId,
                        ClaimGroupId = claimItem.ClaimGroupId,
                        ClaimLabel = claimItem.ClaimLabel,
                        ClaimType = claimItem.ClaimType,
                        ClaimCode = claimItem.ClaimCode,
                        Active = claimItem.Active
                    });
                }
            }

            return model;
        }
        #endregion

       

        
    }
}

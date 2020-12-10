using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// UserInfrastructure inherits from BaseDataAccess and implements IUserInfrastructure. It performs all required CRUD operations on ApplicationUser Entity on database.
    /// </summary>
    public class UserInfrastructure : BaseSQLInfrastructure, IUserInfrastructure, IUserStore<ApplicationUser>, IUserEmailStore<ApplicationUser>, IUserPhoneNumberStore<ApplicationUser>,
            IUserTwoFactorStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        #region Constructor
        /// <summary>
        ///  ApplicationUserfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UserInfrastructure(IConfiguration configuration, ILogger<UserInfrastructure> logger) : base(configuration, logger)
        {

        }
        #endregion

        #region Constants
        private const string GetRoleIdStoredProcedureName = "ApplicationRoleIdGet";
        private const string UserRoleCheckStoredProcedureName = "ApplicationUserRoleCheck";
        private const string AddStoredProcedureName = "ApplicationUserAdd";
        private const string AddUserRoleStoredProcedureName = "ApplicationUserRoleAdd";
        private const string ActivateStoredProcedureName = "ApplicationUserActivate";
        private const string GetStoredProcedureName = "ApplicationUserGet";
        private const string GetAllStoredProcedureName = "ApplicationUserGetAllByUserType";
        private const string GetListStoredProcedureName = "ApplicationUserGetList";
        private const string UpdateStoredProcedureName = "ApplicationUserUpdate";
        private const string RemoveExistingRolesForUserStoredProcedureName = "ApplicationUserRolesInactive";
        private const string ApplicationUserRoleGetByUserStoredProcedureName = "ApplicationUserRoleGetByUser";
        private const string GetbyEmailStoredProcedureName = "ApplicationUserGetbyEmail";
        private const string UserTypeGetbyEmailStoredProcedureName = "UserTypeGetbyEmail";
        private const string GetDealerbyEmailStoredProcedureName = "ApplicationUserGetDealerbyEmail";
        private const string GetInspectorbyEmailStoredProcedureName = "ApplicationUserGetInspectorbyEmail";
        private const string GetbyUserNameStoredProcedureName = "ApplicationUserGetbyName";
        private const string GetUserByIdStoredProcedureName = "GetUserById";
        private const string GetUserWithRolesByEmailStoredProcedureName = "GetUserWithRolesByEmail";
        private const string ApplicationUserGetByRoleStoredProcedureName = "ApplicationUserGetByRole";
        private const string GetAdminsStoredProcedureName = "GetAdmins";
        private const string ApplicationUserSearchStoredProcedureName = "ApplicationUserSearch";
        private const string SetPasswordStoredProcedureName = "SetPasswordHash";
        private const string GetPasswordStoredProcedureName = "GetPasswordHash";

        private const string ApplicationRoleIdColumnName = "RoleId";
        private const string ApplicationRoleNameColumnName = "RoleName";
        private const string ApplicationRoleCodeColumnName = "RoleNameCode";
        private const string ApplicationRoleNameTranslationColumnName = "RoleNameTranslation";
        private const string ApplicationUserIdColumnName = "UserId";
        private const string ApplicationUserNameColumnName = "UserName";
        private const string FirstNameColumnName = "FirstName";
        private const string DealershipNameColumnName = "DealershipName";
        private const string LastNameColumnName = "LastName";
        private const string EmailColumnName = "Email";
        private const string EmailConfirmedColumnName = "EmailConfirmed";
        private const string PasswordHashColumnName = "PasswordHash";
        private const string SecurityStampColumnName = "SecurityStamp";
        private const string ConcurrencyStampColumnName = "ConcurrencyStamp";
        private const string PhoneNumberColumnName = "PhoneNumber";
        private const string PhoneNumberConfirmedColumnName = "PhoneNumberConfirmed";
        private const string TwoFactorEnabledColumnName = "TwoFactorEnabled";
        private const string LockoutEndColumnName = "LockoutEnd";
        private const string LockoutEnabledColumnName = "LockoutEnabled";
        private const string AccessFailedCountColumnName = "AccessFailedCount";
        private const string UserTypeIdColumnName = "UserTypeId";
        private const string UserTypeNameColumnName = "UserTypeName";
        protected const string IdentificationNumberColumnName = "IdentificationNumber";
        protected const string UserTypeCodeColumnName = "UserTypeCode";


        //private const string SearchTextParameterName = "PSearchText";
        private const string ApplicationUserRoleIdParameterName = "PUserRoleId";
        private const string ApplicationRoleIdParameterName = "PRoleId";
        private const string ApplicationUserTypeIdParameterName = "PUserTypeId";
        private const string ApplicationRoleNameParameterName = "PRoleName";
        private const string ApplicationUserIdParameterName = "PUserId";
        private const string ApplicationUserEmailParameterName = "PUserEmail";
        private const string ApplicationUserNameParameterName = "PUserName";
        private const string FirstNameParameterName = "PFirstName";
        private const string LastNameParameterName = "PLastName";
        private const string EmailParameterName = "PEmail";
        private const string IdentificationNumberParameterName = "PIdentificationNumber";
        private const string UserEmailParameterName = "PUserEmail";
        private const string EmailConfirmedParameterName = "PEmailConfirmed";
        private const string PasswordHashParameterName = "PPasswordHash";
        private const string SecurityStampParameterName = "PSecurityStamp";
        private const string ConcurrencyStampParameterName = "PConcurrencyStamp";
        private const string PhoneNumberParameterName = "PPhoneNumber";
        private const string PhoneNumberConfirmedParameterName = "PPhoneNumberConfirmed";
        private const string TwoFactorEnabledParameterName = "PTwoFactorEnabled";
        private const string LockoutEndParameterName = "PLockoutEnd";
        private const string LockoutEnabledParameterName = "PLockoutEnabled";
        private const string AccessFailedCountParameterName = "PAccessFailedCount";
        private const string Address1ParameterName = "PAddress1";
        private const string Address2ParameterName = "PAddress2";
        private const string CityIdParameterName = "PCityId";
        private const string StateIdParameterName = "PStateId";

        #endregion

        #region Interface IApplicationUserfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            var applicationUserIdParamter = base.GetParameterOut(UserInfrastructure.ApplicationUserIdParameterName, SqlDbType.Int, applicationUser.UserId);
            var parameters = new List<DbParameter>
            {
                applicationUserIdParamter,
                base.GetParameter(UserInfrastructure.ApplicationUserNameParameterName, applicationUser.UserName),
                base.GetParameter(UserInfrastructure.FirstNameParameterName, applicationUser.FirstName),
                base.GetParameter(UserInfrastructure.LastNameParameterName, applicationUser.LastName),
                base.GetParameter(UserInfrastructure.EmailParameterName, applicationUser.Email),
                base.GetParameter(UserInfrastructure.IdentificationNumberParameterName, applicationUser.IdentificationNumber),
                base.GetParameter(UserInfrastructure.Address1ParameterName, applicationUser.Address1),
                base.GetParameter(UserInfrastructure.Address2ParameterName, applicationUser.Address2),
                base.GetParameter(UserInfrastructure.CityIdParameterName, applicationUser.CityId),
                base.GetParameter(UserInfrastructure.StateIdParameterName, applicationUser.StateId),
                base.GetParameter(UserInfrastructure.EmailConfirmedParameterName, applicationUser.EmailConfirmed),
                base.GetParameter(UserInfrastructure.PasswordHashParameterName, applicationUser.PasswordHash),
                base.GetParameter(UserInfrastructure.SecurityStampParameterName, applicationUser.SecurityStamp),
                base.GetParameter(UserInfrastructure.ConcurrencyStampParameterName, applicationUser.ConcurrencyStamp),
                base.GetParameter(UserInfrastructure.PhoneNumberParameterName, applicationUser.PhoneNumber),
                base.GetParameter(UserInfrastructure.PhoneNumberConfirmedParameterName, applicationUser.PhoneNumberConfirmed),
                base.GetParameter(UserInfrastructure.TwoFactorEnabledParameterName, applicationUser.TwoFactorEnabled),
                base.GetParameter(UserInfrastructure.LockoutEnabledParameterName, applicationUser.LockoutEnabled),
                base.GetParameter(UserInfrastructure.AccessFailedCountParameterName, applicationUser.AccessFailedCount),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, applicationUser.Active),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.CreatedById)
            };

            await base.ExecuteNonQuery(parameters, UserInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            applicationUser.UserId = Convert.ToInt32(applicationUserIdParamter.Value);


            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    await connection.OpenAsync(cancellationToken);
            //    await connection.ExecuteAsync($"DELETE FROM [ApplicationUser] WHERE [Id] = @{nameof(ApplicationUser.Id)}", user);
            //}

            return IdentityResult.Success;
        }
        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser applicationUserItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, userId),
                //base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        applicationUserItem = new ApplicationUser
                        {

                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserName = dataReader.GetStringValue(UserInfrastructure.ApplicationUserNameColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            PhoneNumber = dataReader.GetStringValue(UserInfrastructure.PhoneNumberColumnName),
                            Active = dataReader.GetBooleanValue(UserInfrastructure.ActiveColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName)
                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return applicationUserItem;
        }
        public async Task<ApplicationUser> GetByUserIdAsync(ApplicationUser user)
        {

            ApplicationUser applicationUserItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, user.UserId),
                //base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetUserByIdStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        applicationUserItem = new ApplicationUser
                        {
                            Roles = new List<ApplicationRole>(),
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            IdentificationNumber = dataReader.GetStringValue(UserInfrastructure.IdentificationNumberColumnName),
                            UserTypeId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.UserTypeIdColumnName),
                            UserTypeName = dataReader.GetStringValue(UserInfrastructure.UserTypeNameColumnName),
                            PhoneNumber = dataReader.GetStringValue(UserInfrastructure.PhoneNumberColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                        };
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            var roles = new ApplicationRole();
                            roles.RoleId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationRoleIdColumnName);
                            roles.RoleName = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameColumnName);
                            roles.RoleNameCode = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleCodeColumnName);

                            roles.RoleNameTranslation = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameTranslationColumnName);

                            applicationUserItem?.Roles.Add(roles);
                        }
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationUserItem;
        }

        public async Task<ApplicationUser> GetUserWithRolesByEmail(ApplicationUser user)
        {
            ApplicationUser applicationUserItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserEmailParameterName, user.Email),
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetUserWithRolesByEmailStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        applicationUserItem = new ApplicationUser
                        {
                            Roles = new List<ApplicationRole>(),
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            UserTypeCode = dataReader.GetStringValue(UserInfrastructure.UserTypeCodeColumnName),
                            UserTypeName = dataReader.GetStringValue(UserInfrastructure.UserTypeNameColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                        };
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            var roles = new ApplicationRole();
                            roles.RoleId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationRoleIdColumnName);
                            roles.RoleName = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameColumnName);
                            roles.RoleNameCode = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleCodeColumnName);
                            roles.RoleNameTranslation = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameTranslationColumnName);
                            applicationUserItem?.Roles.Add(roles);
                        }
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationUserItem;
        }

        public async Task<List<ApplicationUser>> GetUsersByRole(ApplicationUser user)
        {
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            ApplicationUser applicationUser = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationRoleIdParameterName, user.RoleId),
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.ApplicationUserGetByRoleStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            applicationUser = new ApplicationUser
                            {
                                UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                                FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                                LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                                Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                                Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName),
                                IdentificationNumber = dataReader.GetStringValue(UserInfrastructure.IdentificationNumberColumnName)
                            };

                            applicationUsers.Add(applicationUser);
                        };

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }
                    }
                   
                }
            }

            return applicationUsers;
        }


        public async Task<AllResponse<ApplicationUser>> GetAll(AllRequest<ApplicationUser> make)
        {
            var result = new AllResponse<ApplicationUser>
            {
                Data = new List<ApplicationUser>(),
                Offset = make.Offset,
                PageSize = make.PageSize,
                SortColumn = make.SortColumn,
                SortAscending = make.SortAscending
            };
            ApplicationUser currentApplicationUser = null;
            UserRoles roleItems;
            var totalRecordParamter = base.GetParameterOut(BaseSQLInfrastructure.TotalRecordParameterName, SqlDbType.Int, result.TotalRecord);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(BaseSQLInfrastructure.OffsetParameterName, make.Offset),
                base.GetParameter(BaseSQLInfrastructure.PageSizeParameterName, make.PageSize),
                base.GetParameter(BaseSQLInfrastructure.SortColumnParameterName, make.SortColumn),
                base.GetParameter(BaseSQLInfrastructure.SortAscendingParameterName, make.SortAscending),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, make.Data.CreatedById),
                base.GetParameter(UserInfrastructure.ApplicationUserTypeIdParameterName, make.Data.UserTypeId),
                base.GetParameter(UserInfrastructure.ActiveParameterName, make.Data.ActiveColumn),
                base.GetParameter(UserInfrastructure.ApplicationRoleIdParameterName, make.Data.RoleId),
                base.GetParameter(UserInfrastructure.SearchTextParameterName, make.Data.SearchText)



            };
            //TODO: Add other parameters.

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var userItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            UserTypeId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.UserTypeIdColumnName),
                            UserTypeName = dataReader.GetStringValue(UserInfrastructure.UserTypeNameColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName),
                            ApplicationUserRoles = new List<UserRoles>()
                        };
                        //TODO: Add other Columns.

                        result.Data.Add(userItem);
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            roleItems = new UserRoles();
                            roleItems.RoleId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationRoleIdColumnName);
                            roleItems.UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName);
                            roleItems.RoleName = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameColumnName);
                            roleItems.RoleNameTranslation = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameTranslationColumnName);

                            if (currentApplicationUser == null || currentApplicationUser.UserId != roleItems.UserId)
                            {

                                currentApplicationUser = result.Data.FirstOrDefault(c => c.UserId == roleItems.UserId);

                            }

                            currentApplicationUser?.ApplicationUserRoles.Add(roleItems);
                        }
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                    result.TotalRecord = Convert.ToInt32(totalRecordParamter.Value);
                }
            }

            return result;
        }
        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser modelItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserNameParameterName, normalizedUserName),
                //base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, model.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetbyUserNameStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserName = dataReader.GetStringValue(UserInfrastructure.ApplicationUserNameColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            PhoneNumber = dataReader.GetStringValue(UserInfrastructure.PhoneNumberColumnName),
                            Active = dataReader.GetBooleanValue(UserInfrastructure.ActiveColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName),
                            PasswordHash = dataReader.GetStringValue(UserInfrastructure.PasswordHashColumnName)
                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return modelItem;
        }
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }
        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }
        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, applicationUser.UserId),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, applicationUser.Active),

                base.GetParameter(UserInfrastructure.ApplicationUserNameParameterName, applicationUser.UserName),
                base.GetParameter(UserInfrastructure.FirstNameParameterName, applicationUser.FirstName),
                base.GetParameter(UserInfrastructure.LastNameParameterName, applicationUser.LastName),
                base.GetParameter(UserInfrastructure.EmailParameterName, applicationUser.Email),
                base.GetParameter(UserInfrastructure.EmailConfirmedParameterName, applicationUser.EmailConfirmed),
                base.GetParameter(UserInfrastructure.IdentificationNumberParameterName, applicationUser.IdentificationNumber),
                base.GetParameter(UserInfrastructure.SecurityStampParameterName, applicationUser.SecurityStamp),
                base.GetParameter(UserInfrastructure.ConcurrencyStampParameterName, applicationUser.ConcurrencyStamp),
                base.GetParameter(UserInfrastructure.PhoneNumberParameterName, applicationUser.PhoneNumber),
                base.GetParameter(UserInfrastructure.PhoneNumberConfirmedParameterName, applicationUser.PhoneNumberConfirmed),
                base.GetParameter(UserInfrastructure.TwoFactorEnabledParameterName, applicationUser.TwoFactorEnabled),
                base.GetParameter(UserInfrastructure.LockoutEndParameterName, applicationUser.LockoutEnd),
                base.GetParameter(UserInfrastructure.LockoutEnabledParameterName, applicationUser.LockoutEnabled),
                base.GetParameter(UserInfrastructure.AccessFailedCountParameterName, applicationUser.AccessFailedCount),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.CreatedById)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, UserInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            // return returnValue > 0;

            return returnValue > 0 ? IdentityResult.Success : IdentityResult.Failed();
        }
        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }
        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }
        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }
        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }
        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser modelItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.UserEmailParameterName, normalizedEmail),
                //base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, model.CreatedById)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetbyEmailStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserName = dataReader.GetStringValue(UserInfrastructure.ApplicationUserNameColumnName),
                            DealershipName = dataReader.GetStringValue(UserInfrastructure.DealershipNameColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            PhoneNumber = dataReader.GetStringValue(UserInfrastructure.PhoneNumberColumnName),
                            Active = dataReader.GetBooleanValue(UserInfrastructure.ActiveColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return modelItem;
        }

        public async Task<ApplicationUser> GetUserTypeByEmailAsync(string normalizedEmail)
        {
            ApplicationUser modelItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.UserEmailParameterName, normalizedEmail)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.UserTypeGetbyEmailStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserTypeCode = dataReader.GetStringValue(UserInfrastructure.UserTypeCodeColumnName),
                            UserTypeName = dataReader.GetStringValue(UserInfrastructure.UserTypeNameColumnName)
                            
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return modelItem;
        }
        public async Task<ApplicationUser> GetDealerbyEmail(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser modelItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.UserEmailParameterName, normalizedEmail)                
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetDealerbyEmailStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserName = dataReader.GetStringValue(UserInfrastructure.ApplicationUserNameColumnName),
                            DealershipName = dataReader.GetStringValue(UserInfrastructure.DealershipNameColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            PhoneNumber = dataReader.GetStringValue(UserInfrastructure.PhoneNumberColumnName),
                            Active = dataReader.GetBooleanValue(UserInfrastructure.ActiveColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return modelItem;
        }

        public async Task<ApplicationUser> GetInspectorbyEmail(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ApplicationUser modelItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.UserEmailParameterName, normalizedEmail)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetInspectorbyEmailStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserName = dataReader.GetStringValue(UserInfrastructure.ApplicationUserNameColumnName),
                            DealershipName = dataReader.GetStringValue(UserInfrastructure.DealershipNameColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName),
                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            PhoneNumber = dataReader.GetStringValue(UserInfrastructure.PhoneNumberColumnName),
                            Active = dataReader.GetBooleanValue(UserInfrastructure.ActiveColumnName),
                            CreatedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.CreatedByIdColumnName),
                            CreatedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.CreatedDateColumnName),
                            ModifiedById = dataReader.GetUnsignedIntegerValue(BaseSQLInfrastructure.ModifiedByIdColumnName),
                            ModifiedDate = dataReader.GetDateTimeValueNullable(BaseSQLInfrastructure.ModifiedDateColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return modelItem;
        }
        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }
        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
            return Task.FromResult(0);
        }
        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }
        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }
        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }
        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }
        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, user.UserId),

               base.GetParameter(UserInfrastructure.PasswordHashParameterName, passwordHash)

            };


            var returnValue = base.ExecuteReader(parameters, UserInfrastructure.SetPasswordStoredProcedureName, CommandType.StoredProcedure);



            return Task.FromResult(0);
        }
        public async Task<List<ApplicationUser>> Search(string searchText)
        {
            var models = new List<ApplicationUser>();

            var parameters = new List<DbParameter>
            {

                base.GetParameter(UserInfrastructure.SearchTextParameterName, searchText)

            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.ApplicationUserSearchStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        var modelItem = new ApplicationUser
                        {

                            FirstName = dataReader.GetStringValue(UserInfrastructure.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(UserInfrastructure.LastNameColumnName),
                            //Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName)
                        };
                        models.Add(modelItem);

                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }
            return models;
        }
        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ApplicationUser appuser = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName , user.UserId)

            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetPasswordStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        appuser = new ApplicationUser
                        {
                            PasswordHash = dataReader.GetStringValue(UserInfrastructure.PasswordHashColumnName)

                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }



            return appuser.PasswordHash;
        }
        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
        public async Task<int> GetRoleId(string role)
        {
            ApplicationRole modelItem = new ApplicationRole();
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationRoleNameParameterName, role),

            };
            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetRoleIdStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        modelItem.RoleId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationRoleIdColumnName);
                        modelItem.RoleName = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameColumnName);
                        modelItem.RoleNameTranslation = dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameTranslationColumnName);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }
            return modelItem.RoleId;
        }
        public async Task AddToRoleAsync(ApplicationUser applicationUser, string role, CancellationToken ct)
        {
            var roleId = await GetRoleId(role);
            var applicationUserRoleIdParamter = base.GetParameterOut(UserInfrastructure.ApplicationUserRoleIdParameterName, SqlDbType.Int);
            var parameters = new List<DbParameter>
            {
                applicationUserRoleIdParamter,
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, applicationUser.UserId),
                base.GetParameter(UserInfrastructure.ApplicationRoleIdParameterName, roleId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.CreatedById)
            };

            await base.ExecuteNonQuery(parameters, UserInfrastructure.AddUserRoleStoredProcedureName, CommandType.StoredProcedure);

            var userRoleId = Convert.ToInt32(applicationUserRoleIdParamter.Value);


        }
        public async Task<int> InsertRole(ApplicationUser applicationUser, string role, CancellationToken ct)
        {
            var roleId = await GetRoleId(role);
            var applicationUserRoleIdParamter = base.GetParameterOut(UserInfrastructure.ApplicationUserRoleIdParameterName, SqlDbType.Int);
            var parameters = new List<DbParameter>
            {
                applicationUserRoleIdParamter,
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, applicationUser.UserId),
                base.GetParameter(UserInfrastructure.ApplicationRoleIdParameterName, roleId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, applicationUser.CreatedById)
            };

            await base.ExecuteNonQuery(parameters, UserInfrastructure.AddUserRoleStoredProcedureName, CommandType.StoredProcedure);

            var userRoleId = Convert.ToInt32(applicationUserRoleIdParamter.Value);
            return userRoleId;


        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var UserRoles = new List<string>();

            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, user.UserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.ApplicationUserRoleGetByUserStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {

                        UserRoles.Add(dataReader.GetStringValue(UserInfrastructure.ApplicationRoleNameColumnName));
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return UserRoles;
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var totalRecordParamter = base.GetParameterOut(BaseSQLInfrastructure.TotalRecordParameterName, SqlDbType.Int);
            var parameters = new List<DbParameter>
            {
                totalRecordParamter,
                base.GetParameter(UserInfrastructure.ApplicationRoleNameParameterName, roleName),
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, user.UserId),

            };

            await base.ExecuteNonQuery(parameters, UserInfrastructure.UserRoleCheckStoredProcedureName, CommandType.StoredProcedure);

            var matchingRoles = (int)totalRecordParamter.Value;

            return matchingRoles > 0;
        }
        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    await connection.OpenAsync(cancellationToken);
            //    var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
            //    if (!roleId.HasValue)
            //        await connection.ExecuteAsync($"DELETE FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
            //}
        }
        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var queryResults = await connection.QueryAsync<ApplicationUser>("SELECT u.* FROM [ApplicationUser] u " +
            //        "INNER JOIN [ApplicationUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [ApplicationRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
            //        new { normalizedName = roleName.ToUpper() });

            //    return queryResults.ToList();
            //}

            List<ApplicationUser> user = new List<ApplicationUser>();
            return user;
        }
        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.FromResult(0);
        }
        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }
        public void Dispose()
        {
            // Nothing to dispose.
        }

        public async Task<bool> RemoveExistingRolesForUser(ApplicationUser user, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var parameters = new List<DbParameter>
            {
                base.GetParameter(UserInfrastructure.ApplicationUserIdParameterName, user.UserId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, user.CreatedById)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, UserInfrastructure.RemoveExistingRolesForUserStoredProcedureName, CommandType.StoredProcedure);


            return true;
        }

        public async Task<List<ApplicationUser>> GetAdmins()
        {

            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            ApplicationUser applicationUserItem = null;
            List<DbParameter> parameters = null;

            using (var dataReader = await base.ExecuteReader(parameters, UserInfrastructure.GetAdminsStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        applicationUserItem = new ApplicationUser
                        {
                            UserId = dataReader.GetUnsignedIntegerValue(UserInfrastructure.ApplicationUserIdColumnName),
                            UserName = dataReader.GetStringValue(UserInfrastructure.ApplicationUserNameColumnName),
                            Email = dataReader.GetStringValue(UserInfrastructure.EmailColumnName)
                        };

                        applicationUsers.Add(applicationUserItem);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return applicationUsers;
        }

        #endregion
    }
}
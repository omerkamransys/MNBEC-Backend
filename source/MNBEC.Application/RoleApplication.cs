using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Cache;
using MNBEC.CacheInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// RoleApplication inherits from BaseApplication and implements IRoleApplication. It provides the implementation for Role related operations.
    /// </summary>
    public class RoleApplication : BaseApplication, IRoleApplication
    {
        private readonly IClaimApplication claimApplication;
        //private readonly IRoleClaimCache roleClaimCache;
        #region Constructor
        /// <summary>
        /// RoleApplication initailizes object instance.
        /// </summary>
        /// <param name="roleInfrastructure"></param>
        /// <param name="roleCache"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public RoleApplication(IClaimApplication claimApplication, IRoleInfrastructure roleInfrastructure, IConfiguration configuration, ILogger<RoleApplication> logger) : base(configuration, logger)//, RoleManager<ApplicationRole> roleManager)
        {
            this.claimApplication = claimApplication;         
            this.RoleInfrastructure = roleInfrastructure;
           // this.RoleCache = roleCache;
            //this.RoleManager = roleManager;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// ApplicationRoleInfrastructur holds the Infrastructure object.
        /// </summary>
        public IRoleInfrastructure RoleInfrastructure { get; }

        //public IRoleCache RoleCache { get; }

        //private RoleManager<ApplicationRole> RoleManager { get; }
        #endregion

        //#region Indentity  Implementation
        //public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken ct)
        //{
        //    return await RoleManager.CreateAsync(role);
        //}

        //public async Task<ApplicationRole> GetRoleById(ApplicationRole role)
        //{
        //    return await _roleManager.FindByIdAsync(role.RoleId.ToString());
        //}

        //public async Task<IdentityResult> Update(ApplicationRole role, CancellationToken ct)
        //{
        //    return await RoleManager.UpdateAsync(role);
        //}
        //#endregion

        #region Interface IApplicationRoleInfrastructur Implementation
        /// <summary>
        /// Add calls ApplicationRoleInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<int> Add(ApplicationRole applicationRole)
        {
            return await this.RoleInfrastructure.Add(applicationRole);
        }

        /// <summary>
        /// Activate calls ApplicationRoleInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<bool> Activate(ApplicationRole applicationRole)
        {
            return await this.RoleInfrastructure.Activate(applicationRole);
        }

        /// <summary>
        /// Get calls ApplicationRoleInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<ApplicationRole> Get(ApplicationRole applicationRole)
        {
            return await this.RoleInfrastructure.Get(applicationRole);
        }

        /// <summary>
        /// GetAll calls ApplicationRoleInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<AllResponse<ApplicationRole>> GetAll(AllRequest<ApplicationRole> applicationRole)
        {
            return await this.RoleInfrastructure.GetAll(applicationRole);
        }

        /// <summary>
        /// GetList calls ApplicationRoleInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<List<ApplicationRole>> GetList(ApplicationRole applicationRole)
        {
            return await this.RoleInfrastructure.GetList(applicationRole);
        }

        /// <summary>
        /// Update calls ApplicationRoleInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="applicationRole"></param>
        /// <returns></returns>
        public async Task<bool> Update(ApplicationRole applicationRole)
        {
            var isUpdated = await this.RoleInfrastructure.Update(applicationRole);

            if (isUpdated)
            {
                #region populating Dictionary
                var roles = await claimApplication.GetAllClaimsWithRole();

                if (roles.Count > 0)
                {
                    Dictionary<string, HashSet<string>> roleClaimsDictionary = new Dictionary<string, HashSet<string>>();

                    foreach (var role in roles)
                    {
                        foreach (var claim in role.ApplicationClaims)
                        {
                            if (roleClaimsDictionary.ContainsKey(role.RoleNameCode))
                            {
                                roleClaimsDictionary[role.RoleNameCode].Add(claim.ClaimCode);
                            }
                            else
                            {
                                var hashSet = new HashSet<string>();
                                hashSet.Add(claim.ClaimCode);
                                roleClaimsDictionary.Add(role.RoleNameCode, hashSet);
                            }
                        }
                    }

                   // await roleClaimCache.SetBulk("RoleClaims", roleClaimsDictionary);
                }
                else {
                    throw new Exception("Failed in Role Application.Update - no roles with claims found - Need to set it for Redis Cache so users may get the correct priveliges ");
                }
                #endregion
            }

            return isUpdated;
        }
        #endregion
    }
}

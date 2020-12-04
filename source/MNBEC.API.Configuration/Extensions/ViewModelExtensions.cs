using System.Collections.Generic;
using MNBEC.API.Core.Extensions;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.ViewModel.Common;
using MNBEC.ViewModel.Configuration;

namespace MNBEC.API.Vehicle.Extensions
{
    /// <summary>
    /// ViewModelExtensions class provides implementation for extension methods.
    /// </summary>
    public static class ViewModelExtensions
    {
        

        #region Channel
        public static Channel Convert(this ChannelRequestVM vm)
        {
            var model = new Channel
            {
                ChannelId = vm.ChannelId,
                ClientId = vm.ClientId,
                ChannelName = vm.ChannelName,
                CreatedById = vm.CurrentUserId,
                Active = vm.Active
            };

            return model;
        }


        /// <summary>
        /// Convert converts object of Channel type to RequestChannelVM type.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ChannelResponseVM Convert(this Channel model, bool useLanguage)
        {
            ChannelResponseVM vm = null;

            if (model != null)
            {
                vm = new ChannelResponseVM
                {
                    ChannelId = model.ChannelId,
                    ChannelName = model.ChannelName,

                    Description = model.Description,
                    CreatedById = model.CreatedById,
                    CreatedByName = model.CreatedByName,
                    CreatedDate = model.CreatedDate,
                    ModifiedById = model.ModifiedById,
                    ModifiedByName = model.ModifiedByName,
                    Active = model.Active,
                    ModifiedDate = model.ModifiedDate
                };
            }

            return vm;
        }

        /// <summary>
        /// ConvertAdd converts object of ChannelUpdateRequestVM type to Channel type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static Channel ConvertActivate(this ChannelActivateRequestVM vm)
        {
            var model = new Channel
            {
                ChannelId = vm.ChannelId,
                CreatedById = vm.CurrentUserId,
                Active = vm.Active
            };

            return model;
        }

        /// <summary>
        /// ConvertAdd converts object of ChannelUpdateRequestVM type to Channel type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static Channel ConvertAdd(this ChannelAddRequestVM vm)
        {
            var model = new Channel
            {
                ChannelId = vm.ChannelId,
                ChannelName = vm.ChannelName,
                ClientId = vm.ClientId,
                Description = vm.Description ?? "",
                CreatedById = vm.CurrentUserId
            };

            return model;
        }

        /// <summary>
        /// ConvertUpdate converts object of ChannelUpdateRequestVM type to Channel type.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static Channel ConvertUpdate(this ChannelUpdateRequestVM vm)
        {
            var model = new Channel
            {
                ChannelId = vm.ChannelId,
                ChannelName = vm.ChannelName,
                Description = vm.Description ?? "",
                Active = vm.Active,
                CreatedById = vm.CurrentUserId
            };

            return model;
        }

        /// <summary>
        /// Convert converts object of Channel type to RequestChannelVM type.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<ChannelListResponseVM> ConvertList(this List<Channel> models, bool useLanguage)
        {
            List<ChannelListResponseVM> vms = new List<ChannelListResponseVM>();

            foreach (var model in models)
            {
                vms.Add(
                    new ChannelListResponseVM
                    {
                        ChannelId = model.ChannelId,
                        ClientId = model.ClientId,
                        ChannelName = model.ChannelName,
                        Description = model.Description ?? "",
                        Active = model.Active
                    });
            }

            return vms;
        }



        /// <summary>
        /// Convert converts object of RequestChannelAllVM type to AllRequest Channel.
        /// </summary>
        /// <param name="allRequestVm"></param>
        /// <returns></returns>
        public static AllRequest<Channel> ConvertAll(this ChannelAllRequestVM allRequestVm)
        {
            var allRequest = allRequestVm.Convert<Channel>();

            allRequest.Data.ChannelId = allRequestVm.ChannelId;
            allRequest.Data.ClientId = allRequestVm.ClientId;
            allRequest.Data.ChannelName = allRequestVm.ChannelName;
            allRequest.Data.Description = allRequestVm.Description;


            return allRequest;
        }

        /// <summary>
        /// Convert converts object of AllResponse Channel type to AllResponseVM ResponseChannelAllVM.
        /// </summary>
        /// <param name="allResponse"></param>
        /// <returns></returns>
        public static AllResponseVM<ChannelAllResponseVM> ConvertAll(this AllResponse<Channel> allResponse, bool useLanguage)
        {
            var allResponseVm = allResponse.Convert<Channel, ChannelAllResponseVM>();

            allResponseVm.Data = new List<ChannelAllResponseVM>();

            foreach (var dataItem in allResponse.Data)
            {
                allResponseVm.Data.Add(
                        new ChannelAllResponseVM
                        {
                            ChannelId = dataItem.ChannelId,
                            ClientId = dataItem.ClientId,
                            ChannelName = dataItem.ChannelName,
                            Description = dataItem.Description ?? ""

                        }
                    );
            }

            return allResponseVm;
        }


        #endregion

    }

}
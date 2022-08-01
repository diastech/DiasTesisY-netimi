using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasShared.Errors;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.Shared.Error;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class AttachmentWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.IAttachmentWrapperBusinessRules, IBaseAttachmentWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomAttachmentProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public AttachmentWrapperBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>())
        {
        }
        private AttachmentWrapperBusinessRules(DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules )
        {
            _attachmentBusinessRules = attachmentBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomAttachmentDto>>> GetTicketAttachmentsByTicketId(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomAttachmentDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            Tuple<Error, List<DevelopmentDto.AttachmentDto>> resultGetAttachmentList = await _attachmentBusinessRules.GetAttachmentsByTicketId(Id);

                            try
                            {
                                if ((resultGetAttachmentList.Item1.BusinessOperationSucceed == true) && (resultGetAttachmentList.Item2 != null))
                                {
                                    List<CustomDto.CustomAttachmentDto> returnDtoList = new List<CustomDto.CustomAttachmentDto>();

                                    returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                            Map<List<DevelopmentDto.AttachmentDto>, List<CustomDto.CustomAttachmentDto>>(resultGetAttachmentList.Item2);

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomAttachmentDto>>(resultGetAttachmentList.Item1, returnDtoList);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomAttachmentDto>>(resultGetAttachmentList.Item1, null);
                                }
                            }
                            catch (Exception e)
                            {

                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomAttachmentDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}

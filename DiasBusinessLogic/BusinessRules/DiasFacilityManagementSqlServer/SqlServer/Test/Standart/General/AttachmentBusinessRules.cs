using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentAttachmentInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentAttachmentProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class AttachmentBusinessRules : BusinessRuleAbstract, DevelopmentAttachmentInterface.IAttachmentBusinessRules, IBaseAttachmentBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentAttachmentProfile.AttachmentProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public AttachmentBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private AttachmentBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<ErrorCodes, AttachmentDto>> AddAsync(AttachmentDto attachmentDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.Attachment sonucEntity;
                                AttachmentDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                Attachment convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<AttachmentDto, Attachment>(attachmentDto);
                                if(convertedEntity.TicketId == 0)
                                {
                                    convertedEntity.TicketId = 20;
                                }
                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Attachment, AttachmentDto>(convertedEntity);


                                return new(ErrorCodes.None, addedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception e)
                            {
                                return new(ErrorCodes.UnknownErrorOnGettingEntityFromUserTable, null);
                            }
                        }

                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async Task<Tuple<ErrorCodes, AttachmentDto>> UpdateAsync(AttachmentDto attachmentDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.Attachment sonucEntity;
                                AttachmentDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                Attachment convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<AttachmentDto, Attachment>(attachmentDto);
                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Attachment, AttachmentDto>(convertedEntity);


                                return new(ErrorCodes.None, addedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new(ErrorCodes.UnknownErrorOnGettingEntityFromUserTable, null);
                            }
                        }

                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async  Task<Tuple<ErrorCodes, AttachmentDto>> DeleteAsync(AttachmentDto attachmentDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.Attachment sonucEntity;
                                AttachmentDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                Attachment convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<AttachmentDto, Attachment>(attachmentDto);

                                convertedEntity.IsActive = false;

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Attachment, AttachmentDto>(convertedEntity);


                                return new(ErrorCodes.None, addedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new(ErrorCodes.UnknownErrorOnGettingEntityFromUserTable, null);
                            }
                        }

                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async Task<Tuple<ErrorCodes, IEnumerable<AttachmentDto>>> GetAllTicketAttachmentsByTicketIdAsync(int basicTicketId)
        {
            throw new NotImplementedException();
        }
    }
}

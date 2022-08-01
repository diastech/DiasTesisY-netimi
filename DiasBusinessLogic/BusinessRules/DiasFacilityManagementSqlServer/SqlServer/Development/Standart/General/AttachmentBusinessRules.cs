using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentAttachmentInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentAttachmentProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class AttachmentBusinessRules : BusinessRuleAbstract, DevelopmentAttachmentInterface.IAttachmentBusinessRules, IBaseAttachmentBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentAttachmentProfile.AttachmentProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public AttachmentBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private AttachmentBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, AttachmentDto>> AddAsync(AttachmentDto attachmentDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {                
                return new Tuple<Error, AttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
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

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Attachment, AttachmentDto>(convertedEntity);

                                return new Tuple<Error, AttachmentDto>(Errors.General.SuccessInsert("Attachment"), addedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.ErrorInsert("Attachment"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, AttachmentDto>> UpdateAsync(AttachmentDto attachmentDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, AttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
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
                                return new Tuple<Error, AttachmentDto>(Errors.General.SuccessUpdate("Attachment"), addedDto);                                
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.ErrorInsert("Attachment"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async  Task<Tuple<Error, AttachmentDto>> DeleteAsync(AttachmentDto attachmentDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, AttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
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
                                AttachmentDto deletedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                Attachment convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<AttachmentDto, Attachment>(attachmentDto);

                                convertedEntity.IsActive = false;

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));

                                deletedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Attachment, AttachmentDto>(convertedEntity);


                                return new Tuple<Error, AttachmentDto>(Errors.General.SuccessDelete("Attachment"), deletedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.ErrorInsert("Attachment"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<AttachmentDto>>> GetAttachmentsByTicketId(int ticketId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.AttachmentDto> returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.Attachment> sonucEntity;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {

                                    sonucEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Attachments.AsQueryable().Where(x => x.TicketId == ticketId && x.IsActive == true && x.IsDeleted == false).Include(x=>x.AddedByUser)
                                    .AsNoTracking<DevelopmentModel.Attachment>().ToList<DevelopmentModel.Attachment>());
                                }

                                List<DevelopmentDto.AttachmentDto> convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.Attachment>, List<DevelopmentDto.AttachmentDto>>(sonucEntity);
                                returnDto = convertedDto;

                                return new(Errors.General.SuccessGetById("Attachment"), returnDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorGetById("Attachment"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
               
        public async Task<Tuple<Error,int>> GetAttachmentsCountByTicketId(int ticketId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), -1);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                int sonuc;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {

                                    sonuc = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Attachments.AsQueryable().Where(x => x.TicketId == ticketId && x.IsActive == true && x.IsDeleted == false)
                                    .AsNoTracking<DevelopmentModel.Attachment>().ToList<DevelopmentModel.Attachment>().Count);
                                }

                                return new(Errors.General.SuccessGetById("Attachment"), sonuc);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), -1);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), -1);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorGetById("Attachment"), -1);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), -1);
                        }
                }
            }
        }
    }
}

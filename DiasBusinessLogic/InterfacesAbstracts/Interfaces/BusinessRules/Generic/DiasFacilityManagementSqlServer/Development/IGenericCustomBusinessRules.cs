using AutoMapper;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasBusinessLogic.Shared.Enums.EntityDtoMapping;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development
{
    public interface IGenericCustomBusinessRules<TDto, TAutomapperProfile> where TDto : IBaseDevelopmentCustomDto, new()
                                                                            where TAutomapperProfile : Profile, new()
    {
        #region Custom Crud 

        public Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAll();

        public Task<Tuple<ErrorCodes, TDto>> GetByIdFromInt(int id);

        public Task<Tuple<ErrorCodes, TDto>> GetByIdFromLong(long id);

        public Task<Tuple<ErrorCodes, TDto>> Insert(TDto insertedDto);

        public Task<Tuple<ErrorCodes, TDto>> Update(TDto updatedDto);

        public Task<Tuple<ErrorCodes, IEnumerable<TDto>>> DeleteFromInt(int id);

        public Task<Tuple<ErrorCodes, TDto>> DeleteFromLong(long id);


        #endregion Custom Crud

    }
}

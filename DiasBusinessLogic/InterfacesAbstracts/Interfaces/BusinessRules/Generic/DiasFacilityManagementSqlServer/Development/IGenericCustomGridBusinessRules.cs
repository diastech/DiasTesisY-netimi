using AutoMapper;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development
{
    public interface IGenericCustomGridBusinessRules<TDto, TAutomapperProfile>  where TDto : IBaseDevelopmentCustomDto, new()
                                                                                  where TAutomapperProfile : Profile, new()
    {
        public Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAll();

        public Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAllFiltered();

        public Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAllWithPaging();

        public Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAllWithPagingFiltered();

        public Task<Tuple<ErrorCodes, TDto>> Insert(TDto insertedDto);

        Task<Tuple<ErrorCodes, TDto>> Update(TDto updatedDto);

        Task<Tuple<ErrorCodes, TDto>> DeleteFromInt(int id);

        Task<Tuple<ErrorCodes, TDto>> DeleteFromLong(long id);


    }
}

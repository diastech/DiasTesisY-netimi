using AutoMapper;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development
{
    public interface IGenericStandartBusinessRules<TDto, TAutomapperProfile> where TDto : BaseDevelopmentStandartDto, new()
                                                                                where TAutomapperProfile : Profile, new()
    {
        #region Standart Crud 

        Task<Tuple<ErrorCodes, IEnumerable<TDto>>> GetAll();

        Task<Tuple<ErrorCodes, TDto>> GetByIdFromInt(int id);

        Task<Tuple<ErrorCodes, TDto>> GetByIdFromLong(long id);

        Task<Tuple<ErrorCodes, TDto>> Insert(TDto insertedDto, List<string> uniqueColumns = null, List<object> uniqueValues = null);

        Task<Tuple<ErrorCodes, TDto>> Update(TDto updatedDto, List<string> uniqueColumns, List<object> uniqueValues, bool isBaseDtoUpdated = true);

        Task<Tuple<ErrorCodes, TDto>> DeleteFromInt(int id);

        Task<Tuple<ErrorCodes, TDto>> DeleteFromLong(long id);


        Task<Tuple<Error, IEnumerable<TDto>>> GetAllV2();

        Task<Tuple<Error, TDto>> GetByIdFromIntV2(int id);

        Task<Tuple<Error, TDto>> GetByIdFromLongV2(long id);

        Task<Tuple<Error, TDto>> InsertV2(TDto insertedDto, List<string> uniqueColumns = null, List<object> uniqueValues = null, bool checkIsDeleted = false);

        Task<Tuple<Error, TDto>> UpdateV2(TDto updatedDto, List<string> uniqueColumns, List<object> uniqueValues, bool isBaseDtoUpdated = true);

        Task<Tuple<Error, TDto>> DeleteFromIntV2(int id);

        Task<Tuple<Error, TDto>> DeleteFromLongV2(long id);

        #endregion Standart Crud
    }
}

using AutoMapper;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Standart.Test;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Custom.Test
{
    public class GenericCustomGridBusinessRules<TDto, TAutomapperProfile> : IGenericCustomGridBusinessRules<TDto, TAutomapperProfile> where TDto : IBaseDevelopmentCustomDto, new()
                                                                                where TAutomapperProfile : Profile, new()
    {
        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<TDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        //TODO : Parametreler belirlenecek
        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<TDto>>> GetAllFiltered()
        {
            throw new NotImplementedException();
        }

        //TODO : Parametreler belirlenecek
        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<TDto>>> GetAllWithPaging()
        {
            throw new NotImplementedException();
        }

        //TODO : Parametreler belirlenecek
        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<TDto>>> GetAllWithPagingFiltered()
        {
            throw new NotImplementedException();
        }


        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TDto>> Insert(TDto insertedDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TDto>> Update(TDto updatedDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TDto>> DeleteFromInt(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TDto>> DeleteFromLong(long id)
        {
            throw new NotImplementedException();
        }


    }
}

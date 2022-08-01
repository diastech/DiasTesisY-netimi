using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;


namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IFacilityDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.FacilityDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.FacilityDto>> DeleteAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.FacilityDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.FacilityDto>> InsertAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.FacilityDto>> UpdateAsync(BusinessLogicRequest request);
    }
}

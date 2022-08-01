using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;


namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketNoteWrapperDtoRepository
    {
        #region Development

        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketNoteDto>>> GetTicketNoteByTicketIdAsync(int Id);
        public Task<Tuple<Error, CustomDto.CustomTicketNoteDto>> AddTicketNoteWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketNoteDto>> DeleteTicketNoteWrapperAsync(BusinessLogicRequest request);

        #endregion Development
    }
}

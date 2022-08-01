using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom
{
    public class CustomBasicTicketDto : BasicTicketDto, IBaseDevelopmentCustomDto
    {
        public IFormFile AttachmentsFile { get; set; }
        
    }
}

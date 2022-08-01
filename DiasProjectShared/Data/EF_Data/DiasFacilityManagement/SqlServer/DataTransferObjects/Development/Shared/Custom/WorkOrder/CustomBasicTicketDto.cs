using DiasShared.Attributes;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    
    public class CustomBasicTicketDto : BasicTicketDto, IBaseDevelopmentCustomDto
    {
        //[JsonProperty(Required =Required.Always)]
        [Required,Email]
        public string Deneme { get; set; }
        public IFormFile AttachmentsFile { get; set; }
        
    }
}

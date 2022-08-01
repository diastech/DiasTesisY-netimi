using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class BasicTicketDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string TicketDescription { get; set; }
        public int? MobilePhoneNumber { get; set; }
        public int StateId { get; set; }
        public string FirstLastName { get; set; }
        public UserDto AddedByUser { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public List<TicketDto> Tickets { get; set; }
        public BasicTicketStateDto BasicTicketState { get; set; }
    }
}

using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class BasicTicketStateDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string BasicStateDescription { get; set; }

        public UserDto AddedByUser { get; set; }
        public UserDto LastModifiedByUser { get; set; }

        public virtual ICollection<BasicTicketDto> BasicTickets { get; set; }
    }
}

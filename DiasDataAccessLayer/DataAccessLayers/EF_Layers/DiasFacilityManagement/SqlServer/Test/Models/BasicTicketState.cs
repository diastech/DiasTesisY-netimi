using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class BasicTicketState : DevelopmentBaseEntity
    {
      
        public int Id { get; set; }
        public string BasicStateDescription { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }

        public virtual ICollection<BasicTicket> BasicTicketAddedByUsers { get; set; }

    }
}

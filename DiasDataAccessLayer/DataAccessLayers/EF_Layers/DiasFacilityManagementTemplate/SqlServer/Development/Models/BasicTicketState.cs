using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class BasicTicketState : DevelopmentBaseEntity
    {
      
        public int Id { get; set; }
        public string BasicStateDescription { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }

        public virtual ICollection<BasicTicket> BasicTicketStateFK { get; set; }

    }
}

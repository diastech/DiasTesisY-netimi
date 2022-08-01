using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development
{
    public interface IDevelopmentBaseEntity
    {
        public int AddedByUserId { get; set; }
        public DateTime AddedTime { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class TicketReasonCategoryV2 : DevelopmentBaseEntity
    {
        public TicketReasonCategoryV2()
        {
            ResolutionFormV2s = new HashSet<ResolutionFormV2>();
            ResolutionForms = new HashSet<ResolutionForm>();
            TicketReasons = new HashSet<TicketReason>();
        }

        public int Id { get; set; }
        public HierarchyId HierarchyId { get; set; }
        public HierarchyId OldHierarchyId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ICollection<ResolutionFormV2> ResolutionFormV2s { get; set; }
        public virtual ICollection<ResolutionForm> ResolutionForms { get; set; }
        public virtual ICollection<TicketReason> TicketReasons { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class VwTicket
    {
        public int Id { get; set; }
        public int TicketReasonId { get; set; }
        public string ReasonName { get; set; }
        public string ReasonCategory { get; set; }
        public int TicketStatusId { get; set; }
        public string StateDescription { get; set; }
        public int TicketPriority { get; set; }
        public string TicketDescription { get; set; }
        public DateTime TicketOpenedTime { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public DateTime? ExpectedResponseTime { get; set; }
        public DateTime? ExpectedResolutionTime { get; set; }
        public string TicketLocations { get; set; }
        public string LocationHierarchy { get; set; }
        public string LocationName { get; set; }
        public string TicketOwnerUserId { get; set; }
        public string TicketOwnerUser { get; set; }
        public string ResponsibleUserId { get; set; }
        public string ResponsibleUser { get; set; }
        public int? TickedAssignedAssignmentGroupId { get; set; }
        public string GroupName { get; set; }
    }
}

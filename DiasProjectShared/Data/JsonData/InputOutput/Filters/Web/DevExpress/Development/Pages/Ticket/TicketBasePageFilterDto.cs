using System;
using System.Collections.Generic;

namespace DiasShared.Data.JsonData.InputOutput.Filters.Web.DevExpress.Development
{
    public class TicketBasePageFilterDto
    {
        public List<string> TicketReasonIds { get; set; }

        public List<string> FilterLocationIds { get; set; }

        public List<int> ListTicketReasonId { get; set; }

        public List<int> ListLocationId { get; set; }

        public int PriorityId { get; set; }

        public int TicketStatusId { get; set; }

        //sorumlu kişi
        public int UserId { get; set; }

        public string TicketDescription { get; set; }

        public string TicketCode { get; set; }

        public DateTime TicketResponseTimeTargetedStart { get; set; }

        public DateTime TicketResponseTimeTargetedEnd { get; set; }

        public DateTime TicketResolutionTimeTargetedStart { get; set; }

        public DateTime TicketResolutionTimeTargetedEnd { get; set; }

        public string ReportedUser { get; set; }

        public string PhoneNumber { get; set; }

        public int AssignedGroupId { get; set; }

        public DateTime ResponsedTime { get; set; }

        public DateTime ResoulitionedTime { get; set; }

        public DateTime TicketClosedTime { get; set; }

        public DateTime TicketDateTicketStart { get; set; }

        public DateTime TicketDateTicketEnd { get; set; }

        public int InsertUser { get; set; }
    }
}
